using AjaxDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AjaxDemo.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress; Initial Catalog = People; Integrated Security=true;";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPeople()
        {
            var repo = new PersonRepo(_connectionString);
            return Json(repo.GetAll());
        }
        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PersonRepo(_connectionString);
            repo.AddPerson(person);
        }
        [HttpPost]
        public void DeletePerson(int id)
        {
            var repo = new PersonRepo(_connectionString);
            repo.DeletePerson(id);
        }
        public IActionResult GetPerson(int id)
        {
            var repo = new PersonRepo(_connectionString);
            var person = repo.GetPerson(id);
            return Json(person);
        }
        [HttpPost]
        public void EditPerson(Person person)
        {
            var repo = new PersonRepo(_connectionString);
            repo.EditPerson(person);
        }

    }
}