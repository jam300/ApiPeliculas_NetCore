using ApiPeliculas.Data;
using ApiPeliculas.PeliculasMapper;
using Microsoft.EntityFrameworkCore;
using ApiPeliculas.Shared.Utilities;
using ApiPeliculas.Extensions;
using ApiPeliculas.Entities;
using ApiPeliculas.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Ass IdentityService
builder.Services.AddIdentityServices(builder.Configuration);
//Add JwtToken
builder.Services.AddJwtAuthentication(builder.Configuration);
////Add TokenProvider
builder.Services.AddSingleton<TokenProvider>();
//Add Swagger documentation
builder.Services.AddSwaggerDocumentation();
//Add repository Layers
builder.Services.AddConfigureRepositories();
//Add ServiceLayer
builder.Services.AddConfigureServices();

//Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add JSON support
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

app.UseMiddleware<ApiPeliculas.Middlewares.ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiPeliculas v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
