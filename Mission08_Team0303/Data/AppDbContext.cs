using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Mission08_Team0303.Models;


namespace Mission08_Team0303.Data
{
    // The main database context -- EF uses this to interact with the SQLite database
    public class AppDbContext : DbContext
    {
        // Constructor passes options (like connection string) up to the base DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Represents the TaskItems table in the database
        public DbSet<TaskItem> TaskItems { get; set; }

        // Represents the Categories table in the database
        public DbSet<Category> Categories { get; set; }

        // Seed the database with initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Category data -- these match the required dropdown options
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Home" },
                new Category { CategoryID = 2, CategoryName = "School" },
                new Category { CategoryID = 3, CategoryName = "Work" },
                new Category { CategoryID = 4, CategoryName = "Church" }
            );

            // Seed sample TaskItems to demonstrate all four quadrants
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    TaskItemID = 1,
                    TaskName = "Submit tax return",
                    DueDate = new DateTime(2025, 4, 15),
                    Quadrant = 1,
                    CategoryID = 3,
                    IsCompleted = false
                },
                new TaskItem
                {
                    TaskItemID = 2,
                    TaskName = "Study for final exam",
                    DueDate = new DateTime(2025, 5, 1),
                    Quadrant = 2,
                    CategoryID = 2,
                    IsCompleted = false
                },
                new TaskItem
                {
                    TaskItemID = 3,
                    TaskName = "Reply to group chat",
                    DueDate = null,
                    Quadrant = 3,
                    CategoryID = 1,
                    IsCompleted = false
                },
                new TaskItem
                {
                    TaskItemID = 4,
                    TaskName = "Reorganize bookshelf",
                    DueDate = null,
                    Quadrant = 4,
                    CategoryID = 1,
                    IsCompleted = false
                }
            );
        }
    }
}