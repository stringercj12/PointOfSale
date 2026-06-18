using BCrypt.Net;
using PointOfSale.Communication.Enums;

namespace PointOfSale.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public RoleEnum Role { get; private set; }
        public bool Active { get; private set; }
        public string ProfileImage { get; private set; }
        private User() { }

        public User(string name, string username, string email, string clearTextPassword, RoleEnum role, bool active, string profileImagePath)
        {
            Name = name;
            Username = username;
            Email = email;
            Password = BCrypt.Net.BCrypt.EnhancedHashPassword(clearTextPassword);
            Role = role;
            Active = active;
            ProfileImage = profileImagePath;
        }
    }
}
