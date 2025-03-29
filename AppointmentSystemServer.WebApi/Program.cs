using AppointmentSystemServer.Application;
using AppointmentSystemServer.Infrastructure;
using AppointmentSystemServer.Persistence;
using AppointmentSystemServer.WebApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddOpenApi();







var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseCors(PresentationServiceRegistration.AllowSpecificOrigins);

app.MapScalarApiReference();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
