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
        IDriverRepository DriverRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        ICityRepository CityRepository { get; }
        ITravelOrderRepository TravelOrderRepository { get; }
        IRepository<TravelOrderType> TravelOrderTypeRepository { get; }

        void BeginTransaction();
        void CommitTranasction();
        void RollbackTransaction();
    }
}
