using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskifyAPI.Data;
using TaskifyAPI.Services.CommentsService;
using TaskifyAPI.Services.ProjectsService;
using TaskifyAPI.Services.TasksService;
using TaskifyAPI.Services.UsersService;

namespace TaskifyAPI.Models.Entities
{
    public class SeedData
    {
        private readonly AppDbContext context;

        public SeedData(AppDbContext _context)
        {
            context = _context;
        }
        public void Initialize()
        {
            SeedRoles();
        }

        private void SeedRoles()
        {
            // Verificam daca in baza de date exista cel putin rol
            // insemnand ca a fost rulat codul
            // De aceea facem return pentru a nu insera inca o data
            // Acesta metoda trebuie sa se execute o singura data
            if (context.Roles.Any())
            {
                return; // baza de date contine deja roluri
            }
            // CREAREA ROLURILOR IN BD
            // daca nu contine roluri, acestea se vor crea
            context.Roles.AddRange(
                new IdentityRole { Id = "2c5e174e-3b0e-446f-86af483d56fd7210", Name = "Admin", NormalizedName = "Admin".ToUpper() },
                new IdentityRole { Id = "2c5e174e-3b0e-446f-86af483d56fd7212", Name = "User", NormalizedName = "User".ToUpper() }
            );
            // o noua instanta pe care o vom utiliza pentru crearea parolelor utilizatorilor
            // parolele sunt de tip hash
            var hasher = new PasswordHasher<ApplicationUser>();
            // CREAREA USERILOR IN BD
            // Se creeaza cate un user pentru fiecare rol
            context.Users.AddRange(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                    FirstName = "MyAdmin",
                    LastName = "Admin",
                    UserName = "admin@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "ADMIN@TEST.COM",
                    Email = "admin@test.com",
                    NormalizedUserName = "ADMIN@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Admin1!")
                },
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb2",
                    FirstName = "MyUser",
                    LastName = "User",
                    UserName = "user@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "USER@TEST.COM",
                    Email = "user@test.com",
                    NormalizedUserName = "USER@TEST.COM",
                    PasswordHash = hasher.HashPassword(null,
                "User1!")
                }
            );
            // ASOCIEREA USER-ROLE
            context.UserRoles.AddRange(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                },
               new IdentityUserRole<string>
               {
                   RoleId = "2c5e174e-3b0e-446f-86af483d56fd7212",
                   UserId = "8e445865-a24d-4543-a6c6-9443d048cdb2"
               }
            );
            context.SaveChanges();
        }
    }
}
