using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMA.DAL.Data;
using TMA.DAL.DbModels;
using TMA.DAL.Repository.Interfaces;

namespace TMA.UI.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IGenericRepository<Assignment> _repository;
        private readonly AppDbContext _dbcontext;
        public AssignmentController(IGenericRepository<Assignment> repository, AppDbContext dbcontext)
        {
            _repository = repository;
            this._dbcontext = dbcontext;
        }

        public IActionResult GetList()
        {
            var response = _dbcontext.Assignments.Include(p => p.User).ToList();
            return View(response);
        }
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _dbcontext.Assignments.Include(p => p.User).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public IActionResult Create()
        {
            ViewBag.User = new SelectList(_dbcontext.Users.ToList(), "Id", "Name");
            ViewBag.StatusList = new SelectList(new List<string> { "Active", "Completed" });
            return View();
        }
        [HttpPost]
        public IActionResult Create(Assignment item)
        {
            if (ModelState.IsValid)
            {
                item.CreatedAt = DateTime.UtcNow;
                _repository.AddItem(item);
                return RedirectToAction("GetList");
            }
            else
            {
                ViewBag.User = new SelectList(_dbcontext.Users.ToList(), "Id", "Name");
                ViewBag.StatusList = new SelectList(new List<string> { "Active", "Completed" });
                ViewBag.Error = "Please doublecheck your information";
                return View(item);
            }
        }

        public IActionResult Update(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _dbcontext.Assignments.Include(p => p.User).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            ViewBag.User = new SelectList(_dbcontext.Users.ToList(), "Id", "Name");
            ViewBag.StatusList = new SelectList(new List<string> { "Active", "Completed" });
            return View(response);
        }
        [HttpPost]
        public IActionResult UpdateAssignment(int id, [Bind("Id,Name,Description,Status,CreatedAt,DueDate,UserId")] Assignment item)
        {
            var response = _repository.GetById(id);
            response.Name = item.Name;
            response.Description = item.Description;
            response.Status = item.Status;
            response.CreatedAt = item.CreatedAt;
            response.DueDate = item.DueDate;
            response.UserId = item.UserId;
            _repository.Update(response);
            return RedirectToAction("GetList");
        }


        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _dbcontext.Assignments.Include(p => p.User).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            ViewBag.User = new SelectList(_dbcontext.Users.ToList(), "Id", "Name");
            ViewBag.StatusList = new SelectList(new List<string> { "Active", "Completed" });
            return View(response);
        }
        public IActionResult DeleteAssignment(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("GetList");
        }
        public IActionResult Read(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _dbcontext.Assignments.Include(p => p.User).FirstOrDefault(p => p.Id == id);
            if (response == null)
            {
                return NotFound();
            }
            ViewBag.User = new SelectList(_dbcontext.Users.ToList(), "Id", "Name");
            ViewBag.StatusList = new SelectList(new List<string> { "Active", "Completed" });
            return View(response);
        }

    }
}
