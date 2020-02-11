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
            var vehicle = unitOfWork.VehicleRepository.GetById(vehicleID);
            var vehicles = unitOfWork.VehicleRepository.GetAll().ToList();
            var services = unitOfWork.ServiceInfoRepository.GetAll().ToList();
            //var si = vehicle.ServiceInfos;

            return null;
        }
    }
}