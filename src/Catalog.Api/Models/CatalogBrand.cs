using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Catalog.Api.Models
{
    public class CatalogBrand
    {
        public int Id { get; private set; }
        
        [Required(ErrorMessage = "Brand is required.")]
        public string? Brand { get; set; }

        [Required(ErrorMessage = "TypeId is required.")]
        public int TypeId { get; set; }
    }
}