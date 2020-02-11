using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=pppk")
        {
        }

        public virtual DbSet<TravelOrderType> TravelOrderTypes { get; set; }
        public virtual DbSet<TravelOrder> TravelOrders { get; set; }
        public virtual DbSet<ServiceInfo> ServiceInfos { get; set; }
        public virtual DbSet<RouteInfo> RouteInfos { get; set; }
        public virtual DbSet<FuelInfo> FuelInfos { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.ServiceInfos)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleID);
            //modelBuilder.Entity<FuelInfo>()
            //    .HasMany(e => e.)

            //modelBuilder.Entity<ServiceInfo>()
            //    .Property(e => e.Cost)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<TravelOrder>()
            //    .Property(e => e.TotalCost)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<TravelOrder>()
            //    .HasMany(e => e.FuelInfo)
            //    .WithOptional(e => e.TravelOrder)
            //    .HasForeignKey(e => e.TravelOrderID);

            //modelBuilder.Entity<TravelOrder>()
            //    .HasMany(e => e.RouteInfo)
            //    .WithOptional(e => e.TravelOrder)
            //    .HasForeignKey(e => e.TravelOrderID);

            //modelBuilder.Entity<TravelOrderType>()
            //    .HasMany(e => e.TravelOrder)
            //    .WithRequired(e => e.TravelOrderType)
            //    .HasForeignKey(e => e.TravelOrderTypeID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Vehicle>()
            //    .HasMany(e => e.ServiceInfo)
            //    .WithRequired(e => e.Vehicle)
            //    .HasForeignKey(e => e.VehicleID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Vehicle>()
            //    .HasMany(e => e.TravelOrder)
            //    .WithRequired(e => e.Vehicle)
            //    .HasForeignKey(e => e.VehicleID)
            //    .WillCascadeOnDelete(false);
        }
    }
}