using PPPK.DAL.Implementations;
using PPPK.Helpers;
using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace PPPK.Services
{
    public class XmlService
    {
        private AppUnitOfWork unitOfWork;

        private SqlConnection _sqlConnection;
        private IDictionary<string, SqlDataAdapter> dataAdapters;

        //private DataSet dataSet;
        private string backupPath = "~/App_Data/Backup.xml";

        private IDictionary<string, DataTable> tables;

        public XmlService()
        {
            unitOfWork = new AppUnitOfWork();
            _sqlConnection = unitOfWork.Connection;
            InitTables();

        }

        private void InitTables()
        {
            List<string> tableList = new List<string>
            {
                nameof(Driver),
                nameof(Vehicle),
                nameof(City),
                nameof(TravelOrderType),
                nameof(ServiceInfo),
                nameof(TravelOrder),
                nameof(FuelInfo),
                nameof(RouteInfo)
            };

            dataAdapters = new Dictionary<string, SqlDataAdapter>();
            tables = new Dictionary<string, DataTable>();

            List<DataSet> dataSets = new List<DataSet>();

            tableList.ForEach(t =>
            {
                var da = new SqlDataAdapter($" SELECT * from {t};", _sqlConnection);
                var cmdBuilder = new SqlCommandBuilder(da);

                var ds = new DataSet();
                da.Fill(ds);

                tables.Add(t, ds.Tables[0]);

                dataAdapters.Add(t, da);
            });
        }

        public bool CreateBackup()
        {
            try
            {
                XmlWriterSettings xmlSettings = new XmlWriterSettings
                {
                    Indent = true
                };

                XmlWriter xmlWriter = XmlWriter.Create(HostingEnvironment.MapPath(backupPath), xmlSettings);

                //XML declaration
                xmlWriter.WriteStartDocument();

                //root
                xmlWriter.WriteStartElement("Backup");

                foreach (KeyValuePair<string, DataTable> entry in tables)
                {
                    //table name
                    xmlWriter.WriteStartElement(Pluralize(entry.Key));

                    //iterate over rows of table
                    foreach (DataRow row in entry.Value.Rows)
                    {
                        xmlWriter.WriteStartElement(entry.Key);
                        //add each property if it's not virtual or collection
                        Type.GetType("PPPK.Models." + entry.Key).GetProperties()
                            .Where(p => !p.GetGetMethod().IsVirtual)
                            .Where(p => !p.PropertyType.IsCollection())
                            .ToList()
                            .ForEach(p =>
                            {
                                xmlWriter.WriteElementString(p.Name, row[p.Name].ToString());
                            });
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();

                xmlWriter.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ClearDatabase()
        {
            try
            {
                unitOfWork.ClearDatabase();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ImportBackup()
        {
            try
            {
                XmlDocument xmlDOM = new XmlDocument();
                xmlDOM.Load(HostingEnvironment.MapPath(backupPath));

                //dataAdapter.Update(dataSet);

                foreach (KeyValuePair<string, DataTable> entry in tables)
                {
                    //table name
                    XmlNodeList entities = xmlDOM.GetElementsByTagName(entry.Key);

                    //iterate over rows of table
                    foreach (XmlNode entity in entities)
                    {
                        var newRow = tables[entity.Name].NewRow();
                        foreach (XmlNode properties in entity.ChildNodes)
                        {
                            newRow[properties.Name] = (object)properties?.FirstChild?.Value ?? DBNull.Value;
                        }
                        tables[entity.Name].Rows.Add(newRow);

                    }

                }
                unitOfWork.BeginTransaction();
                using (var bulkCopy = new SqlBulkCopy(_sqlConnection.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                {
                    foreach (var entry in tables)
                    {
                        bulkCopy.ColumnMappings.Clear();
                        foreach (DataColumn col in entry.Value.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }
                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.DestinationTableName = entry.Key;
                        bulkCopy.WriteToServer(entry.Value);
                    }
                }

                //dataAdapters.Keys.ToList().ForEach(table => dataAdapters[table].Update(tables[table]));

                unitOfWork.CommitTransaction();
                return true;

            }
            catch (Exception)
            {
                unitOfWork.RollbackTransaction();
                return false;
            }
        }

        private string Pluralize(string text) => text + "s";
    }
}