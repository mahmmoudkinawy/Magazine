namespace Magazine.Extensions;
public static class ApplicationServicesExtensions
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

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}
