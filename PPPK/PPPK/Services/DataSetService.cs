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
        private const string PATH = "App_Data/routeInfo.xml";
        private AppUnitOfWork unitOfWork;
        private SqlConnection _sqlConnection;
        private SqlDataAdapter dataAdapter;

        public DataSetService()
        {
            unitOfWork = new AppUnitOfWork();
            _sqlConnection = unitOfWork.Connection;
            dataAdapter = new SqlDataAdapter("SELECT * FROM RouteInfo", _sqlConnection);
        }

        public void ExportToXml()
        {
            DataSet dataSet = new DataSet("TravelOrderRouteInfo");
            dataAdapter.Fill(dataSet);

            dataSet.Tables[0].TableName = "RouteInfo";

            dataSet.WriteXml(HostingEnvironment.MapPath("App_Data/routeInfo.xml"), XmlWriteMode.IgnoreSchema);
        }

        public void ImportFromXml()
        {
            DataSet dataSet = new DataSet();

            var cmdBuilder = new SqlCommandBuilder(dataAdapter);
            
            dataSet.ReadXml(HostingEnvironment.MapPath(PATH));

            dataAdapter.Fill(dataSet);

            dataAdapter.Update(dataSet);
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