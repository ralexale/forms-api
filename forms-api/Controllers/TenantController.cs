using forms_api.Models;
using forms_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace forms_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly TenantService _tenantService;

        public TenantController(TenantService tenantService)
        {
            _tenantService = tenantService;
        }

        // GET: api/tenants
        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            var tenants = await _tenantService.GetAllTenantsAsync();
            return Ok(tenants);
        }

        // GET: api/tenants/{tenantId}
        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetTenantById(ObjectId tenantId)
        {
            var tenant = await _tenantService.GetTenantByIdAsync(tenantId);
            if (tenant == null) return NotFound("Tenant not found");
            return Ok(tenant);
        }

        // POST: api/tenants
        [HttpPost]
        public async Task<IActionResult> AddTenant(Tenant tenant)
        {
            var newTenant = await _tenantService.AddTenantAsync(tenant);
            return CreatedAtAction(nameof(GetTenantById), new { tenantId = newTenant.Id }, newTenant);
        }

        // POST: api/tenants/{tenantId}/forms
        [HttpPost("{tenantId}/forms")]
        public async Task<IActionResult> AddFormToTenant(ObjectId tenantId, Form form)
        {
            try
            {
                var newForm = await _tenantService.AddFormToTenantAsync(tenantId, form);
                return Ok(newForm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/tenants/{tenantId}/forms/{formId}/fields
        [HttpPost("{tenantId}/forms/{formId}/fields")]
        public async Task<IActionResult> AddFieldData(ObjectId tenantId, string formId, FormField fieldData)
        {
            try
            {
                var newField = await _tenantService.AddFieldDataAsync(tenantId, formId, fieldData);
                return Ok(newField);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
