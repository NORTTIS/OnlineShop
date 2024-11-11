using System;
using System.Collections.Generic;

namespace OnlineShop.Models;

public partial class Account
{
    public Account()
    {
    }

    public Account(string username, string password, string? address, string email, string? phoneNumber, string role)
    {
        Username = username;
        Password = password;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        Role = role;
    }

    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Order> Order { get; set; } = new List<Order>();
}
