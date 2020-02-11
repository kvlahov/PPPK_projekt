using PPPK.DAL.Interfaces.SqlConnections;
using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPPK.DAL.Implementations.SqlConnections
{
    class TravelOrderRepository : SqlConnectionGenericRepository<TravelOrder>, ITravelOrderRepository
    {
        public TravelOrderRepository(SqlConnection connection) : base(connection)
        {
        }

        protected override void DeleteCommandParameters(long id, SqlCommand cmd)
        {
            var type = default(TravelOrder);
            cmd.CommandText = "deleteTravelOrder";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue($"@{nameof(type.IDTravelOrder)}", id);
        }

        protected override IEnumerable<TravelOrder> GetAllEntitiesFromReader(SqlCommand cmd)
        {
            cmd.CommandText = "getAllTravelOrders";
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var type = default(TravelOrder);
                        yield return GetTravelOrderFromReader(reader, type);
                    }
                }
            }
        }

        protected override TravelOrder GetEntityFromReader(long id, SqlCommand cmd)
        {
            cmd.CommandText = "getTravelOrder";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDTravelOrder", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var type = default(TravelOrder);
                        return GetTravelOrderFromReader(reader, type);
                    }
                }
                return null;
            }
        }

        private TravelOrder GetTravelOrderFromReader(SqlDataReader reader, TravelOrder type)
        {
            return new TravelOrder
            {
                IDTravelOrder = Convert.ToInt32(reader[nameof(type.IDTravelOrder)].ToString()),
                DriverID = Convert.ToInt32(reader[nameof(type.DriverID)].ToString()),
                VehicleID = Convert.ToInt32(reader[nameof(type.VehicleID)].ToString()),
                TravelOrderTypeID = Convert.ToInt32(reader[nameof(type.TravelOrderTypeID)].ToString()),
                ExpectedNumberOfDays = Convert.ToInt32(reader[nameof(type.ExpectedNumberOfDays)].ToString()),
                ReasonForTravel = reader[nameof(type.ReasonForTravel)].ToString(),
                CityStartId = Convert.ToInt32(reader[nameof(type.CityStartId)].ToString()),
                CityEndId = Convert.ToInt32(reader[nameof(type.CityEndId)].ToString()),
                DocumentDate = Convert.ToDateTime(reader[nameof(type.DocumentDate)].ToString())                
            };
        }

        protected override void InsertCommandParameters(TravelOrder entity, SqlCommand cmd, out SqlParameter newId)
        {
            cmd.CommandText = "insertTravelOrder";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue($"@{nameof(entity.DriverID)}", entity.DriverID);
            cmd.Parameters.AddWithValue($"@{nameof(entity.VehicleID)}", entity.VehicleID);
            cmd.Parameters.AddWithValue($"@{nameof(entity.TravelOrderTypeID)}", entity.TravelOrderTypeID);
            cmd.Parameters.AddWithValue($"@{nameof(entity.ExpectedNumberOfDays)}", entity.ExpectedNumberOfDays);
            cmd.Parameters.AddWithValue($"@{nameof(entity.ReasonForTravel)}", entity.ReasonForTravel);
            cmd.Parameters.AddWithValue($"@{nameof(entity.CityStartId)}", entity.CityStartId);
            cmd.Parameters.AddWithValue($"@{nameof(entity.CityEndId)}", entity.CityEndId);
            cmd.Parameters.AddWithValue($"@{nameof(entity.TotalCost)}", entity.TotalCost);
            cmd.Parameters.AddWithValue($"@{nameof(entity.DocumentDate)}", entity.DocumentDate);

            newId = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(newId);
        }

        protected override void UpdateCommandParameters(TravelOrder entity, SqlCommand cmd)
        {
            cmd.CommandText = "updateTravelOrder";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue($"@{nameof(entity.IDTravelOrder)}", entity.IDTravelOrder);
            cmd.Parameters.AddWithValue($"@{nameof(entity.DriverID)}", entity.DriverID);
            cmd.Parameters.AddWithValue($"@{nameof(entity.VehicleID)}", entity.VehicleID);
            cmd.Parameters.AddWithValue($"@{nameof(entity.TravelOrderTypeID)}", entity.TravelOrderTypeID);
            cmd.Parameters.AddWithValue($"@{nameof(entity.ExpectedNumberOfDays)}", entity.ExpectedNumberOfDays);
            cmd.Parameters.AddWithValue($"@{nameof(entity.ReasonForTravel)}", entity.ReasonForTravel);
            cmd.Parameters.AddWithValue($"@{nameof(entity.CityStartId)}", entity.CityStartId);
            cmd.Parameters.AddWithValue($"@{nameof(entity.CityEndId)}", entity.CityEndId);
            cmd.Parameters.AddWithValue($"@{nameof(entity.DocumentDate)}", entity.DocumentDate);
        }
    }
}