using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPPK.DAL.Implementations.SqlConnections
{
    public abstract class SqlConnectionGenericRepository<T> : IRepository<T> where T : class, new()
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
                return GetAllEntitiesFromReader(cmd);
            }
        }

        public T GetById(long id)
        {
            using (var cmd = Connection.CreateCommand())
            {
                return GetEntityFromReader(id, cmd);

            }
        }

        protected abstract void InsertCommandParameters(T entity, SqlCommand cmd, out SqlParameter newId);
        protected abstract void UpdateCommandParameters(T entity, SqlCommand cmd);
        protected abstract void DeleteCommandParameters(T entity, SqlCommand cmd);
        protected abstract T GetEntityFromReader(long id, SqlCommand cmd);
        protected abstract IEnumerable<T> GetAllEntitiesFromReader(SqlCommand cmd);

        public void Dispose()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }
    }
}