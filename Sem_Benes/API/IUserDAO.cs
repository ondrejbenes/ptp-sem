using Sem_Benes.Model;

namespace Sem_Benes.API
{
    interface IUserDao : ICommonDao<User>
    {
        User FindByUsername(string username);
    }
}
