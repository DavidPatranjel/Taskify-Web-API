using System.ComponentModel.DataAnnotations.Schema;

namespace TaskifyAPI.Models
{
    public class UserProject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
        public virtual User? User { get; set; }
        public virtual Project? Project { get; set; }
    }
}
