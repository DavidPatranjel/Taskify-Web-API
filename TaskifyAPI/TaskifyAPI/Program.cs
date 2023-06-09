using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.UnitOfWorkService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
///builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
///    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                                    options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole> (options => 
                                    options.User.RequireUniqueEmail = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
builder.Services.AddTransient<SeedData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Run seeders
using (var serviceScope = app.Services.CreateScope())
{
    var seeder = serviceScope.ServiceProvider.GetRequiredService<SeedData>();
    seeder.Initialize();
}

app.Run();
