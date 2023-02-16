using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Project.Business.Blog;
using Project.Business.DependencyResolvers.System;
using Project.Business.Helpers.Mapper.AutoMapper;
using Project.Core.Core.Entities.Configuration;
using Project.Core.Core.Entities.Security;
using Project.Core.Utilities.Security.Encryption;
using Project.Core.Utilities.Security.Jwt;
using Project.Data;
using Project.Data.Blog;
using Project.Data.DependencyResolvers.System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<ITokenHelper, JwtHelper>();

builder.Services.RegisterServices();
builder.Services.RegisterRepositories();
var connectionString = builder.Configuration.GetConnectionString("EvrimDb");
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
      .AddCookie(opts =>
      {
          opts.Cookie.Name = $".evrimersin.auth";   // todo : deðiþtirin.
          opts.AccessDeniedPath = "/Home/AccessDenied";
          opts.LoginPath = "/Authentication/Login";
          opts.SlidingExpiration = false;
          opts.ExpireTimeSpan = TimeSpan.FromHours(2);
      });
    

builder.Services.AddScoped((s) => new SqlConnection(connectionString));
builder.Services.AddScoped<IDbTransaction>(s =>
{
    SqlConnection conn = s.GetRequiredService<SqlConnection>();
    conn.Open();
    return conn.BeginTransaction();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Lesson}/{action=Index}/{id?}");

app.Run();
