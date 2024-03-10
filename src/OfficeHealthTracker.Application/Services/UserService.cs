using System;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Interfaces;
using OfficeHealthTracker.Interfaces.Repository;

namespace OfficeHealthTracker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(User user)
        {
            user.Id = Guid.NewGuid();
            _userRepository.AddUser(user);
        }

    }
}
