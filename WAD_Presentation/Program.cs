using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Primarie_Craiova.Models;
using Primarie_Craiova.Repositories;
using Primarie_Craiova.Repositories.Interfaces;
using Primarie_Craiova.Services;
using Primarie_Craiova.Services.Interfaces;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        builder.Services.AddScoped<IEmployeesService, EmployeesService>();
        builder.Services.AddScoped<ICitizensService, CitizensService>();
        builder.Services.AddScoped<ICitizensRepository, CitizensRepository>();
        builder.Services.AddScoped<IComplaintsRepository, ComplaintsRepository>();
        builder.Services.AddScoped<IComplaintsService, ComplaintsService>();
        builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
        builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var app = builder.Build();

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
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        await app.RunAsync();
    }
}
