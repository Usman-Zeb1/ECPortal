using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Services;
using System.Configuration;
using Newtonsoft.Json;
using Pk.Com.Jazz.ECP.Utilities;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public IConfiguration _configuration { get; }
    public IWebHostEnvironment _env { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ECContext>(options =>
            options.UseSqlServer(
                _configuration.GetConnectionString("DefaultConnection")));

        services.AddDefaultIdentity<AppUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ECContext>();

        // Consolidate AddControllersWithViews, AddRazorPages, AddNewtonsoftJson
        services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .AddSessionStateTempDataProvider();

        services.AddRazorPages().AddSessionStateTempDataProvider();
        services.AddSession();
        services.AddServerSideBlazor();
        services.Configure<EmailSettings>(_configuration.GetSection("EmailSettings"));
        services.AddScoped<EmailSender, EmailSender>();

        IMvcBuilder builder = services.AddRazorPages();
#if DEBUG
        if (_env.IsDevelopment())
        {
            //builder.AddRazorRuntimeCompilation();
        }
#endif
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        RoleInitializer.InitializeAsync(roleManager).GetAwaiter().GetResult();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        CreateRoles(serviceProvider).Wait();
    }

    private async Task CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roleNames = { Roles.Admin, Roles.Agent, Roles.TeamLead, Roles.RCCH, Roles.ECM, Roles.HOD, Roles.Trainer, Roles.OPG };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}