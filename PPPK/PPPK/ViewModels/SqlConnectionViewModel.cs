using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPK.ViewModels
{
    public class SqlConnectionViewModel
    {
        public IList<Driver> DriverListViewModel { get; set; }
        public IList<Vehicle> VehicleListViewModel { get; set; }
        public IList<TravelOrder> TravelOrderListViewModel { get; set; }
    }
}