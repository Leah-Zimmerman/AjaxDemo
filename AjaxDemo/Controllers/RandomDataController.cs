using AjaxDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AjaxDemo.Controllers
{
    public class RandomDataController : Controller
    {
        public static Random _rnd = new Random();
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult GetNumber(int from, int to)
        {
            //var vm = new RandomDataViewModel
            //{
            //    Number = _rnd.Next(1, 1000)
            //};
            return Json(new {Number= _rnd.Next(from, to) });
        }
    }
}
