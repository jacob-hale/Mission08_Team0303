using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0303.Models
{
    // Represents a single to-do task entered by the user
    // NOTE: Named "TaskItem" instead of "Task" to avoid conflict with C# System.Threading.Tasks.Task
    public class TaskItem
    {
        // Primary key for TaskItem
        public int TaskItemID { get; set; }

        // The name/description of the task -- REQUIRED
        [Required(ErrorMessage = "Task name is required.")]
        public string TaskName { get; set; } = string.Empty;

        // Optional due date for the task
        public DateTime? DueDate { get; set; }

        // Quadrant number (1-4) -- REQUIRED
        // 1 = Important/Urgent
        // 2 = Important/Not Urgent
        // 3 = Not Important/Urgent
        // 4 = Not Important/Not Urgent
        [Required(ErrorMessage = "Quadrant is required.")]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
        public int Quadrant { get; set; }

        // Foreign key linking to the Category table
        public int? CategoryID { get; set; }

        // Navigation property to the related Category
        public Category? Category { get; set; }

        // Whether the task has been completed (defaults to false)
        public bool IsCompleted { get; set; } = false;
    }
}