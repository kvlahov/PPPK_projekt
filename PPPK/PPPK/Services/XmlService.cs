using PPPK.DAL.Implementations;
using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;
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

            StringBuilder sb = new StringBuilder();

            tableList.ForEach(t => sb.Append($" SELECT * from {t}; "));

            dataAdapter = new SqlDataAdapter(sb.ToString(), _sqlConnection);

            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            tables = new Dictionary<string, DataTable>();

            for (int i = 0; i < tableList.Count; i++)
            {
                tables.Add(tableList[i], dataSet.Tables[i]);
            }
        }

        public void CreateBackup()
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
                    //add each property
                    Type.GetType("PPPK.Models." + entry.Key).GetProperties()
                        .Where(p => !p.GetGetMethod().IsVirtual)
                        .ToList()
                        .ForEach(p => {
                        xmlWriter.WriteElementString(p.Name, row[p.Name].ToString());
                    });
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();

            xmlWriter.Close();
        }

        public void ImportBackup()
        {
            XmlDocument xmlDOM = new XmlDocument();
            xmlDOM.Load(HostingEnvironment.MapPath(backupPath));

            dataSet.Clear();
            dataAdapter.Update(dataSet);

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
                        newRow[properties.Name] = properties.FirstChild.Value;
                    }
                    tables[entity.Name].Rows.Add(newRow);
                }
                
            }

            dataAdapter.Update(dataSet);
        }

        private string Pluralize(string text) => text + "s";


        private string Singularize(string text) => text.Remove(text.Count() - 1);
    }
}