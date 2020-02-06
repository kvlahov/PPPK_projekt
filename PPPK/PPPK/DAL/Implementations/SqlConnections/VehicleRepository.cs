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
    class VehicleRepository : SqlConnectionGenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(SqlConnection connection) : base(connection)
        {
        }

        protected override void DeleteCommandParameters(Vehicle entity, SqlCommand cmd)
        {
            cmd.CommandText = "deleteVehicle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue($"@{nameof(entity.IDVehicle)}", entity.IDVehicle);
        }

        protected override IEnumerable<Vehicle> GetAllEntitiesFromReader(SqlCommand cmd)
        {
            cmd.CommandText = "getAllVehicles";
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var type = default(Vehicle);
                        yield return GetVehicleFromReader(reader, type);
                    }
                }
            }
        }

        protected override Vehicle GetEntityFromReader(long id, SqlCommand cmd)
        {
            cmd.CommandText = "getVehicle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDVehicle", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var type = default(Vehicle);
                        return GetVehicleFromReader(reader, type);
                    }
                }
                return null;
            }
        }

        private static Vehicle GetVehicleFromReader(SqlDataReader reader, Vehicle type)
        {
            return new Vehicle
            {
                IDVehicle = Convert.ToInt64(reader[nameof(type.IDVehicle)].ToString()),
                Model = reader[nameof(type.Model)].ToString(),
                Registration = reader[nameof(type.Registration)].ToString(),
                Type = reader[nameof(type.Type)].ToString(),
                YearManufactured = Convert.ToInt32(reader[nameof(type.YearManufactured)].ToString()),
                InitialKilometres = Convert.ToDouble(reader[nameof(type.InitialKilometres)].ToString()),
                IsAvailable = Convert.ToBoolean(reader[nameof(type.IsAvailable)].ToString())
            };
        }

        protected override void InsertCommandParameters(Vehicle entity, SqlCommand cmd, out SqlParameter newId)
        {
            cmd.CommandText = "insertVehicle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue($"@{nameof(entity.Model)}", entity.Model);
            cmd.Parameters.AddWithValue($"@{nameof(entity.Type)}", entity.Type);
            cmd.Parameters.AddWithValue($"@{nameof(entity.Registration)}", entity.Registration);
            cmd.Parameters.AddWithValue($"@{nameof(entity.YearManufactured)}", entity.YearManufactured);
            cmd.Parameters.AddWithValue($"@{nameof(entity.InitialKilometres)}", entity.InitialKilometres);

            newId = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.Add(newId);
        }

        protected override void UpdateCommandParameters(Vehicle entity, SqlCommand cmd)
        {
            cmd.CommandText = "updateVehicle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue($"@{nameof(entity.IDVehicle)}", entity.IDVehicle);
            cmd.Parameters.AddWithValue($"@{nameof(entity.Model)}", entity.Model);
            cmd.Parameters.AddWithValue($"@{nameof(entity.Type)}", entity.Type);
            cmd.Parameters.AddWithValue($"@{nameof(entity.Registration)}", entity.Registration);
            cmd.Parameters.AddWithValue($"@{nameof(entity.YearManufactured)}", entity.YearManufactured);
            cmd.Parameters.AddWithValue($"@{nameof(entity.InitialKilometres)}", entity.InitialKilometres);
        }
    }
}