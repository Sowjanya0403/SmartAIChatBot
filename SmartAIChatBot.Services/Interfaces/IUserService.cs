using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAIChatBot.Models;

namespace SmartAIChatBot.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserInfo?> AuthenticateAsync(string username, string password);
    }
}
