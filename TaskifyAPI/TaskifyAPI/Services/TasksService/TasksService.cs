using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Data;
using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;
using TaskifyAPI.Services.ProjectsService;

namespace TaskifyAPI.Services.TasksService
{
    public class TasksService : GenericService<Models.Entities.Task>, ITasksService
    {
        public TasksService(AppDbContext db) : base(db) { }

        public async Task<List<Models.Entities.Task>> GetTaskOfProject(int projid)
        {
            return await _db.Tasks.Where(a => a.ProjectId == projid).ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsFromTask(int idtask)
        {
            return await _db.Comments.Where(a => a.TaskId == idtask).ToListAsync();
        }
    }
}
