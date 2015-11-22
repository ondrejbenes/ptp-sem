using Sem_Benes.Model;
using System.Collections.Generic;

namespace Sem_Benes.API
{
    interface ICompanyService
    {
        Company FindCompany(long companyId);

        IEnumerable<Company> FindAllCompanies();

        Company RemoveCompany(Company company);

        Company SaveCompany(Company company);
    }
}
