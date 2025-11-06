using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Backend.Config
{
    public class HideSchemaFilter: IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var toHide = new[] { "Pedido", "Itens" };
            foreach (var schema in toHide)
            {
                swaggerDoc.Components.Schemas.Remove(schema);
            }
        }
    }
}
