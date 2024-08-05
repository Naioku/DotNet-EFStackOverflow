using EFStackOverflow;
using EFStackOverflow.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EFStackOverflowContext>(
    option => option
        .UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<EFStackOverflowContext>();
if (dbContext.Database.GetPendingMigrations().Any())
{
    dbContext.Database.Migrate();
}

DataSeeder.Seed(dbContext);

app.Run();