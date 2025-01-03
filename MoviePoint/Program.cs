using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Data;
using MoviePoint.Models;
using MoviePoint.Repos;
using MoviePoint.Repos.IRepos;

namespace MoviePoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail= true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IMovieRepo, MovieRepo>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<ICinemaRepo, CinemaRepo>();
            builder.Services.AddScoped<IActorRepo, ActorRepo>();
            builder.Services.AddScoped<IActorMovieRepo, ActorMovieRepo>();
            builder.Services.AddScoped<ICinemaReqRepo, CinemaReqRepo>();
            builder.Services.AddScoped<ICartRepo, CartRepo>();
            builder.Services.AddScoped<IApplicationUserRepo, ApplicationUserRepo>();
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=HomeMovie}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
