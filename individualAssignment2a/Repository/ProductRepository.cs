using individualAssignment2a.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individualAssignment2a.Repository
{
    public class ProductRepository
    {

        public IEnumerable<ProductVM> GetAll()
        {
            IEnumerable<ProductVM> productSummary;
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();

            productSummary =
                  from p in db.Products
                  select new ProductVM()
                  {
                      ProductID = p.productID,
                      ProductName = p.productName,
                      Price = (decimal)p.price

                  };
            return productSummary;
        }

        public ProductVM GetDetail(int id)
        {
            ProductVM CBAVM = new ProductVM();
            shoppingcartfaridehEntities db = new shoppingcartfaridehEntities();

           var query =
                  from p in db.Products
                  where(p.productID ==id)

                  select new
                  {
                      ProductID = p.productID,
                      ProductName = p.productName,
                      Price = (decimal)p.price,                      

                  };
            foreach (var item in query)
            {
                CBAVM.ProductID = item.ProductID;
                CBAVM.ProductName = item.ProductName;
                CBAVM.Price = (decimal)item.Price;
                CBAVM.Quantity = 1;
            }

            return CBAVM;
          
        }
    }

}
