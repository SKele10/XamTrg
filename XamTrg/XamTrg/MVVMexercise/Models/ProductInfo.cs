using System;
using System.Collections.Generic;
using System.Text;

namespace XamTrg.MVVMexercise.Models
{
    public class ProductInfo
    {
        public int ProductRowId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public int BasePrice { get; set; }
    }
}
