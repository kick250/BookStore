using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Services;
using System.Text;

namespace Webapi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<UsersService>();
        builder.Services.AddScoped<AuthorsService>();
        builder.Services.AddScoped<BooksService>();
        builder.Services.AddScoped<AuthenticationService>();

        builder.Services.AddControllers()
            .AddNewtonsoftJson(config =>
            {
                config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<BookStoreContext>(contextBuilder =>
        {
            contextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreDb"));
        });

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            var tokenSecret = Encoding.Default.GetBytes(GetTokenSecret(builder.Configuration));

            config.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = AuthenticationService.ISSUER,
                IssuerSigningKey = new SymmetricSecurityKey(tokenSecret),
                ValidateAudience = false
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
    }

    private static string GetTokenSecret(IConfiguration configuration)
    {
        string? value = configuration["TokenSecret"];

        if (value == null)
            return "";

        return value;
    }
}