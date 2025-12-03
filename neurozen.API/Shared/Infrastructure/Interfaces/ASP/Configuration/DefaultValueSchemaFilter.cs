using System.ComponentModel;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace neurozen.API.Shared.Infrastructure.Interfaces.ASP.Configuration;

/// <summary>
/// Schema filter para aplicar valores por defecto de las propiedades en Swagger
/// </summary>
public class DefaultValueSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null)
            return;

        foreach (var property in schema.Properties)
        {
            var propertyInfo = context.Type
                .GetProperties()
                .FirstOrDefault(p => p.Name.Equals(property.Key, StringComparison.OrdinalIgnoreCase));

            if (propertyInfo == null)
                continue;

            // Obtener el atributo DefaultValue si existe
            var defaultValueAttribute = propertyInfo
                .GetCustomAttributes(typeof(DefaultValueAttribute), false)
                .FirstOrDefault() as DefaultValueAttribute;

            if (defaultValueAttribute != null)
            {
                var defaultValue = defaultValueAttribute.Value;
                
                // Asignar el valor por defecto al esquema
                property.Value.Default = defaultValue switch
                {
                    bool boolValue => new OpenApiBoolean(boolValue),
                    int intValue => new OpenApiInteger(intValue),
                    string stringValue => new OpenApiString(stringValue),
                    _ => null
                };
            }
        }
    }
}

