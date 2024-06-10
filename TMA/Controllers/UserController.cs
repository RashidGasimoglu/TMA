using Microsoft.AspNetCore.Mvc;
using TMA.DAL.DbModels;
using TMA.DAL.Repository.Interfaces;

namespace TMA.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IGenericRepository<User> _repository;
        public UserController(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<User>> GetList()
        {
            var response = _repository.GetList();
            return View(response);
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _repository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult<User> Create(User item)
        {
            if (ModelState.IsValid)
            {
                _repository.AddItem(item);
                return RedirectToAction("GetList");
            }
            else
            {
                ViewBag.Error = "Please doublecheck your information";
                return View(item);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _repository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        public IActionResult UpdateUser(int id, [Bind("Id,Name,Surname,Email")] User item)
        {
            var response = _repository.GetById(id);
            response.Name = item.Name;
            response.Surname = item.Surname;
            response.Email = item.Email;
            _repository.Update(response);
            return RedirectToAction("GetList");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var response = _repository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }
        public IActionResult DeleteUser(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("GetList");
        }
    }
}
