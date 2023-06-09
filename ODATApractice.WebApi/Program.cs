using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ODATApractice.WebApi.Infrastructure.Context;
using System.Diagnostics;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(i => i.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve)
    .AddOData(conf =>
    {
        conf.EnableQueryFeatures();
    })
    ;

builder.Services.AddDbContext<StudentDbContext>((sp, conf) =>
{
    conf.UseNpgsql("User ID=postgres; Password=mysecretpassword; Server=localhost; Port=5432; Database=ODataPractice;Pooling=true;Include Error Detail=true; ");
    conf.EnableSensitiveDataLogging();
});


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();