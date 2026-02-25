using Microsoft.AspNetCore.Mvc;
using Mission08_Team0303.Models;
using Mission08_Team0303.Repositories;
using System.Diagnostics;

namespace Mission08_Team0303.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        public HomeController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public IActionResult Index()
        {
            var tasks = _taskRepository.GetAllIncompleteTasks();
            return View(tasks);
        }

        // action to get add view
        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = _taskRepository.GetAllCategories()
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
                _taskRepository.AddTask(task);
                // save changes?

                return RedirectToAction("Matrix", task);
            }
            else
            {
                ViewBag.Categories = _taskRepository.GetAllCategories()
                    .OrderBy(x => x.CategoryName)
                    .ToList();
                return View(task);
            }
                
        }

        // action to get the view all tasks
        [HttpGet]
        public IActionResult Matrix()
        {
            var taskItems = _taskRepository.GetAllCategories()
                .ToList();

            return View(taskItems);
        }

        // action to get add view to edit
        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var recordToEdit = _taskRepository.GetAllIncompleteTasks()
                .Single(x => x.TaskItemID == id);
            ViewBag.Categories = _taskRepository.GetAllCategories()
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddTask", recordToEdit);
        }

        // action to post edit
        [HttpPost]
        public IActionResult EditTask(TaskItem updatedInfo)
        {
            _taskRepository.UpdateTask(updatedInfo);
            // save changes?

            return RedirectToAction("Matrix");
        }

        // action to get delete view
        [HttpGet]
        public IActionResult DeleteTask(int id)
        {
            var recordToDelete = _taskRepository.GetTaskById(id);
                    //.Single(x => x.TaskItemID == id);
            return View(recordToDelete);
        }

        // action to post delete
        [HttpPost]
        public IActionResult DeleteTask(TaskItem taskItem)
        {
            _taskRepository.DeleteTask(taskItem.TaskItemID);
                // save changes?

            return RedirectToAction("Matrix");
        }
    }
}
