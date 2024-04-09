using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Web.Models.Catalog
{
    public class CatalogBrandModel
    {
        public int Id { get; set; }

        public string? Brand { get; set; }

        public int TypeId { get; set; }
    }
}