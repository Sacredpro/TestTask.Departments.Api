using Microsoft.EntityFrameworkCore;
using TestTask.Departments.Api;
using TestTask.Departments.Api.Filters;
using TestTask.Departments.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingFilter>();
    options.Filters.Add<ExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.Configuration["DB_CONNECTION_STRING"]));
builder.Services.AddScoped<IApplicationContext, ApplicationContext>();
builder.Services.AddScoped<IDataParser, DataParser>();
builder.Services.AddScoped<IDbImporter, DbImporter>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var internalEmployeesContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    internalEmployeesContext.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
