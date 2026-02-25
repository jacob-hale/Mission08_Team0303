using Mission08_Team0303.Models;

namespace Mission08_Team0303.Repositories
{
    // Interface defining all database operations available for TaskItems
    // The Controller (Role #4) will interact with the database ONLY through this interface
    public interface ITaskRepository
    {
        // Returns all incomplete tasks (used for the Quadrants view)
        IEnumerable<TaskItem> GetAllIncompleteTasks();

        // Returns a single task by its ID (used for Edit)
        TaskItem? GetTaskById(int id);

        // Adds a new task to the database
        void AddTask(TaskItem task);

        // Updates an existing task in the database
        void UpdateTask(TaskItem task);

        // Deletes a task by its ID
        void DeleteTask(int id);

        // Marks a task as completed
        void MarkAsCompleted(int id);

        // Returns all categories (used to populate the dropdown)
        IEnumerable<Category> GetAllCategories();
    }
}