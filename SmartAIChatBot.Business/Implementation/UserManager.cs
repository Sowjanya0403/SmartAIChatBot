using SmartAIChatBot.Business.Interfaces;
using SmartAIChatBot.Models;
using SmartAIChatBot.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAIChatBot.Business.Implementation
{
    public class UserManager : IUserManager
    {
        public IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserInfo?> AuthenticateAsync(string username, string password)
        {
            if(username == null || password == null)
            {
                throw new ArgumentNullException("Username and password cannot be null.");
            }
            return await _userRepository.AuthenticateAsync(username, password);
        }
    }
}
