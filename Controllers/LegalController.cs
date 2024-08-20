using Microsoft.AspNetCore.Mvc;
using dotNet.Models;
using dotNet.Data;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace dotNet.Controllers
{
    public class LegalController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LegalController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ViewBag.page = "Index";
            return View();
        }

        public IActionResult About()
        {
            ViewBag.page = "About";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.page = "Contact";
            return View();
        }
        public ActionResult Details()
        {
            List<Product> products = new List<Product>()
            {
                 new Product() {id=1, PName="Ac", Rate=2000 },
                 new Product() {id=2, PName="Refrigerator", Rate=3444.34 }
            };
            ViewBag.product = products;
            ViewBag.page = "Details";
            return View();
        }

        public ActionResult Detail(int id)
        {
            List<Product> products = new List<Product>()
            {
                new Product() {id=1, PName="Ac", Rate=2000 },
                new Product() {id=2, PName="Refrigerator", Rate=3444.34 }
            };
            ViewBag.products = products;
            ViewBag.id = id;
            return View("ProductDetail");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(productViewModel prod)
        {
            var Products = new ProductDataModel
            {
                PName = prod.PName,
                Rate = prod.Rate
            };
            // Console.WriteLine(prod.PName + prod.Rate);
            var l = await dbContext.products.AddAsync(Products);

            await dbContext.SaveChangesAsync();
            Console.WriteLine(l);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        { 
            var products = dbContext.products.ToList();
            Console.WriteLine(products);
            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> displayList(string finder)
        {
            Console.WriteLine(">>>>>>>"+finder);
            var products = dbContext.products.Where(temp => temp.PName==finder).ToList();
            Console.WriteLine("...................."+products);
            return View("List", products);
        }

        public ActionResult GetFormUpdate(Guid id, string PName, int Rate)
        {
            Console.WriteLine(id);
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> GetFormUpdate(Guid id, productViewModel p)
        {
            Console.WriteLine("consoling the id for update " + id);
            var product = await dbContext.products.FindAsync(id);
            Console.WriteLine("....." + product);
   
            if(product is not null)
            {
                product.PName = p.PName;
                product.Rate = p.Rate;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult DeleteData(Guid id)
        {
            // Find the product by ID
            /*var isId = dbContext.products.Where(temp => temp.id == id).FirstOrDefault();

            // Check if the product exists
            if (isId == null)
            {
                Console.WriteLine("Product not found: " + id);
                return RedirectToAction("List"); // Or handle the error appropriately
            }

            // Remove the product and save changes
            dbContext.products.Remove(isId);
            dbContext.SaveChanges(); // Using synchronous SaveChanges

            return RedirectToAction("List");*/
            Console.WriteLine("id=========" + id);
            var data = dbContext.products.Find(id);
            var Data = dbContext.products.Where(temp => temp.id == id).FirstOrDefault();
            Console.WriteLine("data.........."+ data);
            Console.WriteLine("Data==========="+ Data);

            if (data is not null)
            {
                dbContext.products.Remove(data);
                dbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }

    }
}
