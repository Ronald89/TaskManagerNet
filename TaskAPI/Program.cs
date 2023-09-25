using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4747")
            .WithMethods("POST", "PUT", "DELETE", "GET").AllowAnyHeader();
        });
});

builder.Services.AddDbContext<TaskContext>(opt =>
    opt.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TaskManager;Trusted_Connection=True;MultipleActiveResultSets=true"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();


app.UseRouting();

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
