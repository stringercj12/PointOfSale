using PointOfSale.Domain.Enums;

namespace PointOfSale.Domain.Entities;

public class User
{
    private User()
    {
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public RoleEnum Role { get; private set; }
    public bool Active { get; private set; }
    public string? ProfileImage { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public static User Create(string name, string username, string email, string profileImagePath, string passwordHash,
        RoleEnum role)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("O nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("O username é obrigatório.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("O e-mail é obrigatório.");


        return new User
        {
            Name = name,
            Username = username,
            Email = email,
            ProfileImage = profileImagePath,
            PasswordHash = passwordHash,
            Role = role,
            Active = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateInfo(string name, string email, RoleEnum role, bool active)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("O nome não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("O e-mail não pode ser vazio.");

        Name = name;
        Email = email;
        Role = role;
        Active = active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("O hash da nova senha não pode ser nulo ou vazio.");

        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfileImage(string? imageUrl)
    {
        ProfileImage = string.IsNullOrWhiteSpace(imageUrl) ? null : imageUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleActiveStatus()
    {
        Active = !Active;
        UpdatedAt = DateTime.UtcNow;
    }
}