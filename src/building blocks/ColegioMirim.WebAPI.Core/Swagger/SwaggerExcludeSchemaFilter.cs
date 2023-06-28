using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ColegioMirim.WebAPI.Core.Swagger
{
    public class SwaggerExcludeSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
                return;

            var excludedProperties = context.Type
                .GetProperties()
                .Where(t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null);

            foreach (var excludedProperty in excludedProperties)
            {
                var keys = schema.Properties.Keys
                    .Where(c => string.Equals(c, excludedProperty.Name, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();

                foreach (var key in keys)
                    schema.Properties.Remove(key);
            }
        }
    }
}
