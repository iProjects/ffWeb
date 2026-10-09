using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Text.RegularExpressions;
using System.Data;


namespace ffWeb.Data
{
    [ConfigurationElementType(typeof(GenericDatabaseData))]
    public class SqliteDatabase : Database
    {
        // Enforces the custom connection interceptor setup
        public SqliteDatabase(string connectionString, DbProviderFactory dbProviderFactory)
            : base(connectionString, SqliteInterceptorFactory.Instance)
        {
        }

        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            throw new NotSupportedException("SQLite does not support stored procedure parameter discovery.");
        }
    }
}
