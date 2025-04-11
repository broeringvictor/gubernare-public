using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Gubernare.Api.Extensions;

public static class OpenApiExtensions
{
    public static IHostApplicationBuilder AddDefaultOpenApi(this IHostApplicationBuilder builder)
    {
        // Caso você use ou não versionamento...
        // Se não usar versionamento, basta usar .AddOpenApi("v1", options => { ... })

        builder.Services.AddOpenApi("v1", options =>
        {
            // Você pode criar seus transformers:
            // ex: adicionar transform que insere o Bearer
            options.AddDocumentTransformer<BearingSecurityTransformer>();
        });
        
        return builder;
    }
}

public class BearingSecurityTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Insira o token JWT no formato: Bearer {TOKEN}"
        };

        document.Components ??= new();
        document.Components.SecuritySchemes["Bearer"] = securityScheme;

        return Task.CompletedTask;
    }
}
