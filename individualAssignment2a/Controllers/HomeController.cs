using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using individualAssignment2a.Repository;
using individualAssignment2a.View_Model;
using individualAssignment2a.BusinussLogic;

namespace individualAssignment2a.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ProductRepository productRepository = new ProductRepository();

            IEnumerable<ProductVM> productVM = productRepository.GetAll();
            int x = productVM.Count();
            return View(productVM);

        }
        [HttpGet]
        public ActionResult Add(int id)
        {
            ProductVisitRepository productVisitRepository = new ProductVisitRepository();
            if(productVisitRepository.Getdetail(id) != null)
            {
                return View(productVisitRepository.Getdetail(id));
            }
            else
            {
                ProductRepository productRepository = new ProductRepository();
                return View(productRepository.GetDetail(id));
            }
            
        }

        [HttpPost]
        public ActionResult Add(int id, int Quantity)
        {
            ProductVisitRepository productVisitRepository = new ProductVisitRepository();
            SessionHelper sessionHelper = new SessionHelper();
            string  sessionid = sessionHelper.SessionID;
            productVisitRepository.Delete(id, sessionid);
            productVisitRepository.Add(id, Quantity, sessionid);
           // return View();
            return RedirectToAction("ViewCart");

        }
        
        public ActionResult ViewCart()
        {
            const int TAX = 7;
            ProductVisitRepository productVisitRepository = new ProductVisitRepository();
            SessionHelper sessionHelper = new SessionHelper();
            string sessionid = sessionHelper.SessionID;
            ViewBag.Subtotal = Decimal.Round(productVisitRepository.GetAll().Sum(p => p.Price * p.QtyOrdered),2);
            ViewBag.TaxRate = TAX;
            ViewBag.Tax = Decimal.Round(ViewBag.SubTotal * TAX / 100,2);
            ViewBag.Total = Decimal.Round(ViewBag.SubTotal + ViewBag.Tax,2);

            return View(productVisitRepository.GetAll());


        }
   //removes session
        public ActionResult Remove(int id, string sessionid)
        {
            ProductVisitRepository productVisitRepository = new ProductVisitRepository();
            SessionHelper sessionHelper = new SessionHelper();
            sessionid = sessionHelper.SessionID;

            if (productVisitRepository.Delete(id, sessionid) == true)
                return RedirectToAction("ViewCart");

            return RedirectToAction("ViewCart");

        }
        public ActionResult Cancel()
        {

            ProductVisitRepository productVisitRepository = new ProductVisitRepository();
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();
            SessionHelper sessionHelper = new SessionHelper();
            string sessionid = sessionHelper.SessionID;
            TempData["msg"] = "<script>alert('Are you sure that you want to remove all items from shopping cart?');</script>";
            if (sessionid != null)
            db.ProductVisits.Where(p => p.sessionID == sessionid).ToList().ForEach(p => db.ProductVisits.Remove(p));
            db.SaveChanges();
            return View();
        }
        public ActionResult ThankYou()
        {
            return View();
        }


    }
}