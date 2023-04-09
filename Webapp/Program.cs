using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Webapp.Models;
using Webapp.Repositories;
using Webapp.APIs;

namespace Webapp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<AuthorsAPI>();
        builder.Services.AddScoped<BooksAPI>();
        builder.Services.AddScoped<UsersAPI>();
        builder.Services.AddScoped<AuthenticationAPI>();

        builder.Services.AddTransient<IUserStore<Account>, AccountRepository>();
        builder.Services.AddTransient<IRoleStore<AccountRole>, AccountRoleRepository>();
        builder.Services.AddTransient<IAccountManager, AccountManager>();

        builder.Services.AddIdentity<Account, AccountRole>()
                        .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(config =>
        {
            config.LoginPath = "/Users/Login";
            config.AccessDeniedPath = "/Users/AccessDenied";
            config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            config.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        });

        builder.Services.AddSession();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Authors}/{action=Index}");

        app.Run();
    }
}