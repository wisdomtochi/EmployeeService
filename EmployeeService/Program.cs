using EmployeeService.Data;
using EmployeeService.Data_Access;
using EmployeeService.Data_Access.Implementation;
using EmployeeService.Data_Access.Interfaces;
using EmployeeService.Services.Implementations;
using EmployeeService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string dbcon = builder.Configuration.GetConnectionString("CustomerDBConnection");
builder.Services.AddDbContextPool<EmployeeDbContext>(options =>
{
    options.UseMySql(dbcon, ServerVersion.AutoDetect(dbcon), sqlopt =>
    {
        sqlopt.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), null);
        sqlopt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IConnectionsLogicLayer, ConnectionsLogicLayer>();
builder.Services.AddScoped<IConnectionRequestLogicLayer, ConnectionRequestLogicLayer>();

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
