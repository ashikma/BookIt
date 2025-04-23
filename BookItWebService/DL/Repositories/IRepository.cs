namespace BookItWebService
{

    public interface IRepository<T>
    {
        List<T> GetAll();   
        T GetById(string id);
        string GetLast();

        bool Create(T model);
        bool Update(T model);
        bool Delete(string id);
    }
}
