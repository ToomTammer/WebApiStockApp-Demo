using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using webapi.Controllers.Data;
using webapi.Interfaces;
using webapi.Model;
using webapi.Repository;
using webapi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Swagger like postman, UI for test API

// Swagger/OpenAPI is a specification for documenting RESTful APIs.
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" }); /// for set Name and version  our API
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



//Install NuGet Newtonsoft.Json and Microsoft.AspNetCore.Mvc.NewtonsoftJson and type this
// Configure ASP.NET Core controllers to use Newtonsoft.Json for JSON serialization.
// Newtonsoft.Json is a popular library for working with JSON data in .NET applications.
// Setting ReferenceLoopHandling to Ignore prevents errors when serializing objects with circular references.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//set up for identity
builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
{
    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequiredLength = 12;
})
.AddEntityFrameworkStores<ApplicationDBContext>(); // Adds Entity Framework implementation of Identity stores

builder.Services.AddAuthentication(option =>
{
    // Setting various default authentication scheme
    option.DefaultAuthenticateScheme =
    option.DefaultChallengeScheme =
    option.DefaultForbidScheme =
    option.DefaultScheme =
    option.DefaultSignInScheme =
    option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>{
    // Configuring token validation parameters
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Validates the issuer of the token
        ValidIssuer = builder.Configuration["JWT:Issuer"], // Specifies the valid issuer of the token
        ValidateAudience = true, // Validates the audience of the token
        ValidAudience = builder.Configuration["JWT:Audience"], // Specifies the valid audience of the token
        ValidateIssuerSigningKey = true, // Validates the signing key of the token
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]) // Sets the symmetric security key used for token validation
        )

    };
});

//Ceeate Interface & Repo, we need to wire up this service for make it work
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

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
