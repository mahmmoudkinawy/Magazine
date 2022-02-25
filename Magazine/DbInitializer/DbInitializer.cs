using Magazine.Helpers;

namespace Magazine.DbInitializer;
public class DbInitializer : IDbInitializer
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly MagazineDbContext _context;

    public DbInitializer(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        MagazineDbContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = context;
    }

    public async Task InitializeAsync()
    {
        if (_context.Database.GetPendingMigrations().Any())
            await _context.Database.MigrateAsync();

        if (!await _roleManager.RoleExistsAsync(Constants.RoleAdmin))
        {
            await _roleManager.CreateAsync(new IdentityRole(Constants.RoleAdmin));
            await _roleManager.CreateAsync(new IdentityRole(Constants.RoleUser));

            await _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com"
            }, "Pa$$w0rd");

            var admin = await _userManager.FindByEmailAsync("admin@test.com");

            await _userManager.AddToRoleAsync(admin, Constants.RoleAdmin);
        }
        return;
    }
}
