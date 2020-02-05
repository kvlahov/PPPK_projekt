using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPPK.DAL.Implementations
{
    public class SqlConnectionGenericRepository<T> : IRepository<T> where T : class, new()
    {
        public SqlConnection Connection { get; private set; }
        public SqlConnectionGenericRepository(SqlConnection connection)
        {
            Connection = connection;
        }

        public long Add(T entity)
        {
            using (var cmd = Connection.CreateCommand())
            {
                InsertCommandParameters(entity, cmd, out SqlParameter newId);
                cmd.ExecuteNonQuery();                
                return long.Parse(newId.Value.ToString());
            }
        }

        public int Delete(T entity)
        {
            var rowsAffected = 0;

            using (var cmd = Connection.CreateCommand())
            {
                DeleteCommandParameters(entity, cmd);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        public int Update(T newEntity)
        {
            var rowsAffected = 0;

            using (var cmd = Connection.CreateCommand())
            {
                UpdateCommandParameters(newEntity, cmd);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        public IEnumerable<T> GetAll()
        {
            using (var cmd = Connection.CreateCommand())
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return GetAllEntitiesFromReader(reader);
                }
            }
        }

        public T GetById(long id)
        {
            using (var cmd = Connection.CreateCommand())
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return GetEntityFromReader(id, reader);
                }
            }
        }

        protected virtual void InsertCommandParameters(T entity, SqlCommand cmd, out SqlParameter newId) { newId = new SqlParameter(); }
        protected virtual void UpdateCommandParameters(T entity, SqlCommand cmd) { }
        protected virtual void DeleteCommandParameters(T entity, SqlCommand cmd) { }
        protected virtual T GetEntityFromReader(long id, SqlDataReader reader) { return null; }
        protected virtual IEnumerable<T> GetAllEntitiesFromReader(SqlDataReader reader) { return null; }

        public void Dispose()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }
    }
}