using PPPK.DAL;
using PPPK.DAL.Implementations;
using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPK.Services
{
    public class EntityService
    {
        private readonly IUnitOfWork unitOfWork;

        public EntityService()
        {
            unitOfWork = new AppUnitOfWork(Enums.RepoMode.Entity);
        }

        public IEnumerable<ServiceInfo> GetServices(long vehicleID)
        {
            using (var context = new ApplicationContext())
            {
                var cities = context.Cities.ToList();
                var vehicles = context.Vehicles.ToList();
            }
            var vehicle = unitOfWork.VehicleRepository.GetById(vehicleID);
            
            //var si = vehicle.ServiceInfos;

            return null;
        }
    }
}