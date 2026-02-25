using Microsoft.EntityFrameworkCore;
using Mission08_Team0303.Data;
using Mission08_Team0303.Repositories;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------
// Register MVC (Controllers + Views)
// -------------------------------------------------------
builder.Services.AddControllersWithViews();

// -------------------------------------------------------
// Register SQLite database using the connection string
// defined in appsettings.json under "DefaultConnection"
// -------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------------------------------------------
// Register the Repository Pattern (Dependency Injection)
// When any class asks for ITaskRepository, it will receive
// a TaskRepository instance automatically
// -------------------------------------------------------
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

// -------------------------------------------------------
// Standard middleware pipeline
// -------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// -------------------------------------------------------
// Default route -- Role #4 should name their controller
// "TaskController" with a default action of "Index"
// -------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();