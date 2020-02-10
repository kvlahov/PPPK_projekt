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
        public SqlConnection Connection { get; private set; }
        private SqlTransaction _transaction;

        public IDriverRepository DriverRepository { get; private set; }

        public IVehicleRepository VehicleRepository { get; private set; }

        public ICityRepository CityRepository { get; private set; }

        public ITravelOrderRepository TravelOrderRepository{ get; private set; }
        public IRepository<TravelOrderType> TravelOrderTypeRepository { get; private set; }

        public AppUnitOfWork()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            Connection = new SqlConnection(connectionString);
            Connection.Open();

            DriverRepository = new DriverRepository(Connection);
            VehicleRepository = new VehicleRepository(Connection);
            CityRepository = new CityRepository(Connection);
            TravelOrderRepository = new TravelOrderRepository(Connection);
            TravelOrderTypeRepository = new TravelOrderTypeRepository(Connection);
        }

        public void BeginTransaction()
        {
            _transaction = Connection.BeginTransaction();
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