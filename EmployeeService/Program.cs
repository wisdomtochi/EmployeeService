using EmployeeService.Data.Context;
using EmployeeService.DataAccess.Implementation;
using EmployeeService.DataAccess.Interfaces;
using EmployeeService.Services.Implementations;
using EmployeeService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string dbcon = builder.Configuration.GetConnectionString("EmployeeDBConnection");
builder.Services.AddDbContext<EmployeeDbContext>(options =>
{
    options.UseMySql(dbcon, ServerVersion.AutoDetect(dbcon), sqlopt =>
    {
        sqlopt.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), null);
        sqlopt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IUnitofWork<>), typeof(UnitofWork<>));
builder.Services.AddScoped<IEmployeeServiceLogic, EmployeeServiceLogic>();
builder.Services.AddScoped<IConnectionService, ConnectionService>();
builder.Services.AddScoped<IConnectionRequestService, ConnectionRequestService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
