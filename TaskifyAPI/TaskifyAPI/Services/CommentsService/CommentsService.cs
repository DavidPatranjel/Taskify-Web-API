using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;
using TaskifyAPI.Services.ProjectsService;

namespace TaskifyAPI.Services.CommentsService
{
    public class CommentsService : GenericService<Comment>, ICommentsService
    {
        public CommentsService(AppDbContext db) : base(db) { }

    }
}

