using PointOfSale.Application.DTOs.Request.Login;
using PointOfSale.Application.DTOs.Response.Login;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Services.Common.Interfaces;
using PointOfSale.Infra.Repositories.Interfaces;

namespace PointOfSale.Application.Services.Users.Login
{
    public class UserLoginService : IUserLoginService
    {

        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;

        public UserLoginService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

        public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
        {
            var user = await _userRepository.FindByEmailOrUsernameAsync(request.Login);
            bool isPasswordValid = false;

            if (user is not null)
            {
                isPasswordValid = _cryptoService.VerifyPassword(request.Password, user.PasswordHash);
            }

            if (user is null || !isPasswordValid)
            {
                throw new InvalidCredentialsException("Usuário ou senha inválidos.");
            }

            if (!user.Active)
            {
                throw new AccountDisabledException("Esta conta está desativada. Contate o administrador.");
            }

            var expirationHours = 8;
            var expiresInSeconds = expirationHours * 3600;

            var token = "fdsfsfsfsf";
            //var token = _jwtService.GenerateToken(user, expirationHours);

            return new LoginResponse(
                token,
                TokenType: "Bearer",
                ExpiresIn: expiresInSeconds,
                User: new UserSessionInfo(
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Role,
                    user.ProfileImage
                )
            );
        }
    }
}
