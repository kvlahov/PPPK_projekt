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
    class CityRepository : SqlConnectionGenericRepository<City>, ICityRepository
    {
        public CityRepository(SqlConnection connection) : base(connection)
        {
        }

        protected override void DeleteCommandParameters(long id, SqlCommand cmd)
        {
            
        }

        protected override IEnumerable<City> GetAllEntitiesFromReader(SqlCommand cmd)
        {
            cmd.CommandText = "select * from City";
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var type = default(City);
                        yield return GetCityFromReader(type, reader);
                    }
                }
            }
        }

        protected override City GetEntityFromReader(long id, SqlCommand cmd)
        {
            var type = default(City);

            cmd.CommandText = $"select * from City where {nameof(type.IDCity)} = {id} ";
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return GetCityFromReader(type, reader);
                    }
                }
                return null;
            }
        }

        protected override void InsertCommandParameters(City entity, SqlCommand cmd, out SqlParameter newId)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateCommandParameters(City entity, SqlCommand cmd)
        {
            
        }

        private City GetCityFromReader(City type, SqlDataReader reader)
        {
            return new City
            {
                IDCity = Convert.ToInt64(reader[nameof(type.IDCity)].ToString()),
                Name = reader[nameof(type.Name)].ToString()
            };
        }
    }
}