using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAIChatBot.Data
{
    public partial class User
    {
        public Guid UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
