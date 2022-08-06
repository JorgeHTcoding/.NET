using Microsoft.EntityFrameworkCore;
using University1.DataAccess;
using University1.Services;

var builder = WebApplication.CreateBuilder(args);

const string CONNECTIONNAME = "UniversityDB3";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3. Add Context to Services of Builder
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();

// 4. Add Custom Services (folder Services)
builder.Services.AddScoped<IStudentServices, StudentService>();
builder.Services.AddScoped<ICourseServices, CourseService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 6. Tell our app to use CORS

app.UseCors("CorsPolicy");

app.Run();
