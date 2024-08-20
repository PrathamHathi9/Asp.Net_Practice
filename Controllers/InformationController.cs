using Microsoft.AspNetCore.Mvc;
using dotNet.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dotNet.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([ModelBinder(typeof(InformationBinder))] Information I)
        {
            return View();
        }
    }
}
