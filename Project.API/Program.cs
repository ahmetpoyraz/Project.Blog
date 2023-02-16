using Microsoft.Extensions.DependencyInjection;
using Project.Business.Helpers.Mapper.AutoMapper;
using Project.Data;
using Project.Data.Blog;
using System.Data;
using System.Data.SqlClient;
using Project.Business.Blog;
using Project.Business.Authentication;
using Project.Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Core.Entities.Security;
using Project.Core.Utilities.Security.Encryption;
using Microsoft.OpenApi.Models;
using Project.Business.DependencyResolvers.System;
using Project.Data.DependencyResolvers.System;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
string connectionString = builder.Configuration.GetConnectionString("EvrimDb");
builder.Services.AddScoped((s) => new SqlConnection(connectionString));
builder.Services.AddScoped<IDbTransaction>(s =>
{
    SqlConnection conn = s.GetRequiredService<SqlConnection>();
    conn.Open();
    return conn.BeginTransaction();
});
builder.Services.RegisterServices();
builder.Services.RegisterRepositories();
builder.Services.AddScoped<ITokenHelper, JwtHelper>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//JwtBearerDefaults.AuthenticationScheme
builder.
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
