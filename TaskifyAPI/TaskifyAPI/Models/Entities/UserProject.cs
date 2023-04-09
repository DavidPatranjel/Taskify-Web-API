using System.ComponentModel.DataAnnotations.Schema;

namespace TaskifyAPI.Models.Entities
{
    public class UserProject
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }

    }
}
