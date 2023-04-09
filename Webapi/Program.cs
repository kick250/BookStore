using Microsoft.EntityFrameworkCore;
using Repository;
using Services;

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
    }
}