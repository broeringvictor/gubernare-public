using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Gubernare.Api.Extensions;

public abstract class DescriptionSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
            foreach (var prop in context.Type.GetProperties())
            {
                var descriptionAttribute = prop.GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null)
                {
                    // Verifica se existe a propriedade no schema
                    var schemaProperty = schema?.Properties?
                        .FirstOrDefault(x => x.Key.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)).Value;

                    if (schemaProperty != null)
                    {
                        schemaProperty.Description = descriptionAttribute.Description;
                    }
                }
            }
        }
    }

