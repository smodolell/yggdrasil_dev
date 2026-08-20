using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using System.Collections.Concurrent;
using System.Reflection;
using Yggdrasil.Common.Attributes;
using Yggdrasil.Infrastructure.Persistence;

namespace Yggdrasil.ApiService;

public static class DependencyInjection
{

    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddHttpContextAccessor();

#if (!UseAspire)
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
#endif


        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Error de validación en los parámetros",
                    Detail = "Uno o más parámetros no pudieron ser convertidos al tipo esperado."
                };
                return new BadRequestObjectResult(problemDetails);
            };
            options.SuppressModelStateInvalidFilter = true;
        });
        builder.Services.AddEndpointsApiExplorer();

        // CONFIGURACIÓN OPENAPI PARA SCALAR
        builder.Services.AddOpenApi(options =>
        {
            // Transformer 1: Seguridad JWT
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                const string schemeName = "Bearer";
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                var securityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Token de autenticación JWT"
                };

                document.Components.SecuritySchemes.TryAdd(schemeName, securityScheme);

                var reference = new OpenApiSecuritySchemeReference(schemeName);
                var requirement = new OpenApiSecurityRequirement { [reference] = [] };

                if (document.Paths != null)
                {
                    foreach (var operation in document.Paths.Values.SelectMany(v => v.Operations.Values))
                    {
                        operation.Security ??= new List<OpenApiSecurityRequirement>();
                        operation.Security.Add(requirement);
                    }
                }
                return Task.CompletedTask;
            });

            // Transformer 2: Info de la API (Separado, no anidado)
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info.Contact = new() { Name = "Soporte Técnico", Email = "anfexi@soporte.com" };
                document.Info.License = new() { Name = "MIT" };
                return Task.CompletedTask;
            });
        });

        // SwaggerGen se mantiene solo si lo usas para generar los XMLs, 
        // pero Scalar usa lo configurado en AddOpenApi.
        builder.Services.AddSwaggerGen(c =>
        {
            c.SupportNonNullableReferenceTypes();
            //// Usar CamelCase en los esquemas
            c.DescribeAllParametersInCamelCase();

            // Soporte para tipos anulables
            c.UseAllOfToExtendReferenceSchemas();



            var apiXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, apiXml));
        });

        builder.Services.AddFluentValidationRulesToSwagger();
    }


}
