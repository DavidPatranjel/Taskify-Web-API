using TaskifyAPI.Data;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;

namespace TaskifyAPI.Services.UsersService
{
    public class UsersService : GenericService<ApplicationUser>, IUsersService
    {
        public UsersService(AppDbContext db) : base(db) { }

    }
}