using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Catalog.Api.Models
{
    public class CatalogItem
    {
        public int Id { get; private set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Summary is required.")]
        public string? Summary { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "ImageFile is required.")]
        public string? ImageFile { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "CatalogBrandId is required.")]
        public int? CatalogBrandId { get; set; }

        [Required(ErrorMessage = "CatalogTypeId is required.")]
        public int? CatalogTypeId { get; set; }
    }


}