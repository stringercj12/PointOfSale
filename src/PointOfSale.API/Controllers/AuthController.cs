using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.DTOs.Request.Login;
using PointOfSale.Application.DTOs.Response.Login;
using PointOfSale.Application.Services.Users.Login;

namespace PointOfSale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> Login([FromServices] IUserLoginService loginUseCase, [FromBody] LoginRequest request)
        {
            var response = await loginUseCase.ExecuteAsync(request);
            return Ok(response);
        }
    }
}

