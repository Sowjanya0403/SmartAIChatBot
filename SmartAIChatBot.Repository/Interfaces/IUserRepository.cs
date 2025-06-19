using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAIChatBot.Models;

namespace SmartAIChatBot.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserInfo?> AuthenticateAsync(string username, string password);
    }
}
