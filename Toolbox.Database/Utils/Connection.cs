using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Database.Utils
{
    public class Connection
    {
        private string _ConnectionString;
        private DbProviderFactory _DbProvider;
        private IDbConnection _DbConnection;

        public Connection(DbProviderFactory dbProvider, string connectionString)
        {
            if(string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            if(dbProvider is null)
            {
                throw new ArgumentNullException(nameof(dbProvider));
            }

            _ConnectionString = connectionString;
            _DbProvider = dbProvider;
        }


        private IDbConnection CreateConnection()
        {
            IDbConnection dbConnection = _DbProvider.CreateConnection();
            dbConnection.ConnectionString = _ConnectionString;
            return dbConnection;
        }

        private bool CheckConnectionIsOpen()
        {
            return _DbConnection != null && _DbConnection.State == ConnectionState.Open;
        }

        private IDbCommand CreateCommand(Command command)
        {
            if(_DbConnection is null)
            {
                throw new Exception("Db Connection is not initialize ! :(");
            }

            IDbCommand cmd = _DbConnection.CreateCommand();

            cmd.CommandText = command.Query;
            cmd.CommandType = command.IsProcedure ? CommandType.StoredProcedure : CommandType.Text;

            foreach (KeyValuePair<string, object> parameter in command.Parameters)
            {
                IDbDataParameter dbParameter = _DbProvider.CreateParameter();
                dbParameter.ParameterName = parameter.Key;
                dbParameter.Value = parameter.Value is null ? DBNull.Value : parameter.Value;

                cmd.Parameters.Add(dbParameter);
            }

            return cmd;
        }


        #region Connection operation
        public void Open()
        {
            if (CheckConnectionIsOpen())
                return;

            _DbConnection = CreateConnection();
            _DbConnection.Open();
        }

        public void Close()
        {
            if(_DbConnection != null)
            {
                _DbConnection.Close();
                _DbConnection.Dispose();
                _DbConnection = null;
            }
        }
        #endregion

        #region Execute SQL
        public TResult ExecuteScalar<TResult>(Command command)
        {
            if(!CheckConnectionIsOpen())
            {
                throw new Exception("Connection to DB not open ! :o");
            }

            using(IDbCommand cmd = CreateCommand(command))
            {
                object result = cmd.ExecuteScalar();

                return (result is DBNull) ?  default(TResult) : (TResult)result;
            }
        }

        public int ExecuteNonQuery(Command command)
        {
            if(!CheckConnectionIsOpen())
            {
                throw new Exception("Connection to DB not open ! :o");
            }

            using(IDbCommand cmd = CreateCommand(command))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> mapper)
        {
            if (!CheckConnectionIsOpen())
            {
                throw new Exception("Connection to DB not open ! :o");
            }

            using(IDbCommand cmd = CreateCommand(command))
            {
                using(IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return mapper(reader);
                    }
                }
            }
        }
        #endregion
    }
}
