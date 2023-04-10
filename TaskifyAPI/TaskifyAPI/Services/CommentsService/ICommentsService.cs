using TaskifyAPI.Models.Entities;
using TaskifyAPI.Services.GenericService;

namespace TaskifyAPI.Services.CommentsService
{
    public interface ICommentsService : IGenericService<Comment>
    {
    }
}
