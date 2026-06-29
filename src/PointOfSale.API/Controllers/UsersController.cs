using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.DTOs.Request.Users;
using PointOfSale.Application.DTOs.Response.Users;
using PointOfSale.Application.Services.Users.Create;
using PointOfSale.Application.Services.Users.Delete;
using PointOfSale.Application.Services.Users.Get;
using PointOfSale.Application.Services.Users.Update;

namespace PointOfSale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserCreateResponse>> CreateUser([FromServices] IUserCreateService userWriteService, [FromForm] UserCreateRequest request)
        {
            var user = await userWriteService.ExecuteAsync(request);

            return Created(string.Empty, user);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromServices] IUserUpdateService userUpdateService, [FromRoute] int userId, [FromBody] UserUpdateRequest request)
        {
            await userUpdateService.ExecuteAsync(request, userId);

            return NoContent();
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
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll([FromServices] IUserReadService userReadService, [FromQuery] bool active)
        {
            IEnumerable<UserResponse> users = await userReadService.GetAllActiveAsync(active);

            if (users == null || !users.Any())
                return NoContent();

            return Ok(users);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([FromServices] IUserDeleteService userReadService, [FromRoute] int userId)
        {
            await userReadService.ExecuteAsync(userId);

            return NoContent();
        }
    }
}
