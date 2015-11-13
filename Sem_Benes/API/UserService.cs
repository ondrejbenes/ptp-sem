using Sem_Benes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_Benes.API
{
    interface UserService
    {
        User FindUser(long CompanyId);

        IEnumerable<User> FindAllUsers();

        User RemoveUser(User User);

        User SaveUser(User User);
    }
}
