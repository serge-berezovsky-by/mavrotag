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

        public User GetByName(string name)
        {
            return _userRepository.GetAll().Include(c => c.Permissions).FirstOrDefault(c => string.Compare(c.Name, name, true) == 0);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public User Login(string passphrase)
        {
            var user = _userRepository.GetAll().FirstOrDefault(c => c.Passphrase == passphrase);
            return user != null ? GetByName(user.Name) : null;
        }
    }
}
