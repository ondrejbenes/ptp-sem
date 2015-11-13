using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_Benes.API
{
    interface ICommonDAO<T>
    {
        T Find(long Id);

        IEnumerable<T> FindAll();

        T Save(T Entity);
        
        T Remove(T Entity);
    }
}
