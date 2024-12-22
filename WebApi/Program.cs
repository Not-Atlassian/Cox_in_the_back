using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Repositories;
using WebApi.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder => builder
            .WithOrigins("http://localhost:5173", "https://localhost:5173", "http://192.168.1.138") // Include both HTTP and HTTPS origins
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<JyrosContext>(options =>
{
    options.UseSqlServer(builder.Configuration["JyrosContext"]);
});

builder.Services.AddScoped<IStoryRepository, StoryRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ISprintRepository, SprintRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITeamMemberAvailabilityRepository, TeamMemberAvailabilityRepository>();
builder.Services.AddScoped<IAdjustmentRepository, AdjustmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost3000");
app.UseAuthorization();

app.MapControllers();

app.Run();