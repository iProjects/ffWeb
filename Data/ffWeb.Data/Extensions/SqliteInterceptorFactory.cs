using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace ffWeb.Data
{
    // 1. The Intercepting Provider Factory (.NET 4.0 Compatible)
    public class SqliteInterceptorFactory : DbProviderFactory
    {
        public static readonly SqliteInterceptorFactory Instance = new SqliteInterceptorFactory();
        private readonly DbProviderFactory _underlyingFactory = DbProviderFactories.GetFactory("System.Data.SQLite");

        public override DbConnection CreateConnection()
        {
            // Forces Enterprise Library to use our custom connection wrapper globally
            return new SqliteInterceptorConnection(_underlyingFactory.CreateConnection());
        }

        public override DbCommand CreateCommand()
        {
            return new SqliteInterceptorCommand(_underlyingFactory.CreateCommand());
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return _underlyingFactory.CreateConnectionStringBuilder();
        }

        public override DbParameter CreateParameter()
        {
            return _underlyingFactory.CreateParameter();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return _underlyingFactory.CreateDataAdapter();
        }

        public override bool CanCreateDataSourceEnumerator
        {
            get { return _underlyingFactory.CanCreateDataSourceEnumerator; }
        }

        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return _underlyingFactory.CreateDataSourceEnumerator();
        }
    }

    // 2. The Intercepting Connection Wrapper (.NET 4.0 Compatible)
    public class SqliteInterceptorConnection : DbConnection
    {
        private readonly DbConnection _innerConnection;

        public SqliteInterceptorConnection(DbConnection innerConnection)
        {
            if (innerConnection == null)
            {
                throw new ArgumentNullException("innerConnection");
            }
            _innerConnection = innerConnection;
        }

        // CRUCIAL FOR DATAADAPTERS: Traps inner command instantiation points
        protected override DbCommand CreateDbCommand()
        {
            return new SqliteInterceptorCommand(_innerConnection.CreateCommand());
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return _innerConnection.BeginTransaction(isolationLevel);
        }

        public override void Close()
        {
            _innerConnection.Close();
        }

        public override void ChangeDatabase(string databaseName)
        {
            _innerConnection.ChangeDatabase(databaseName);
        }

        public override void Open()
        {
            _innerConnection.Open();
        }

        public override string ConnectionString
        {
            get { return _innerConnection.ConnectionString; }
            set { _innerConnection.ConnectionString = value; }
        }

        public override string Database
        {
            get { return _innerConnection.Database; }
        }

        public override ConnectionState State
        {
            get { return _innerConnection.State; }
        }

        public override string DataSource
        {
            get { return _innerConnection.DataSource; }
        }

        public override string ServerVersion
        {
            get { return _innerConnection.ServerVersion; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _innerConnection != null)
            {
                _innerConnection.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    // 3. The Intercepting Command Wrapper (.NET 4.0 Compatible & Aggressive)
    public class SqliteInterceptorCommand : DbCommand
    {
        private readonly DbCommand _innerCommand;

        public SqliteInterceptorCommand(DbCommand innerCommand)
        {
            if (innerCommand == null)
            {
                throw new ArgumentNullException("innerCommand");
            }
            _innerCommand = innerCommand;
        }

        // 1. Intercept ExecuteScalar (Fixes UserId retrieval during initialization and login)
        public override object ExecuteScalar()
        {
            _innerCommand.CommandText = FilterDboSchema(_innerCommand.CommandText);
            object result = _innerCommand.ExecuteScalar();

            // If SQLite returns a 64-bit long, convert it to an int for WebSecurity compatibility
            if (result is long)
            {
                return Convert.ToInt32(result);
            }

            return result;
        }

        // 2. Intercept Data Readers (Fixes columns like UserId being read out as long)
        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            _innerCommand.CommandText = FilterDboSchema(_innerCommand.CommandText);
            DbDataReader reader = _innerCommand.ExecuteReader(behavior);

            // Wrap the reader to convert long database types to int on the fly
            return new SqliteInterceptorDataReader(reader);
        }

        public override string CommandText
        {
            get { return _innerCommand.CommandText; }
            set { _innerCommand.CommandText = FilterDboSchema(value); }
        }

        private string FilterDboSchema(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            // Cleans "dbo." and "[dbo]." out case-insensitively
            return Regex.Replace(text, @"\[?dbo\]?\.", "", RegexOptions.IgnoreCase);
        }
         
        public override int ExecuteNonQuery()
        {
            _innerCommand.CommandText = FilterDboSchema(_innerCommand.CommandText);
            return _innerCommand.ExecuteNonQuery();
        }
         
        public override int CommandTimeout
        {
            get { return _innerCommand.CommandTimeout; }
            set { _innerCommand.CommandTimeout = value; }
        }

        public override void Prepare()
        {
            _innerCommand.CommandText = FilterDboSchema(_innerCommand.CommandText);
            _innerCommand.Prepare();
        }

        public override void Cancel()
        {
            _innerCommand.Cancel();
        }

        protected override DbParameter CreateDbParameter()
        {
            return _innerCommand.CreateParameter();
        }

        public override CommandType CommandType
        {
            get { return _innerCommand.CommandType; }
            set { _innerCommand.CommandType = value; }
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get { return _innerCommand.UpdatedRowSource; }
            set { _innerCommand.UpdatedRowSource = value; }
        }

        protected override DbConnection DbConnection
        {
            get { return _innerCommand.Connection; }
            set { _innerCommand.Connection = value; }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { return _innerCommand.Parameters; }
        }

        protected override DbTransaction DbTransaction
        {
            get { return _innerCommand.Transaction; }
            set { _innerCommand.Transaction = value; }
        }

        public override bool DesignTimeVisible
        {
            get { return _innerCommand.DesignTimeVisible; }
            set { _innerCommand.DesignTimeVisible = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _innerCommand != null)
            {
                _innerCommand.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
