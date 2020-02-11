namespace TestEntity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=PPPK")
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<FuelInfo> FuelInfo { get; set; }
        public virtual DbSet<RouteInfo> RouteInfo { get; set; }
        public virtual DbSet<ServiceInfo> ServiceInfo { get; set; }
        public virtual DbSet<TravelOrder> TravelOrder { get; set; }
        public virtual DbSet<TravelOrderType> TravelOrderType { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(e => e.TravelOrder)
                .WithOptional(e => e.City)
                .HasForeignKey(e => e.CityEndId);

            modelBuilder.Entity<City>()
                .HasMany(e => e.TravelOrder1)
                .WithOptional(e => e.City1)
                .HasForeignKey(e => e.CityStartId);

            modelBuilder.Entity<Driver>()
                .HasMany(e => e.TravelOrder)
                .WithRequired(e => e.Driver)
                .HasForeignKey(e => e.DriverID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FuelInfo>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ServiceInfo>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TravelOrder>()
                .Property(e => e.TotalCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TravelOrder>()
                .HasMany(e => e.FuelInfo)
                .WithOptional(e => e.TravelOrder)
                .HasForeignKey(e => e.TravelOrderID);

            modelBuilder.Entity<TravelOrder>()
                .HasMany(e => e.RouteInfo)
                .WithOptional(e => e.TravelOrder)
                .HasForeignKey(e => e.TravelOrderID);

            modelBuilder.Entity<TravelOrderType>()
                .HasMany(e => e.TravelOrder)
                .WithRequired(e => e.TravelOrderType)
                .HasForeignKey(e => e.TravelOrderTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.ServiceInfo)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.TravelOrder)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleID)
                .WillCascadeOnDelete(false);
        }
    }
}
