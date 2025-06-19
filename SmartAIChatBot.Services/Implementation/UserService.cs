using SmartAIChatBot.Models;
using SmartAIChatBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAIChatBot.Business.Interfaces;

namespace SmartAIChatBot.Services.Implementation
{
    public class UserService:IUserService
    {
        public IUserManager _userManager;
        public UserService(IUserManager userManager)
        {
            _userManager = userManager;
        }
       async Task<UserInfo?> IUserService.AuthenticateAsync(string username, string password)
        {
            return await _userManager.AuthenticateAsync(username, password);
        }
    }
}
