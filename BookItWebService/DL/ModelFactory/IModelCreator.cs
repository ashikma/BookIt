using System.Data;

namespace BookItWebService
{
    public interface IModelCreator<T>
    {
        T CreateModel(IDataReader model);
    }
}
