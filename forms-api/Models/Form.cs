using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace forms_api.Models
{
    public class Form
    {
        [BsonId]
        public string Id { get; set; } // Unique ID for the form
        public string Name { get; set; }  // Name of the form (e.g., "User Survey")

        public string TenantId { get; set; }
        public List<FormField> Fields { get; set; } = new List<FormField>();
    }
}
