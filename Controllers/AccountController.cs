using Microsoft.AspNetCore.Mvc;

namespace dotNet.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                return View();
            }
            else
            {
                Console.WriteLine("going to invalid login method");
                return RedirectToAction("invalidLogin");
            }
        }

        public ActionResult invalidLogin()
        {
            Console.WriteLine("reached at invalid login page");
            return View();
        }

        public ActionResult contact()
        {
            ViewBag.tollNo = 456456;
            return View();
        }

        public ActionResult Abc()
        {
            return View();
        }
    }
}
