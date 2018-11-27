using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsSqlSyncTest.Utils
{
    public class SqliteHandler
    {
        public SqliteHandler(string dbName, string tableName)
        {
            this.dbName = dbName;
            this.tableName = tableName;
        }

        public string dbName { get; set; }
        public string tableName { get; set; }

        public SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection("Data Source=" + dbName);
            connection.Open();
            return connection;
        }

        public void CreateTable()
        {
            var query = $"CREATE TABLE IF NOT EXISTS {tableName} (id integer primary key, name TEXT)";
            ExecuteQuery(query);
        }

        public void DropTable()
        {
            var query = $"DROP TABLE {tableName}";
            ExecuteQuery(query);
        }

        public void InsertRandomPerson()
        {
            var random = new Random();
            var randomPeople = new[] { "Hans", "Peter", "Jens", "Maria", "Natascha" };

            var person = randomPeople[random.Next(randomPeople.Count())];

            var query = $"INSERT INTO {tableName}(name) VALUES ('{person}')";
            ExecuteQuery(query);
        }

        public DataTable GetDataTable()
        {
            var query = $"SELECT * FROM {tableName}";

            using (var connection = GetConnection())
            using (var transaction = connection.BeginTransaction())
            using (var adapter = new SQLiteDataAdapter(query, connection))
            {
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public void UpdateDataTable(DataTable table)
        {
            var query = $"SELECT * FROM {tableName}";

            using (var connection = GetConnection())
            using (var transaction = connection.BeginTransaction())
            using (var adapter = new SQLiteDataAdapter(query, connection))
            using (var builder = new SQLiteCommandBuilder(adapter))
            {
                adapter.Update(table);
                transaction.Commit();
            }
        }

        public void DeleteOneRow()
        {
            var query = $"SELECT Id from {tableName} LIMIT 1";
            int id;

            using (var conneciton = GetConnection())
            using (var command = new SQLiteCommand(query, conneciton))
            {
                id = int.Parse(command.ExecuteScalar().ToString());
            }

            var delQuery = $"DELETE FROM {tableName} WHERE Id = {id}";
            ExecuteQuery(delQuery);
        }

        private void ExecuteQuery(string query)
        {
            using (var connection = GetConnection())
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
