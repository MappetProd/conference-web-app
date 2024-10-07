using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ConferenceWebApp.Models
{
    public interface IDatabase
    {
        public List<T> RetrieveAll<T>() where T : class;
        public bool UpdateEntry<T>(T entity) where T : class;
        public bool InsertEntry<T>(T entity) where T : class;
        public int CreateTable<T>(string tableName) where T : class;
        public bool DeleteEntry<T>(T entity) where T : class;
        public bool DeleteAll<T>() where T : class;

    }
}
