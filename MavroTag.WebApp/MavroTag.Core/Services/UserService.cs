using MavroTag.Core.Domain;
using MavroTag.Core.Interfaces;
using MavroTag.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll().Include(c => c.Permissions).AsEnumerable();
        }

        public User GetById(long id)
        {
            var user = _userRepository.GetAll().FirstOrDefault(c => c.Id == id);
            return user != null ? GetByName(user.Name) : null;
        }

        public User GetByName(string name)
        {
            return _userRepository.GetAll().Include(c => c.Permissions).FirstOrDefault(c => string.Compare(c.Name, name, true) == 0);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public User Add(User user)
        {
            return _userRepository.Insert(user);
        }

        public User Login(string name, string passphrase)
        {
            var user = _userRepository.GetAll().FirstOrDefault(c => c.Name == name && c.Passphrase == passphrase && c.IsEnabled);
            return user != null ? GetByName(user.Name) : null;
        }

        public void Delete(long id)
        {
            _userRepository.Delete(id);
        }
    }
}
