using PPPK.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPPK.DAL.Implementations.SqlConnections
{
    public class TravelOrderTypeRepository : SqlConnectionGenericRepository<TravelOrderType>, IRepository<TravelOrderType>
    {
        public TravelOrderTypeRepository(SqlConnection connection) : base(connection)
        {
        }

        protected override void DeleteCommandParameters(long id, SqlCommand cmd)
        {
            
        }

        protected override IEnumerable<TravelOrderType> GetAllEntitiesFromReader(SqlCommand cmd)
        {
            cmd.CommandText = "select * from TravelOrderType";
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var type = default(TravelOrderType);
                        yield return GetTravelOrderTypeFromReader(type, reader);
                    }
                }
            }
        }

        private TravelOrderType GetTravelOrderTypeFromReader(TravelOrderType type, SqlDataReader reader)
        {
            return new TravelOrderType
            {
                IDTravelOrderType = Convert.ToInt64(reader[nameof(type.IDTravelOrderType)].ToString()),
                Type = reader[nameof(type.Type)].ToString()
            };
        }

        protected override TravelOrderType GetEntityFromReader(long id, SqlCommand cmd)
        {
            var type = default(TravelOrderType);
            cmd.CommandText = $"select * from TravelOrderType where ${nameof(type.IDTravelOrderType)} = {id}";
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return GetTravelOrderTypeFromReader(type, reader);
                    }
                }
                return null;
            }
        }

        protected override void InsertCommandParameters(TravelOrderType entity, SqlCommand cmd, out SqlParameter newId)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateCommandParameters(TravelOrderType entity, SqlCommand cmd)
        {
            
        }
    }
}