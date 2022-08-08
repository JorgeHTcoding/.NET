using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ApiModulo9
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;   
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Mi .net api restful",
                Version = description.ApiVersion.ToString(),
                Description = " This is my first API Versioning Control",
                Contact = new OpenApiContact()
                {
                    Email = "loquesea@gmail.com",
                    Name = "Pepin"
                }
            };
            if(description.IsDeprecated)
            {
                info.Description += "This API version has been deprecated";
            }
            return info;           

        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach( var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName,CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}
