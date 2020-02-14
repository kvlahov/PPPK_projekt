using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PPPK.DAL.Implementations.DAAB;
using PPPK.DAL.Implementations.Entity;
using PPPK.DAL.Implementations.SqlConnections;
using PPPK.DAL.Interfaces.SqlConnections;
using PPPK.Enums;
using PPPK.Models;

namespace PPPK.DAL.Implementations
{
    public class AppUnitOfWork : IUnitOfWork, IDisposable
    {
        public SqlConnection Connection { get; private set; }
        public SqlTransaction Transaction { get; private set; }
        string _connectionString;
        ApplicationContext _context;

        public IRepository<Driver> DriverRepository { get; private set; }

        public IRepository<Vehicle> VehicleRepository { get; private set; }

        public IRepository<City> CityRepository { get; private set; }

        public IRepository<TravelOrder> TravelOrderRepository { get; private set; }
        public IRepository<TravelOrderType> TravelOrderTypeRepository { get; private set; }

        public IRepository<RouteInfo> RouteInfoRepository { get; private set; }

        public IRepository<ServiceInfo> ServiceInfoRepository { get; private set; }

        public IRepository<FuelInfo> FuelInfoRepository { get; private set; }

        public AppUnitOfWork() : this(RepoMode.Mixed)
        {
        }

        public AppUnitOfWork(RepoMode mode)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            _context = new ApplicationContext();

            if (mode == RepoMode.Entity)
            {

                DriverRepository = new EntityRepository<Driver>(_context);
                VehicleRepository = new EntityRepository<Vehicle>(_context);
                CityRepository = new EntityRepository<City>(_context);
                TravelOrderRepository = new EntityRepository<TravelOrder>(_context);
                TravelOrderTypeRepository = new EntityRepository<TravelOrderType>(_context);
                RouteInfoRepository = new EntityRepository<RouteInfo>(_context);

                ServiceInfoRepository = new EntityRepository<ServiceInfo>(_context);
                FuelInfoRepository = new EntityRepository<FuelInfo>(_context);
            }
            else
            {
                Connection = new SqlConnection(_connectionString);
                Connection.Open();

                DriverRepository = new DriverRepository(Connection);
                VehicleRepository = new VehicleRepository(Connection);
                CityRepository = new CityRepository(Connection);
                TravelOrderRepository = new TravelOrderRepository(Connection);
                TravelOrderTypeRepository = new TravelOrderTypeRepository(Connection);

                RouteInfoRepository = new DaabRepository(_connectionString);
                ServiceInfoRepository = new EntityRepository<ServiceInfo>(_context);
                FuelInfoRepository = new EntityRepository<FuelInfo>(_context);
            }
        }

        public void BeginTransaction()
        {
            Transaction = Connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            Transaction.Rollback();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void ClearDatabase()
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "clearDatabase";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Transaction.Dispose();
                    Connection.Close();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion




    }
}