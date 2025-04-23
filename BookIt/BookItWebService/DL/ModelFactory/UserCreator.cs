using BookIt;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace BookItWebService
{
    public class UserCreator : IModelCreator<User>
    {
        public User CreateModel(IDataReader src)
        {
            User user = new User()
            {
                UserName = Convert.ToString(src["UserName"]),
                Password = Convert.ToString(src["Password"]),
                //IsManager = Convert.ToBoolean(src["IsManager"]),
                Description = Convert.ToString(src["Description"]),
                Email = Convert.ToString(src["Email"]),
                Id = Convert.ToString(src["UserID"])
            };
            return user;
        }
        
    }
}
