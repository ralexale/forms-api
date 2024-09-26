using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace forms_api.Models
{
    [Collection("products")]
    public class Product
    {
        public ObjectId Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TenantId { get; set; }
    }
}
