using forms_api.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace forms_api.Services
{
    public class TenantService
    {
        private readonly AppDbContext _context;

        public TenantService(AppDbContext context)
        {
            _context = context;
        }

        // Add a new tenant
        public async Task<Tenant> AddTenantAsync(Tenant tenant)
        {
            await _context.Tenants.AddAsync(tenant);
            await _context.SaveChangesAsync();
            return tenant;
        }

        // Get tenant by ID
        public async Task<Tenant> GetTenantByIdAsync(ObjectId tenantId)
        {
            return await _context.Tenants.FirstAsync(t => t.Id == tenantId);
        }

        // Add a form to a tenant
        public async Task<Form> AddFormToTenantAsync(ObjectId tenantId, Form form)
        {
            var tenant = await GetTenantByIdAsync(tenantId);
            if (tenant == null)
                throw new Exception("Tenant not found");

            tenant.Forms.Add(form);
            await _context.SaveChangesAsync();
            return form;
        }

        // Get all tenants
        public async Task<List<Tenant>> GetAllTenantsAsync()
        {
            return await _context.Tenants.ToListAsync();
        }

        // Add field data to a form for a specific tenant
        public async Task<FormField> AddFieldDataAsync(ObjectId tenantId, string formId, FormField fieldData)
        {
            var tenant = await GetTenantByIdAsync(tenantId);
            if (tenant == null)
                throw new Exception("Tenant not found");

            var form = tenant.Forms.FirstOrDefault(f => f.Id == formId);
            if (form == null)
                throw new Exception("Form not found");

            form.Fields.Add(fieldData);
            await _context.SaveChangesAsync();
            return fieldData;
        }
    }
}
