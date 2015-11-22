using Sem_Benes.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sem_Benes.API
{
    class UserMemoryDao : IUserDao
    {
        private const string FileName = "users.bin";

        private Dictionary<long, User> _users;

        private static UserMemoryDao _instance;

        public static UserMemoryDao Get()
        {
            return _instance ?? (_instance = new UserMemoryDao());
        }

        private UserMemoryDao()
        {
            LoadFromFile();
        }

        public User Find(long id)
        {
            return _users[id];
        }

        public User FindByUsername(string username)
        {
            try
            {
                return (from entry in _users
                    where entry.Value.Username == username
                    select entry.Value).Single();
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }

        public IEnumerable<User> FindAll()
        {
            return new List<User>(_users.Values);
        }

        public User Remove(User entity)
        {
            if (!_users.ContainsKey(entity.Id)) return null;
            var ret = _users[entity.Id];
            _users.Remove(ret.Id);
            return ret;
        }

        public User Save(User entity)
        {
            if (entity.Id == -1 || !_users.ContainsKey(entity.Id))
            {
                var nextId = _users.Count == 0 ? 0 : _users.Keys.Max() + 1;
                entity.Id = nextId;
                _users.Add(nextId, entity);
            }
            else
                _users[entity.Id] = new User(entity.FirstName, entity.LastName, entity.Password, entity.Role, entity.Username);
            return _users[entity.Id];
        }

        private void LoadFromFile()
        {
            using (Stream stream = File.Open(FileName, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                try
                {
                    _users = (Dictionary<long, User>)formatter.Deserialize(stream);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    _users = new Dictionary<long, User>();
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
                    formatter.Serialize(stream, _users);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                }
            }
        }
    }
}