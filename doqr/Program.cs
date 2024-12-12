using Application.Interfaces;
using Application.Services;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Substitua pelo domínio do seu front-end
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();  // Habilita o uso de anotações no Swagger, como [SwaggerOperation]
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Doqr-API", Version = "v1" });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");



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
