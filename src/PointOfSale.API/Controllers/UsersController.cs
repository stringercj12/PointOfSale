using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.Users.Create;
using PointOfSale.Communication.Request.User;
using PointOfSale.Communication.Response.User;

namespace PointOfSale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserCreateResponse>> CreateUser([FromServices] IUserCreateService userCreateService, [FromForm] UserCreateRequest request)
        {
            var user = await userCreateService.CreateUserAsync(request);

            return Created(string.Empty, user);
        }
    }
}
