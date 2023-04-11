using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskifyAPI.Models.Entities;

namespace TaskifyAPI.Models.DTOs
{
    public class TaskStatusDTO
    {
        public enum TaskStatus
        {
            NotStarted,
            InProgress,
            Completed
        }

        public TaskStatus Status { get; set; } /*Not Started, In Progress, Completed*/

    }
    public class TaskDTO : TaskStatusDTO
    {
        
        [Required(ErrorMessage = "Please insert the task title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please insert the description of this task")]
        [StringLength(200, ErrorMessage = "The task description must have at most 200 characters")]
        public string Description { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public TaskDTO(Models.Entities.Task task)
        {
            Title = task.Title;
            Description = task.Description;
            Status = (TaskStatus) task.Status;
            StartDate = task.StartDate;
            EndDate = task.EndDate;
        }

        public TaskDTO() { }
    }
}
