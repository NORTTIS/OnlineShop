using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DTOs
{
    internal class AccountDTO
    {
        public int AccountId { get; set; }

        public string Username { get; set; } = null!;

        public string? Address { get; set; }

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Role { get; set; } = null!;
    }
}
