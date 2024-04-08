using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace projectC
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public Int16 UnitsInStock { get; set; }
        public Int16 UnitsOnOrder { get; set; }
        public Int16 ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }


    //public class OrderDetailsView
    //{

    //    public int OrderID { get; set; }
    //    public string? ProductName { get; set; }
    //    public decimal UnitPrice { get; set; }
    //    public int Quantity { get; set; }
    //    public decimal Discount { get; set; }

    //}

}
