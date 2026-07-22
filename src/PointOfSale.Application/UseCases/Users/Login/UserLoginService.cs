using PointOfSale.Application.DTOs.Request.Login;
using PointOfSale.Application.DTOs.Response.Login;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.UseCases.Common.Interfaces;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Users.Login;

public class UserLoginService : IUserLoginService
{
    private readonly ICryptoService _cryptoService;
    private readonly IUserRepository _userRepository;

    public UserLoginService(IUserRepository userRepository, ICryptoService cryptoService)
    {
        _userRepository = userRepository;
        _cryptoService = cryptoService;
    }

    public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
    {
        var user = await _userRepository.FindByEmailOrUsernameAsync(request.Login);
        var isPasswordValid = false;

        if (user is not null) isPasswordValid = _cryptoService.VerifyPassword(request.Password, user.PasswordHash);

        if (user is null || !isPasswordValid) throw new InvalidCredentialsException("Usuário ou senha inválidos.");

        if (!user.Active) throw new AccountDisabledException("Esta conta está desativada. Contate o administrador.");

        var expirationHours = 8;
        var expiresInSeconds = expirationHours * 3600;

        var token = "fdsfsfsfsf";
        //var token = _jwtService.GenerateToken(user, expirationHours);

        return new LoginResponse(
            token,
            "Bearer",
            expiresInSeconds,
            new UserSessionInfo(
                user.Id,
                user.Name,
                user.Email,
                user.Role,
                user.ProfileImage
            )
        );
    }
}