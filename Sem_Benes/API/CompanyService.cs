using Sem_Benes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_Benes.API
{
    interface CompanyService
    {
        Company FindCompany(long CompanyId);

        IEnumerable<Company> FindAllCompanies();

        Company RemoveCompany(Company Company);

        Company SaveCompany(Company Company);
    }
}
