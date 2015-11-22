using System.Collections.Generic;
using Sem_Benes.Model;

namespace Sem_Benes.API
{
    class UserServiceImpl : IUserService
    {
        IUserDao _dao;

        public UserServiceImpl(IUserDao dao)
        {
            this._dao = dao;
        }

        public IEnumerable<User> FindAllUsers()
        {
            return _dao.FindAll();
        }

        public User FindUserById(long userId)
        {
            return _dao.Find(userId);
        }

        public User FindUserByUsername(string username)
        {
            return _dao.FindByUsername(username);
        }

        public User RemoveUser(User user)
        {
            return _dao.Remove(user);
        }

        public User SaveUser(User user)
        {
            return _dao.Save(user);
        }
    }
}
