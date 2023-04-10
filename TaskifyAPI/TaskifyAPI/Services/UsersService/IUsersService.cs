using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;

namespace TaskifyAPI.Services.UsersService
{
    public interface IUsersService : IGenericService<ApplicationUser>
    {
    }
}
