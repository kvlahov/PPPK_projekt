using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PPPK.DAL.Implementations.SqlConnections;
using PPPK.DAL.Interfaces.SqlConnections;
using PPPK.Models;

namespace PPPK.DAL.Implementations
{
    public class AppUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public IDriverRepository DriverRepository { get; private set; }

        public IVehicleRepository VehicleRepository { get; private set; }

        public ICityRepository CityRepository { get; private set; }

        public ITravelOrderRepository TravelOrderRepository{ get; private set; }
        public IRepository<TravelOrderType> TravelOrderTypeRepository { get; private set; }

        public AppUnitOfWork()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            DriverRepository = new DriverRepository(_connection);
            VehicleRepository = new VehicleRepository(_connection);
            CityRepository = new CityRepository(_connection);
            TravelOrderRepository = new TravelOrderRepository(_connection);
            TravelOrderTypeRepository = new TravelOrderTypeRepository(_connection);
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _transaction.Dispose();
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