using System.Data;

namespace BookItWebService
{
    public interface IDbContext
    {
        void OpenConnection();
        void CloseConnection();

        IDataReader Read(string sql);

        object ReadValue(string sql);

        bool Update(string sql);

        bool Insert(string sql);

        bool Delete(string sql);

        void Commit();

        void Rollback();
    }
}
