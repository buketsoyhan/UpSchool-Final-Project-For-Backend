using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WebApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddCors(options =>
{
    //'https://localhost:7257/'
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration); // Encapsulation with AddInfrastructure

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SeleniumLogHub>("/Hubs/SeleniumLogHub");

app.Run();
