using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SpaNails.Api.Middlewares
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Agrega cabeceras de seguridad HTTP a todas las respuestas de la página/API
            context.Response.OnStarting(() =>
            {
                // Previene ataques de XSS (Cross-Site Scripting)
                context.Response.Headers.Append("X-Xss-Protection", "1; mode=block");
                
                // Previene Clickjacking evitando que la página se incruste en un iframe
                context.Response.Headers.Append("X-Frame-Options", "DENY");
                
                // Evita que el navegador "adivine" (sniffing) el Content-Type, previniendo ataques maliciosos
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                
                // Oculta información sensible del origen cuando se navega hacia afuera
                context.Response.Headers.Append("Referrer-Policy", "no-referrer");
                
                // Controla qué recursos se pueden cargar (muy importante para la seguridad front-end)
                context.Response.Headers.Append("Content-Security-Policy", "default-src 'self';");

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
