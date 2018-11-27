using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsSqlSyncTest.Utils
{
    public static class SqliteFactory
    {
        private const string DbName = "lds.sqlite";
        private const string TableName = "lds_features";

        public static SqliteHandler CreateInstance()
        {
            return new SqliteHandler(DbName, TableName);
        }
    }
}
