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
                try
                {
                    cmd.Transaction = Connection.BeginTransaction();
                    InsertCommandParameters(entity, cmd, out SqlParameter newId);
                    cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();
                    return long.Parse(newId.Value.ToString());
                }
                catch (Exception e)
                {
                    cmd.Transaction?.Rollback();
                    return 0;
                }
            }
        }

        public int Delete(long id)
        {
            var rowsAffected = 0;

            using (var cmd = Connection.CreateCommand())
            {
                try
                {
                    cmd.Transaction = Connection.BeginTransaction();
                    DeleteCommandParameters(id, cmd);
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();
                }
                catch (Exception e)
                {
                    cmd.Transaction?.Rollback();
                }
            }
            return rowsAffected;
        }

        public int Update(T newEntity)
        {
            var rowsAffected = 0;

            using (var cmd = Connection.CreateCommand())
            {
                try
                {
                    cmd.Transaction = Connection.BeginTransaction();
                    UpdateCommandParameters(newEntity, cmd);
                    rowsAffected = cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();
                }
                catch (Exception e)
                {
                    Connection.FireInfoMessageEventOnUserErrors = true;
                    cmd.Transaction?.Rollback();
                }
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
        protected abstract void DeleteCommandParameters(long id, SqlCommand cmd);
        protected abstract T GetEntityFromReader(long id, SqlCommand cmd);
        protected abstract IEnumerable<T> GetAllEntitiesFromReader(SqlCommand cmd);

        public void Dispose()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }
    }
}