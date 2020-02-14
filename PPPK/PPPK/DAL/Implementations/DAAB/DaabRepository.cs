using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPPK.DAL.Implementations.DAAB
{
    public class DaabRepository : IRepository<RouteInfo>
    {
        private SqlDatabase db;
        public DaabRepository(string connectionString)
        {
            db = new SqlDatabase(connectionString);
        }

        public long Add(RouteInfo entity)
        {
            var parameters = new object[]
            {
                entity.DateTimeStart,
                entity.DateTimeEnd,
                entity.LatitudeStart,
                entity.LongitudeStart,
                entity.LatitudeEnd,
                entity.LongitudeEnd,
                entity.DistanceInKm,
                entity.AverageSpeed,
                entity.FuelExpense,
                entity.TravelOrderID
            };

            return db.ExecuteNonQuery("insertRouteInfo", parameters);
        }

        public int Delete(long id)
        {
            return db.ExecuteNonQuery("deleteRouteInfo", id);
        }

        public IEnumerable<RouteInfo> GetAll()
        {
            using (IDataReader dr = db.ExecuteReader(CommandType.StoredProcedure, "getAllRouteInfoes"))
            {
                while (dr.Read())
                {
                    yield return new RouteInfo
                    {
                        IDRouteInfo = Convert.ToInt32(dr[nameof(RouteInfo.IDRouteInfo)]),
                        DateTimeStart = ToDateTimeSafe(dr[nameof(RouteInfo.DateTimeStart)]),
                        DateTimeEnd = ToDateTimeSafe(dr[nameof(RouteInfo.DateTimeEnd)]),
                        LatitudeStart = ToTypeSafely<double?>(dr[nameof(RouteInfo.LatitudeStart)]),
                        LongitudeStart = ToTypeSafely<double?>(dr[nameof(RouteInfo.LongitudeStart)]),
                        LatitudeEnd = ToTypeSafely<double?>(dr[nameof(RouteInfo.LatitudeEnd)]),
                        LongitudeEnd = ToTypeSafely<double?>(dr[nameof(RouteInfo.LongitudeEnd)]),
                        DistanceInKm = ToTypeSafely<double?>(dr[nameof(RouteInfo.DistanceInKm)]),
                        AverageSpeed = ToTypeSafely<double?>(dr[nameof(RouteInfo.AverageSpeed)]),
                        FuelExpense = ToTypeSafely<double?>(dr[nameof(RouteInfo.FuelExpense)]),
                        TravelOrderID = ToTypeSafely<int>(dr[nameof(RouteInfo.TravelOrderID)])
                    };
                }
            }
        }

        private DateTime? ToDateTimeSafe(object dataReaderValue) 
            => dataReaderValue == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataReaderValue);
        private T ToTypeSafely<T>(object dataReaderValue)
            => dataReaderValue == DBNull.Value ? default : (T) dataReaderValue;

        public RouteInfo GetById(long id)
        {
            DataSet ds = db.ExecuteDataSet("getRouteInfo", id);
            DataRow dr = ds?.Tables?[0]?.Rows?[0];
            if (dr != null)
            {
                return new RouteInfo
                {
                    IDRouteInfo = Convert.ToInt32(dr[nameof(RouteInfo.IDRouteInfo)]),
                    DateTimeStart = ToDateTimeSafe(dr[nameof(RouteInfo.DateTimeStart)]),
                    DateTimeEnd = ToDateTimeSafe(dr[nameof(RouteInfo.DateTimeEnd)]),
                    LatitudeStart = Convert.ToDouble(dr[nameof(RouteInfo.LatitudeStart)]),
                    LongitudeStart = Convert.ToDouble(dr[nameof(RouteInfo.LongitudeStart)]),
                    LatitudeEnd = Convert.ToDouble(dr[nameof(RouteInfo.LatitudeEnd)]),
                    LongitudeEnd = Convert.ToDouble(dr[nameof(RouteInfo.LongitudeEnd)]),
                    DistanceInKm = Convert.ToDouble(dr[nameof(RouteInfo.DistanceInKm)]),
                    AverageSpeed = Convert.ToDouble(dr[nameof(RouteInfo.AverageSpeed)]),
                    FuelExpense = Convert.ToDouble(dr[nameof(RouteInfo.FuelExpense)]),
                    TravelOrderID = Convert.ToInt32(dr[nameof(RouteInfo.TravelOrderID)])
                };
            }
            return null;
        }

        public int Update(RouteInfo newEntity)
        {
           var parameters = new object[]
           {
                newEntity.IDRouteInfo,
                newEntity.DateTimeStart,
                newEntity.DateTimeEnd,
                newEntity.LatitudeStart,
                newEntity.LongitudeStart,
                newEntity.LatitudeEnd,
                newEntity.LongitudeEnd,
                newEntity.DistanceInKm,
                newEntity.AverageSpeed,
                newEntity.FuelExpense,
                newEntity.TravelOrderID
           };

            return db.ExecuteNonQuery("updateRouteInfo", parameters);
        }
    }
}