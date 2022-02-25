namespace Magazine.Extensions;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<MagazineDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));

        services.AddControllersWithViews().AddRazorRuntimeCompilation();

        services.AddRazorPages().AddRazorRuntimeCompilation();

        services.AddScoped<IDbInitializer, DbInitializer.DbInitializer>();

        services.AddScoped<IEmailSender, EmailSender>();

        services.AddSingleton<IEmailSender, EmailSender>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
            options.LogoutPath = "/Identity/Account/Logout";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });

        return services;
    }
}
