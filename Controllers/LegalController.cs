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
            await dbContext.products.AddAsync(Products);

            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        { 
            ViewBag.products = dbContext.products.Select(p => p.PName).ToList();
            var products = dbContext.products.ToList();

            foreach(var item in ViewBag.products)
            {
                Console.WriteLine("^&*"+ item);
            }
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> displayList(string finder)
        {
            Console.WriteLine(">>>>>>>"+finder);
            var products = dbContext.products.Where(temp => temp.PName == finder).ToList();
            Console.WriteLine("...................."+products);
            ViewBag.products = null;
            return View("List", products);
        }

        public ActionResult GetFormUpdate(Guid id, string PName, int Rate)
        {
            Console.WriteLine("getFormUpdate : "+ id);
            Console.WriteLine("getFormUpdate : " + Rate);
            Console.WriteLine("getFormUpdate : "+ PName);

            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> GetFormUpdate(Guid id, productViewModel p)
        {
            //Console.WriteLine("consoling the id for update " + id);
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

        [HttpGet]
        public ActionResult ShowCustomer()
        {
            ViewBag.customer = dbContext.products.Select(p => new { p.PName, p.id }).ToList();
            /*foreach (var i in ViewBag.customer)
            {
                Console.WriteLine("customer=========================" + i);
            }*/
            ViewBag.action = "CreateCustomer";

            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(CustomerViewModel customer)
        {
            Console.WriteLine("Name: =================" + customer.Name);
            Console.WriteLine("Phone Number: =========" + customer.phoneNo);
            Console.WriteLine("Product Name: =========" + customer.ProductId);
            var Customer = new CustomerDataModel
            {
                Name = customer.Name,
                phoneNo = customer.phoneNo,
                ProductId = new Guid(customer.ProductId),
            };
            dbContext.customers.Add(Customer);
            dbContext.SaveChanges();
            return RedirectToAction("ShowConsumer");
        }

        [HttpGet]
        public ActionResult ShowConsumer()
        {
            ViewBag.Customer = dbContext.customers.ToList();
            var product = dbContext.customers.Skip(25).Take(5).ToList();

            /*foreach (var i in dbContext.customers)
            {
                Console.WriteLine("id"+i.Id);
                Console.WriteLine("name"+i.Name);
                Console.WriteLine("phone"+i.phoneNo);
                Console.WriteLine("proid"+i.ProductId);
            }*/

            return View(product);
        }

        [HttpPost]
        public ActionResult Filter(string filterOption)
        {
            Console.WriteLine("filter "+ filterOption);
            List<ProductDataModel> product;
            ViewBag.products = dbContext.products.Select(p => p.PName).ToList();
            if (filterOption == "ascending")
            {
                product = dbContext.products.OrderBy(data => data.PName).ToList();
            }
            else
            {
                product = dbContext.products.OrderByDescending(data => data.PName).ToList();
            }

            foreach (var item in product)
            {
                Console.WriteLine("product " + item.PName);
            }

            return View("List", product);
        }

        [HttpGet]
        public ActionResult ConsumerFormUpdate(Guid id, string Name, string phoneNo, Guid productId)
        {
            /*
            Console.WriteLine("id: " + id);  
            Console.WriteLine("id: " + Name);
            Console.WriteLine("id: " + phoneNo);
            Console.WriteLine("id: " + productId);*/
            ViewBag.customer = dbContext.products.Select(t => new {t.id, t.PName}).ToList();
            ViewBag.action = "ConsumerUpdate";
            //var product = dbContext.customers.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult ConsumerUpdate(CustomerViewModel data)
        {
            //var Datas = new data();
            return RedirectToAction("ShowConsumer");
        }
    }
}
