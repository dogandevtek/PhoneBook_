using ReportService.API.Middlewares;
using ReportService.Application.Services;
using ReportService.Application;
using ReportService.Infrastructure;
using ReportService.API.Registration;
using ReportService.Infrastructure.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureIntegrationEvents();

builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IReportService, ReportService.Infrastructure.Services.ReportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.MapControllers();

app.RegisterIntegrationEvents();

app.Run();
