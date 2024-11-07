using Microsoft.EntityFrameworkCore;
using WebApi;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddControllers();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new() { Title = "WebApi", Version = "v1" });
// });

builder.Services.AddDbContext<JyrosDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("JyrosDbContext"));
});

var app = builder.Build();