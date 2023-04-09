using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskifyAPI.Models.DTOs;

namespace TaskifyAPI.Models.Entities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please insert the task title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please insert the description of this task")]
        [StringLength(200, ErrorMessage = "The task description must have at most 200 characters")]
        public string Description { get; set; }
        public TaskStatus Status { get; set; } /*Not Started, In Progress, Completed*/


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public enum TaskStatus
        {
            NotStarted,
            InProgress,
            Completed
        }

        public Task(TaskDTO tdto)
        {
            Title = tdto.Title;
            Description = tdto.Description;
            Status = (TaskStatus)tdto.Status;
            StartDate = tdto.StartDate;
            EndDate = tdto.EndDate;
        }

        public Task() { }
    }
}
