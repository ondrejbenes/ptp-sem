using Sem_Benes.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sem_Benes.API
{
    class CompanyMemoryDao : ICompanyDao
    {
        private const string FileName = "companies.bin";

        private Dictionary<long, Company> _companies;

        private static CompanyMemoryDao _instance;

        public static CompanyMemoryDao Get()
        {
            return _instance ?? (_instance = new CompanyMemoryDao());
        }

        private CompanyMemoryDao()
        {
           LoadCompaniesFromFile();
        }

        public Company Find(long id)
        {
            return _companies[id];
        }

        public IEnumerable<Company> FindAll()
        {
            return new List<Company>(_companies.Values);
        }

        public Company Remove(Company entity)
        {
            if (!_companies.ContainsKey(entity.Id)) return null;
            var ret = _companies[entity.Id];
            _companies.Remove(ret.Id);
            return ret;
        }

        public Company Save(Company entity)
        {
            if (entity.Id == -1 || !_companies.ContainsKey(entity.Id))
            {
                var nextId = _companies.Count == 0 ? 0 : _companies.Keys.Max() + 1;
                entity.Id = nextId;
                _companies.Add(nextId, entity);
            }
            else
                _companies[entity.Id] = new Company(entity.Ico, entity.Dic, entity.Address, entity.Name, entity.BusinessType);
            return _companies[entity.Id];
        }

        private void LoadCompaniesFromFile()
        {
            using (Stream stream = File.Open(FileName, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                try
                {
                    _companies = (Dictionary<long, Company>)formatter.Deserialize(stream);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    _companies = new Dictionary<long, Company>();
                }
            }
        }

        public void SaveToFile()
        {
            using (Stream stream = File.Open(FileName, FileMode.Truncate))
            {
                var formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(stream, _companies);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                }
            }
        }
    }
}
