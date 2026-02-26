using Microsoft.AspNetCore.Mvc;
using Mission08_Team0303.Models;
using Mission08_Team0303.Repositories;
using System.Diagnostics;

namespace Mission08_Team0303.Controllers
{
    public class HomeController : Controller
    {
        private ITaskRepository _repo;
        public HomeController(ITaskRepository temp)
        {
            _repo = temp;
        }
        public IActionResult Index()
        {
            var tasks = _repo.GetAllIncompleteTasks();
            return View(tasks);
        }

        // action to get add view
        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = _repo.GetAllCategories()
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
                _repo.AddTask(task);
                // save changes?

                return RedirectToAction("Matrix", task);
            }
            else
            {
                ViewBag.Categories = _repo.GetAllCategories()
                    .OrderBy(x => x.CategoryName)
                    .ToList();
                return View(task);
            }
                
        }

        // action to get the view all tasks
        [HttpGet]
        public IActionResult Matrix()
        {
            var taskItems = _repo.GetAllIncompleteTasks()
                .ToList();

            return View(taskItems);
        }

        // action to get add view to edit
        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var recordToEdit = _repo.GetAllIncompleteTasks()
                .Single(x => x.TaskItemID == id);
            ViewBag.Categories = _repo.GetAllCategories()
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddTask", recordToEdit);
        }

        // action to post edit
        [HttpPost]
        public IActionResult EditTask(TaskItem updatedInfo)
        {
            if (ModelState.IsValid)
            {
                // Make sure this says UpdateTask, not AddTask!
                _repo.UpdateTask(updatedInfo);

                return RedirectToAction("Matrix");
            }
            // If validation fails, reload the categories and the view
            ViewBag.Categories = _repo.GetAllCategories().ToList();
            return View("AddTask", updatedInfo);
        }

        // action to get delete view
        [HttpGet]
        public IActionResult DeleteTask(int id)
        {
            var recordToDelete = _repo.GetTaskById(id);
            //.Single(x => x.TaskItemID == id);
            if (recordToDelete == null)
            {
                return NotFound();
            }
            return View(recordToDelete);
        }

        // action to post delete
        [HttpPost]
        public IActionResult DeleteTask(TaskItem taskItem)
        {
            _repo.DeleteTask(taskItem.TaskItemID);
                // save changes?

            return RedirectToAction("Matrix");
        }
    }
}
