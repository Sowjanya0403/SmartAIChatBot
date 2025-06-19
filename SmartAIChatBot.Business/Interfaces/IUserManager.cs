using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAIChatBot.Models;

namespace SmartAIChatBot.Business.Interfaces
{
    public interface IUserManager
    {
        Task<UserInfo?> AuthenticateAsync(string username, string password);
    }
}
