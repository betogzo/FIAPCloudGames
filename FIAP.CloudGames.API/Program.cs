using System.Text;
using FIAP.CloudGames.API.Middlewares;
using FIAP.CloudGames.Application.Interfaces.Services;
using FIAP.CloudGames.Application.Services;
using FIAP.CloudGames.Domain.DomainServices;
using FIAP.CloudGames.Domain.Interfaces.Policies;
using FIAP.CloudGames.Domain.Interfaces.Repositories;
using FIAP.CloudGames.Infrastructure.Data.Contexts;
using FIAP.CloudGames.Infrastructure.Repositories;
using FIAP.CloudGames.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services
    .AddScoped<IValidateSenhaService, DefaultValidateSenhaService>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization(); 

builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IUsuarioRepository, UsuariosRepository>();
builder.Services.AddTransient<ICriptografiaService, HashService>();
builder.Services.AddTransient<ILoginService, LoginService>();

builder.Services.Configure<ApiBehaviorOptions>(opts =>
{
    opts.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(kvp => kvp.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var customPayload = new
        {
            Success = false,
            Errors = errors
        };

        return new BadRequestObjectResult(customPayload);
    };
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandling();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();