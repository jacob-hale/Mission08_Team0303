using Microsoft.AspNetCore.Mvc;

namespace Mission08_Team0303.Models
{
    // Represents the Category lookup table (Home, School, Work, Church)
    public class Category
    {
        // Primary key for Category
        public int CategoryID { get; set; }

        // Display name of the category (e.g., "Home", "School")
        public string CategoryName { get; set; } = string.Empty;

        // Navigation property - one category can belong to many tasks
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}