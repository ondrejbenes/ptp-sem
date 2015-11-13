using Sem_Benes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_Benes.API
{
    class CompanyMemoryDAO : ICommonDAO<Company>, IDisposable
    {
        private static readonly string fileName = "companies.bin";

        private Dictionary<long, Company> Companies;

        public CompanyMemoryDAO()
        {
            Companies = loadCompaniesFromFile();
        }

        private Dictionary<long, Company> loadCompaniesFromFile()
        {
            throw new NotImplementedException();
        }

        private void saveCompaniesToFile()
        {
            throw new NotImplementedException();
        }

        public Company Find(long Id)
        {
            if (Companies.ContainsKey(Id))
                return Companies[Id];
            else
                return null;
        }

        public IEnumerable<Company> FindAll()
        {
            return new List<Company>(Companies.Values);
        }

        public Company Remove(Company Entity)
        {
            if (Companies.ContainsKey(Entity.Id))
            {
                var ret = Companies[Entity.Id];
                Companies.Remove(ret.Id);
                return ret;
            }
            else
                return null;
        }

        public Company Save(Company Entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            saveCompaniesToFile();
        }
    }
}
