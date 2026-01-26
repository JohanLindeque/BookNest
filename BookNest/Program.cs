using BookNest.Constants;
using BookNest.Data;
using BookNest.Models.Entities;
using BookNest.Repositories;
using BookNest.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookNest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // db context
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("BookNestDB"));
        });

        // default identity user
        builder
            .Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // FALSE = for development only
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // DI
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IRepository<Checkout>, CheckoutRepository>();
        builder.Services.AddScoped<IRepository<Author>, AuthorRepository>();

        builder.Services.AddScoped<ILibraryService, LibraryService>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // add scope for seeding data on app run
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            RoleSeeder.SeedRolesAsync(services).Wait();
            UserSeeder.SeedUsersAsync(services).Wait();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        // enable using razorpages - identity scafolded pages for example
        app.MapRazorPages();

        app.MapStaticAssets();
        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
