using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Authentication

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(

    jwtOption =>
    {
        var key = builder.Configuration.GetValue<string>("jwtAuth:key");
        var keyBytes = Encoding.UTF8.GetBytes(key);
        jwtOption.SaveToken = true;
        jwtOption.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidAudience = "http://localhost:5257",
            ValidIssuer = "http://localhost:5257",
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    }
 );

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
