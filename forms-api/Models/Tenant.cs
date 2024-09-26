using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace forms_api.Models
{

    public class Tenant
    {
        public ObjectId Id { get; set; }
        public string TenantId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public ICollection<Form> Forms { get; set; } = new List<Form>();

    }
}
