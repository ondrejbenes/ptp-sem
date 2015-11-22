using Sem_Benes.Model;
using System.Collections.Generic;

namespace Sem_Benes.API
{
    interface IUserService
    {
        User FindUserById(long userId);

        User FindUserByUsername(string username);

        IEnumerable<User> FindAllUsers();

        User RemoveUser(User user);

        User SaveUser(User user);
    }
}
