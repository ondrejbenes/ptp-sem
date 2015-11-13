using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sem_Benes.Model;

namespace Sem_Benes.API
{
    class UserServiceImpl : UserService
    {
        ICommonDAO<User> Dao;

        public UserServiceImpl(ICommonDAO<User> Dao)
        {
            this.Dao = Dao;
        }

        public IEnumerable<User> FindAllUsers()
        {
            return Dao.FindAll();
        }

        public User FindUser(long UserId)
        {
            return Dao.Find(UserId);
        }

        public User RemoveUser(User User)
        {
            return Dao.Remove(User);
        }

        public User SaveUser(User User)
        {
            return Dao.Save(User);
        }
    }
}
