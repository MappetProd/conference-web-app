using Dapper;
using Microsoft.Data.Sqlite;
using Dapper.Contrib;
using System.Data;
using System.Configuration;
using Dapper.Contrib.Extensions;

namespace ConferenceWebApp.Models
{
    public class SQliteDatabase : IDatabase
    {
        //private static readonly object spinlock = new object();

        private SqliteConnection connection;

        public SQliteDatabase(string connectionString) 
        {
            using (connection = new SqliteConnection(connectionString))
            {
                connection.Open();
            }            
        }

        /*public static SQliteDatabase Instance
        {
            get
            {
                lock (spinlock)
                {
                    if (instance == null)
                    {
                        instance = new SQliteDatabase(connectionString);
                    }
                }
                return instance;
            }
        }*/

        public bool UpdateEntry<T>(T entity) where T : class
        {
            return connection.Update<T>(entity);
        }
        public bool InsertEntry<T>(T entity) where T : class
        {
            long identity = connection.Insert<T>(entity);
            return true;
        }
        public int CreateTable<T>(string tableName) where T : class
        {
            return connection.Execute($"CREATE TABLE {@tableName}");
        }
        public bool DeleteEntry<T>(T entity) where T : class
        {
            return connection.Delete<T>(entity);
        }
        public bool DeleteAll<T>() where T : class 
        {
            return connection.DeleteAll<T>();
        }

        public List<T> RetrieveAll<T>() where T : class 
        {
            return connection.GetAll<T>().ToList();
        }
        
        public List<T> ExecuteQuery<T>(string queryStr) where T : class
        {
            return connection.Query<T>(queryStr).ToList();
        }
    }
}
