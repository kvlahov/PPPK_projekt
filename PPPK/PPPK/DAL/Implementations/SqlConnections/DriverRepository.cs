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
    public class DriverRepository : SqlConnectionGenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(SqlConnection connection) : base(connection)
        {
        }

        protected override void DeleteCommandParameters(Driver entity, SqlCommand cmd)
        {
            cmd.CommandText = $"delete from Driver where IDDriver = {entity.IDDriver}";
            cmd.CommandType = CommandType.Text;
        }

        protected override IEnumerable<Driver> GetAllEntitiesFromReader(SqlCommand cmd)
        {
            cmd.CommandText = "select * from Driver";
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var type = default(Driver);
                        yield return new Driver
                        {
                            IDDriver = Convert.ToInt64(reader[nameof(type.IDDriver)]),
                            FirstName = reader[nameof(type.FirstName)].ToString(),
                            LastName = reader[nameof(type.LastName)].ToString()
                        };
                    }
                }
            }

        }

        protected override Driver GetEntityFromReader(long id, SqlCommand cmd)
        {
            var type = default(Driver);

            cmd.CommandText = $"select * from Driver where {nameof(type.IDDriver)} = {id} ";
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new Driver
                        {
                            IDDriver = Convert.ToInt64(reader[nameof(type.IDDriver)]),
                            FirstName = reader[nameof(type.FirstName)].ToString(),
                            LastName = reader[nameof(type.LastName)].ToString()
                        };
                    }
                }
                return null;
            }
        }

        protected override void InsertCommandParameters(Driver entity, SqlCommand cmd, out SqlParameter newId)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateCommandParameters(Driver entity, SqlCommand cmd)
        {
            throw new NotImplementedException();
        }
    }
}