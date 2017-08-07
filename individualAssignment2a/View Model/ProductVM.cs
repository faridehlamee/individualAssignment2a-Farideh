using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace individualAssignment2a.View_Model
{
    public class ProductVM
    {
        
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        

        public ProductVM() { }
    }
}