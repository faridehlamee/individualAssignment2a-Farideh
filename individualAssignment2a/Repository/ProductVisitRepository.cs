using individualAssignment2a.BusinussLogic;
using individualAssignment2a.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individualAssignment2a.Repository
{
    public class ProductVisitRepository
    {

        public void Add(int id, int Quantity, string sessionid)
        {
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();
            try
            {
                ProductVisit productVisit = new ProductVisit();
                SessionHelper sessionHelper = new SessionHelper();
                sessionid = sessionHelper.SessionID;
             //   Delete(id, sessionid);
                productVisit.productID = id;
                productVisit.sessionID = sessionHelper.SessionID;
                productVisit.updated = DateTime.Now;
                productVisit.qtyOrdered = Quantity;
                db.ProductVisits.Add(productVisit);
                db.SaveChanges();
            }
            catch
            {

            }

        }
        public ProductVM Getdetail(int id)
        {
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();

            try {
                ProductVM CBAVM = new ProductVM();
                var query1 =
                from p in db.Products
                where (p.productID == id)
                select new
                {
                    ProductID = p.productID,
                    ProductName = p.productName,
                    Price = p.price,
                };

                foreach (var item in query1)
                {
                    CBAVM.ProductID = item.ProductID;
                    CBAVM.ProductName = item.ProductName;
                    CBAVM.Price = (decimal)item.Price;
                }
                var query2 =
                from pv in db.ProductVisits
                where (pv.productID == id)
                select new
                {
                    Quantity = pv.qtyOrdered,

                };

                foreach (var item in query2)
                {
                    CBAVM.Quantity = (int)item.Quantity;

                }
                if (CBAVM.Quantity == 0)
                {
                    CBAVM.Quantity = 1;
                }
                else
                {
                    
                }
                return CBAVM;
            }
            catch
                {
                    return null;
                }

        }


        public bool Delete(int id, string sessionid)
        {
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();
            ProductVisit productVisit = db.ProductVisits.Where(pv => pv.productID == id && pv.sessionID == sessionid).FirstOrDefault();
            try
            {
                db.ProductVisits.Remove(productVisit);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            

        }


        public IEnumerable<ProductVisitVM> GetAll()
        {
            IEnumerable<ProductVisitVM> productVisitSummary;
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();

            productVisitSummary =
                  from p in db.Products
                  from pv in p.ProductVisits
                  where(p.productID == pv.productID)
                  select new ProductVisitVM()
                  {
                      ProductID = p.productID,
                      ProductName = p.productName,
                      Price = (decimal)p.price,
                      QtyOrdered = (int)pv.qtyOrdered

                  };
            return productVisitSummary;
        }

    }
}