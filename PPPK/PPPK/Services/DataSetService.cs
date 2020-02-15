using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using PPPK.DAL;
using PPPK.DAL.Implementations;
using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace PPPK.Services
{
    public class DataSetService
    {
        private const string PATH = "~/App_Data/routeInfo.xml";
        private AppUnitOfWork unitOfWork;
        private SqlConnection _sqlConnection;
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;

        public DataSetService()
        {
            unitOfWork = new AppUnitOfWork();
            _sqlConnection = unitOfWork.Connection;
            dataAdapter = new SqlDataAdapter("SELECT * FROM RouteInfo", _sqlConnection);
            var cmdBuilder = new SqlCommandBuilder(dataAdapter);
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            //dataSet.Tables[0].TableName = "RouteInfo";
        }

        public void ExportToXml()
        {
            dataSet.WriteXml(HostingEnvironment.MapPath(PATH), XmlWriteMode.WriteSchema );
        }

        public bool ImportFromXml()
        {
            try
            {
                dataSet.ReadXml(HostingEnvironment.MapPath(PATH), XmlReadMode.ReadSchema);

                dataAdapter.Update(dataSet);
                dataSet.AcceptChanges();

                return true;
            } catch(Exception)
            {
                return false;
            }
        }

        public List<TravelOrder> GetTravelOrders()
        {
            return unitOfWork.TravelOrderRepository.GetAll().ToList();
        }

        public List<RouteInfo> GetRouteInfoesForTravelOrder(int id)
        {
            return unitOfWork.RouteInfoRepository.GetAll().Where(r => r.TravelOrderID == id).ToList();
        }

        internal RouteInfo GetRouteInfo(int? id)
        {
            return id.HasValue ? unitOfWork.RouteInfoRepository.GetById(id.Value) : new RouteInfo();
        }

        public bool InsertRouteInfo(RouteInfo model)
        {
            return unitOfWork.RouteInfoRepository.Add(model) != 0;
        }

        public bool UpdateRouteInfo(RouteInfo model)
        {
            return unitOfWork.RouteInfoRepository.Update(model) > 0;
        }

        public bool DeleteRouteInfo(int id)
        {
            return unitOfWork.RouteInfoRepository.Delete(id) > 0;
        }
    }
}