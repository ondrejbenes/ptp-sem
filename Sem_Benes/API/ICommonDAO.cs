using System.Collections.Generic;

namespace Sem_Benes.API
{
    interface ICommonDao<T>
    {
        T Find(long id);

        IEnumerable<T> FindAll();

        T Save(T entity);
        
        T Remove(T entity);
    }
}
