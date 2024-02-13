using ContactService.API.Middlewares;
using ContactService.Application;
using ContactService.Application.Services;
using ContactService.Infrastructure;
using ContactService.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContactService, UserContactService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
