using ApiForTesting.Data;
using ApiForTesting.Service;
using ApiForTesting.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connectionStrings = "server=VM-104; database=policyadminsystem;User Id=poladminsys;Password=poladmin1234";
builder.Services.AddDbContext<ApiDbContext>(options => options.UseMySql(connectionStrings, new MySqlServerVersion(new System.Version(8, 0, 22))));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//dependency injection
builder.Services.AddScoped<IUserType, UserTypeRepository>();
builder.Services.AddScoped<IAppUser, AppUserRepository>();
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

app.Run();
