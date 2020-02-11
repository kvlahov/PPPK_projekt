using PPPK.DAL.Interfaces.SqlConnections;
using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK.DAL
{
    public interface IUnitOfWork
    {
        IRepository<Driver> DriverRepository { get; }
        IRepository<Vehicle> VehicleRepository { get; }
        IRepository<City> CityRepository { get; }
        IRepository<TravelOrder> TravelOrderRepository { get; }
        IRepository<TravelOrderType> TravelOrderTypeRepository { get; }
        IRepository<RouteInfo> RouteInfoRepository { get; }
        IRepository<ServiceInfo> ServiceInfoRepository { get; }
        IRepository<FuelInfo> FuelInfoRepository { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void SaveChanges();
    }
}
