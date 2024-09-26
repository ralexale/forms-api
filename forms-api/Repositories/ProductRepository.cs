using forms_api.Models;
using Microsoft.EntityFrameworkCore;

namespace forms_api.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;
        private readonly string _tenantId;

        public ProductRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _tenantId = httpContextAccessor.HttpContext?.Items["TenantId"] as string;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }



        public async Task<Product> AddProductAsync(Product product)
        {
            product.TenantId = _tenantId;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

    }
}
