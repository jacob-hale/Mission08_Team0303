using Microsoft.AspNetCore.Mvc;
using Mission08_Team0303.Models;
using System.Diagnostics;

namespace Mission08_Team0303.Controllers
{

    private TaskContext _context;
    public HomeController(TaskContext temp)
    {
        _context = temp;
    }
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // action to get add view
        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View(new TaskItem());
        }

        // action to post new task
        [HttpPost]
        public IActionResult AddTask(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.TaskItems.Add(task);
                _context.SaveChanges();

                return RedirectToAction("Matrix", task);
            }
            else
            {
                ViewBag.Categories = _context.Categories
                    .OrderBy(x => x.CategoryName)
                    .ToList();
                return View(task);
            }
                
        }

        // action to get the view all tasks
        [HttpGet]
        public IActionResult Matrix()
        {
            var taskItems = _context.TaskItems
                .Include(x => x.Category)
                .OrderBy(x => x.LastName)
                .ToList();

            return View(taskItems);
        }

        // action to get add view to edit
        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var recordToEdit = _context.TaskItems
                .Single(x => x.TaskItemId == id);
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddTask", recordToEdit);
        }

        // action to post edit
        [HttpPost]
        public IActionResult EditTask(TaskItem updatedInfo)
        {
            _context.TaskItems.Update(updatedInfo);
            _context.SaveChanges();

            return RedirectToAction("Matrix");
        }

        // action to get delete view
        [HttpGet]
        public IActionResult DeleteTask(int id)
        {
            var recordToDelete = _context.TaskItems
                    .Single(x => x.TaskItemId == id);
            return View(recordToDelete);
        }

        // action to post delete
        [HttpPost]
        public IActionResult DeleteTask(TaskItem taskItem)
        {
            _context.TaskItems.Remove(taskItem);
            _context.SaveChanges();

            return RedirectToAction("Matrix");
        }
    }
}
