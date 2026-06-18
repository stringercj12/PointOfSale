using PointOfSale.Communication.Request.User;
using PointOfSale.Communication.Response.User;

namespace PointOfSale.Application.Users.Create
{
    public interface IUserCreateService
    {
        Task<UserCreateResponse> CreateUserAsync(UserCreateRequest request);
    }
}
