using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using APIVersionControl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// 1. Add HttpClient to send HttpRequests in controllers
//Sin esto no podríamos hacer estas peticiones
builder.Services.AddHttpClient();

// 2. Add VERSION CONTROL
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

// 3. Add configuration to document versions
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";// 1.0.0, 1.1.0, etc solo se cambia de 1 a 2  (pos 0) cuando haya un cambio disruptio (cambia totalmente el DTO),
    setup.SubstituteApiVersionInUrl = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. Configure options
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();


var app = builder.Build();

// 5.
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

//configure the http request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // 6. Configure Swagger DOCS 

    app.UseSwaggerUI(options =>
    {
        // Usamos esto para mostrar la documentación de cada una de las versiones de nuestra aplicación
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant()
                );
        }
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
