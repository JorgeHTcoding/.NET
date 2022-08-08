using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using University1;
using University1.DataAccess;
using University1.Services;
// 10. User serilog to log events
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 11. Config Serilog

builder.Host.UseSerilog( (hostBuilderCtx, loggerConf) =>
{
    loggerConf
    .WriteTo.Console()
    .WriteTo.Debug()
    .ReadFrom.Configuration(hostBuilderCtx.Configuration);
});



const string CONNECTIONNAME = "UniversityDB3";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);
// LOCALIZATION
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
// 3. Add Context to Services of Builder
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// 7. Add JWT Autorization Service

builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

// 4. Add Custom Services (folder Services)
builder.Services.AddScoped<IStudentServices, StudentService>();
builder.Services.AddScoped<ICourseServices, CourseService>();

// 8. Add authorization policy to our project
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1")); //we coul add this tags to endpoints we want to protect

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 9. Config Swagger to take care of the JWT Autorization
builder.Services.AddSwaggerGen( options =>
{
    //We define the security for authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{}
    }
    });
});

// 5. CORS configuration - Antes de empezar el builder.Build inicializamos la política tipo CORS
// que nos permite controlar quienes pueden realizar peticiones, desde que entornos, que tipos de métodos y que tipo de cabeceras pueden enviar
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});
var app = builder.Build();

// 2. SUPPORTED LANGUAGES

var supportedCultures = new[] { "en-US", "es-ES", "fr-FR" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0]) // english by default
    .AddSupportedCultures(supportedCultures)// Add all supported cultures
    .AddSupportedUICultures(supportedCultures);// Ad supported cultures to UI

// 3. ADD LOCALIZATION TO APP
app.UseRequestLocalization(localizationOptions);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 12. Tell app to use Serilog

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 6. Tell our app to use CORS

app.UseCors("CorsPolicy");

app.Run();
