using PPPK.DAL;
using PPPK.DAL.Implementations;
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

        private IEnumerable<TravelOrderViewModel> GetTraverOrderListViewModel()
        {
            TravelOrderViewModel vm = new TravelOrderViewModel();

            return null;
        }
    }
}