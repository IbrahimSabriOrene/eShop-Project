using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Catalog.Api.Models
{
    public class CatalogType
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        public string? Type { get; set; }   
    }
}