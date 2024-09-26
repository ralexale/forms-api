using MongoDB.EntityFrameworkCore;

namespace forms_api.Models
{
    [Collection("users")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TenantId { get; set; }
    }
}
