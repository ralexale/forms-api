namespace forms_api.Config
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantId = context.Request.Headers["X-TenantId"].FirstOrDefault();
            if (string.IsNullOrEmpty(tenantId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Tenant ID is missing");
                return;
            }

            context.Items["TenantId"] = tenantId;
            await _next(context);
        }
    }
}
