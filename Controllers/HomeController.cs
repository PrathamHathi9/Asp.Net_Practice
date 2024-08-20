using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotNet.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Collections.Generic;

namespace dotNet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // Sending data with Content Result method
    public ActionResult GetEmpName(int EmpId)
    {
        var employees = new[]
        {
            new { EmpId = 1 , EmpName = "Scott" , Salary = 20000 },
            new { EmpId = 2 , EmpName = "Smith" , Salary = 24000 },
            new { EmpId = 3 , EmpName = "Allen" , Salary = 22000 },
        };

        string matchEmpName = " ";
        foreach (var item in employees)
        {
            if (item.EmpId == EmpId)
            {
                matchEmpName = item.EmpName;
            }
        }
        //return new ContentResult() { Content = matchEmpName , ContentType = "text/plain" };
        var res = Content(matchEmpName, "text/html");
        return res;
    }

    // sending image as response using file result method
    public ActionResult GetImage(int empId)
    {
        string fileLocation = $"uploads\\image{empId}.png";
        byte[] filebytes = System.IO.File.ReadAllBytes(fileLocation); 
        string fileName = $"image{empId}.png";
        return File(filebytes, "image/png");
    }

    // sending url page as response
    public ActionResult GetInstaPage(string id)
    {
        string instaURL = $"https://www.instagram.com/{id}/?hl=en";
        return Redirect(instaURL);
    }

    // creating candidate table
    public ActionResult getCandidate()
    {
        //Console.WriteLine("reached at getCandidate api");
        ViewBag.first_name = "nikunj";
        ViewBag.last_name = "italiya";
        ViewBag.email = "abcd@gmail.com";
        ViewBag.phone_number = "+919876543223";
        ViewBag.gender = "female";
        ViewBag.resume = "image";
        ViewBag.status = "not_joining";
        ViewBag.loop = 5;
        ViewBag.sub = new List<string>() { "nath", "science", "english" };

        return View("candidateView");
    }

    public ActionResult contact()
    {
        ViewBag.tollNo = 123123;
        return View();
    }

}
