using GameHostsManager.WebApi.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GameHostsManager.WebApi.SwaggerDoc.OperationFilters
{
    public class UserIdentityHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            var parameterSchema = context.SchemaGenerator.GenerateSchema(typeof(Guid), context.SchemaRepository);

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = GameHostsHttpHeaders.UserIdentity,
                In = ParameterLocation.Header,
                Required = true,
                Schema = parameterSchema
            });
        }
    }
}
