using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Web.Models.Catalog
{
    public class CatalogItemModel
    {
        public int Id { get;  set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? ImageFile { get; set; }
        public decimal Price { get; set; }
        public int? CatalogBrandId { get; set; }
        public int? CatalogTypeId { get; set; }
      
    }
}