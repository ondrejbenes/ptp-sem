using System.Collections.Generic;
using Sem_Benes.Model;

namespace Sem_Benes.API
{
    class CompanyServiceImpl : ICompanyService
    {
        private ICompanyDao _dao;

        public CompanyServiceImpl(ICompanyDao dao)
        {
            _dao = dao;
        }

        public IEnumerable<Company> FindAllCompanies()
        {
            return _dao.FindAll();
        }

        public Company FindCompany(long companyId)
        {
            return _dao.Find(companyId);
        }

        public Company RemoveCompany(Company company)
        {
            return _dao.Remove(company);
        }

        public Company SaveCompany(Company company)
        {
            return _dao.Save(company);
        }
    }
}
