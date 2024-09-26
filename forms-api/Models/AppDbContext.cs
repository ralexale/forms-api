using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System;

namespace forms_api.Models
{
    public class AppDbContext: DbContext
    {
        private readonly string _tenantId;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _tenantId = httpContextAccessor.HttpContext?.Items["TenantId"] as string;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Use tenant-specific collections
            string tenantCollection = $"{_tenantId}_Tenants";

            modelBuilder.Entity<Tenant>().ToCollection(tenantCollection);
            modelBuilder.Entity<Tenant>().HasIndex(t => t.TenantId);

            // Add any global filters or additional indexes as needed
            modelBuilder.Entity<Tenant>().HasQueryFilter(t => t.TenantId == _tenantId);
        }

    }
}
