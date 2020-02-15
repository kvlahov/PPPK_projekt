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
    public class SqlConnectionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SqlConnectionService()
        {
            _unitOfWork = new AppUnitOfWork();
        }

        public SqlConnectionViewModel GetSqlConnectionViewModel()
        {
            SqlConnectionViewModel viewModel = new SqlConnectionViewModel();
            viewModel.DriverListViewModel = _unitOfWork.DriverRepository.GetAll().ToList();
            viewModel.VehicleListViewModel = _unitOfWork.VehicleRepository.GetAll().ToList();
            viewModel.TravelOrderListViewModel = _unitOfWork.TravelOrderRepository.GetAll().ToList();

            return viewModel;
        }

        public TravelOrderViewModel GetTravelOrderViewModel(long? id)
        {
            var viewModel = new TravelOrderViewModel();
            viewModel.Drivers = _unitOfWork.DriverRepository.GetAll().ToList();
            viewModel.Vehicles = _unitOfWork.VehicleRepository.GetAll().ToList();
            viewModel.Cities = _unitOfWork.CityRepository.GetAll().ToList();
            viewModel.TravelOrderTypes = _unitOfWork.TravelOrderTypeRepository.GetAll().ToList();

            viewModel.TravelOrder = id.HasValue ? _unitOfWork.TravelOrderRepository.GetById(id.Value) : new Models.TravelOrder(); ;

            return viewModel;

        }

        internal bool RemoveTravelOrder(long id)
        {

            var rowsAffected = _unitOfWork.TravelOrderRepository.Delete(id);

            return rowsAffected > 0;
        }

        internal bool UpdateTravelOrder(TravelOrder travelOrder)
        {
            var rowsAffected = _unitOfWork.TravelOrderRepository.Update(travelOrder);

            return rowsAffected > 0;
        }

        internal bool InsertTravelOrder(TravelOrder travelOrder)
        {
            travelOrder.DocumentDate = DateTime.Now;
            var newID = _unitOfWork.TravelOrderRepository.Add(travelOrder);

            return newID != 0;
        }

        internal Driver GetDriver(long id)
        {
            return _unitOfWork.DriverRepository.GetById(id);
        }

        internal Vehicle GetVehicle(long id)
        {
            return _unitOfWork.VehicleRepository.GetById(id);
        }

        internal bool InsertDriver(Driver model)
        {
            var newId  = _unitOfWork.DriverRepository.Add(model);
            return newId != 0;
        }

        internal bool UpdateDriver(Driver model)
        {
            var rowsAffected = _unitOfWork.DriverRepository.Update(model);
            return rowsAffected > 0;
        }

        internal bool DeleteDriver(long id)
        {
            var rowsAffected = _unitOfWork.DriverRepository.Delete(id);
            return rowsAffected > 0;
        }

        internal object InsertVehicle(Vehicle model)
        {
            model.IsAvailable = true;
            var newId = _unitOfWork.VehicleRepository.Add(model);
            return newId != 0;
        }

        internal object UpdateVehicle(Vehicle model)
        {
            var rowsAffected = _unitOfWork.VehicleRepository.Update(model);
            return rowsAffected > 0;
        }

        internal object DeleteVehicle(long id)
        {
            var rowsAffected = _unitOfWork.VehicleRepository.Delete(id);
            return rowsAffected > 0;
        }
    }
}