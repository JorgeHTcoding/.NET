using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;


namespace APIVersionControl

{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        //este documento lo unico que hace es darle información a swagger para que se muestre en el swagger docs
        {
            var info = new OpenApiInfo()
            {
                Title = "Mi .net Api restful",
                Version = description.ApiVersion.ToString(),
                Description = " This is my first API Versioning Control",
                Contact = new OpenApiContact()
                {
                    Email = "elquesea@gmail.com",
                    Name = "Juanito"
                }

            };
            if (description.IsDeprecated)
            {
                info.Description += "This API version has been deprecated";
            }
            return info;

        }
        public void Configure(SwaggerGenOptions options)
        {
            //Add swagger Documentation for each API version we have
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }

}

       

  
