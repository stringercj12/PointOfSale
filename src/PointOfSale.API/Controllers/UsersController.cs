using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.DTOs.Request.Users;
using PointOfSale.Application.DTOs.Response.Users;
using PointOfSale.Application.Services.Users.Create;
using PointOfSale.Application.Services.Users.Get;

namespace PointOfSale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserCreateResponse>> CreateUser([FromServices] IUserCreateService userCreateService, [FromForm] UserCreateRequest request)
        {
            var user = await userCreateService.CreateUserAsync(request);

            return Created(string.Empty, user);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserCreateResponse>> GetUser([FromServices] IUserReadService userReadService, [FromRoute] int userId)
        {
            var user = await userReadService.FindByIdAsync(userId);

            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll([FromServices] IUserReadService userReadService)
        {
            IEnumerable<UserResponse> users = await userReadService.GetAllActiveAsync();

            if (users == null || !users.Any())
                return NoContent();

            return Ok(users);
        }
    }
}
