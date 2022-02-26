namespace Magazine.Extensions;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<MagazineDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddRazorPages().AddRazorRuntimeCompilation();

        services.AddScoped<IDbInitializer, DbInitializer.DbInitializer>();

        services.AddControllersWithViews().AddRazorRuntimeCompilation();

        return services;
    }
}
