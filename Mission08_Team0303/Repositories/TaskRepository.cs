using Microsoft.EntityFrameworkCore;
using Mission08_Team0303.Data;
using Mission08_Team0303.Models;

namespace Mission08_Team0303.Repositories
{
    // Concrete implementation of ITaskRepository using EF Core + SQLite
    public class TaskRepository : ITaskRepository
    {
        // Private reference to the database context
        private readonly AppDbContext _context;

        // AppDbContext is injected by the DI system configured in Program.cs
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        // Fetches all tasks where IsCompleted == false, including their Category
        public IEnumerable<TaskItem> GetAllIncompleteTasks()
        {
            return _context.TaskItems
                .Include(t => t.Category)
                .Where(t => !t.IsCompleted)
                .ToList();
        }

        // Fetches a single task by ID, including its Category
        public TaskItem? GetTaskById(int id)
        {
            return _context.TaskItems
                .Include(t => t.Category)
                .FirstOrDefault(t => t.TaskItemID == id);
        }

        // Adds a new task and saves to the database
        public void AddTask(TaskItem task)
        {
            _context.TaskItems.Add(task);
            _context.SaveChanges();
        }

        // Updates an existing task and saves changes
        public void UpdateTask(TaskItem task)
        {
            _context.TaskItems.Update(task);
            _context.SaveChanges();
        }

        // Finds and removes a task by ID, then saves
        public void DeleteTask(int id)
        {
            var task = _context.TaskItems.Find(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                _context.SaveChanges();
            }
        }

        // Finds a task and sets IsCompleted to true, then saves
        public void MarkAsCompleted(int id)
        {
            var task = _context.TaskItems.Find(id);
            if (task != null)
            {
                task.IsCompleted = true;
                _context.SaveChanges();
            }
        }

        // Returns all categories ordered alphabetically for the dropdown
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(c => c.CategoryName).ToList();
        }
    }
}