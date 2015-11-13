using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sem_Benes.Model;

namespace Sem_Benes.API
{
    class CompanyServiceImpl : CompanyService
    {
        ICommonDAO<Company> Dao;

        public CompanyServiceImpl(ICommonDAO<Company> Dao)
        {
            this.Dao = Dao;
        }

        public IEnumerable<Company> FindAllCompanies()
        {
            return Dao.FindAll();
        }

        public Company FindCompany(long CompanyId)
        {
            return Dao.Find(CompanyId);
        }

        public Company RemoveCompany(Company Company)
        {
            return Dao.Remove(Company);
        }

        public Company SaveCompany(Company Company)
        {
            return Dao.Save(Company);
        }
    }
}
