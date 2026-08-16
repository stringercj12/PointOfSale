using PointOfSale.Domain.Exceptions;

namespace PointOfSale.Domain.Entities;

public class User
{
    private User()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public Enums.Role Role { get; private set; }
    public bool Active { get; private set; }
    public string? ProfileImage { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public static User Create(string name, string username, string email, string profileImagePath, string passwordHash,
        Enums.Role role)
    {
        ValidateUserState(name, email);

        if (string.IsNullOrWhiteSpace(username)) throw new DomainException("O username é obrigatório.");

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

    public void UpdateInfo(string name, string email, Enums.Role role, bool active)
    {
        ValidateUserState(name, email);

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

    private static void ValidateUserState(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("O nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email)) throw new DomainException("O e-mail é obrigatório.");
    }
}