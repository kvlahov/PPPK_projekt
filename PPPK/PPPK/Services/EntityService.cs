using PPPK.DAL;
using PPPK.DAL.Implementations;
using PPPK.Models;
using PPPK.ViewModels;
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

        internal List<Vehicle> GetAllVehicles()
        {
            return unitOfWork.VehicleRepository.GetAll().ToList();
        }

        internal List<ServiceInfo> GetServicesForVehicle(int id)
        {
            return unitOfWork.VehicleRepository.GetById(id).ServiceInfos.ToList();
        }

        internal bool InsertService(ServiceInfo serviceInfo)
        {
            try
            {
                unitOfWork.ServiceInfoRepository.Add(serviceInfo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool UpdateService(ServiceInfo serviceInfo)
        {
            try
            {
                unitOfWork.ServiceInfoRepository.Update(serviceInfo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool DeleteService(int id)
        {
            try
            {
                unitOfWork.ServiceInfoRepository.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal VehicleViewModel GetVehicleViewModel(int id)
        {
            var vm = new VehicleViewModel();
            vm.Vehicle = unitOfWork.VehicleRepository.GetById(id);
            vm.Services = vm.Vehicle.ServiceInfos.ToList();

            var avg = vm.Vehicle
                .TravelOrders
                .SelectMany(t => t.RouteInfos)
                .Average(r => r.AverageSpeed);

            vm.AverageSpeed = avg ?? 0;

            var kmSum = vm.Vehicle
                .TravelOrders
                .SelectMany(t => t.RouteInfos)
                .Sum(r => r.DistanceInKm);

            vm.TotalKm = (kmSum ?? 0) + vm.Vehicle.InitialKilometres;

            return vm;
        }
    }
}