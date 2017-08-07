using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace individualAssignment2a.View_Model
{
    public class ProductVisitVM
    {
       
        public string SessionID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int QtyOrdered { get; set; }
        public DateTime Updated { get; set; }
        public ProductVisitVM() { }
        

    }
}