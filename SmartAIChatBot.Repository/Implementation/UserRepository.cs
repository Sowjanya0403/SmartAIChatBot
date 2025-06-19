using Microsoft.EntityFrameworkCore;
using SmartAIChatBot.Data;
using SmartAIChatBot.Models;
using SmartAIChatBot.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartAIChatBot.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }   

        public async Task<UserInfo?> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
                return null;

            return new UserInfo
            {
                Id = user.UserId,
                Name = user.Name,
                Password = user.Password,
                Role = user.Role
            };
        }
    }
}
