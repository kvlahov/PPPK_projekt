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
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public IDriverRepository DriverRepository { get; private set; }

        public IVehicleRepository VehicleRepository { get; private set; }

        public ICityRepository CityRepository { get; private set; }

        public ITravelOrderRepository TravelOrderRepository{ get; private set; }
        public IRepository<TravelOrderType> TravelOrderTypeRepository { get; private set; }

        public AppUnitOfWork()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            DriverRepository = new DriverRepository(connection);
            VehicleRepository = new VehicleRepository(connection);
            CityRepository = new CityRepository(connection);
            TravelOrderRepository = new TravelOrderRepository(connection);
            TravelOrderTypeRepository = new TravelOrderTypeRepository(connection);
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTranasction()
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