using PPPK.Models;
using System.Collections.Generic;

namespace PPPK.ViewModels
{
    public class TravelOrderViewModel
    {
        public TravelOrder TravelOrder { get; set; }
        public IList<City> Cities { get; set; }
        public IList<TravelOrderType> TravelOrderTypes { get; set; }
        public IList<Driver> Drivers { get; set; }
        public IList<Vehicle> Vehicles { get; set; }

        public TravelOrderViewModel()
        {
            TravelOrder = new TravelOrder();
            Cities = new List<City>();
            TravelOrderTypes = new List<TravelOrderType>();
            Drivers = new List<Driver>();
            Vehicles = new List<Vehicle>();
        }
    }
}