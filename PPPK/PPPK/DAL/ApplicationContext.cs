using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PPPK.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("cs")
        {
        }

        public DbSet<TravelOrderType> TravelOrderTypes { get; set; }
        public DbSet<TravelOrder> TravelOrders { get; set; }
        public DbSet<ServiceInfo> ServiceInfos { get; set; }
        public DbSet<RouteInfo> RouteInfos { get; set; }
        public DbSet<FuelInfo> FuelInfos { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}