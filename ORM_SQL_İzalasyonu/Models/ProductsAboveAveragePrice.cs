using System;
using System.Collections.Generic;

namespace ORM_SQL_İzalasyonu.Models
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
    }
}
