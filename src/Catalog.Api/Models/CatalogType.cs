using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Catalog.Api.Models
{
    public class CatalogType
    {
        public int Id { get; private set; }
        [Required(ErrorMessage = "Type is required.")]
        public string? Type { get; set; }   
    }
}