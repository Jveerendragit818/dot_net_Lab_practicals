using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using practical_6dotnet.Models;

namespace practical_6dotnet.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();
            Product p1 = new Product();

            p1.Id = 1;
            p1.Name = "laptop";
            p1.Category = "Electronics";
            p1.Price = 550000;
            p1.Description = "Hp Laptop";

            Product p2 = new Product();

            p2.Id = 2;
            p2.Name = "Mobile";
            p2.Category = "Electronics";
            p2.Price = 25000;
            p2.Description = "Samsung Mobile";


            Product p3 = new Product();

            p3.Id = 3;
            p3.Name = "Chair";
            p3.Category = "Furniture";
            p3.Price =7500;
            p3.Description = "office chair";


            products.Add(p1);
            products.Add(p2);
            products.Add(p3);

            return View(products);
        }
    }
}