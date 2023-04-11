using Microsoft.AspNetCore.Mvc.ActionConstraints;
using TaskifyAPI.Data;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;

namespace TaskifyAPI.Services.UsersService
{
    public class UsersService : GenericService<ApplicationUser>, IUsersService
    {
        public UsersService(AppDbContext db) : base(db) { }
        public async Task<ApplicationUser> GetById(string id)
        {
            return await _db.Users.FindAsync(id);
        }
    }
}