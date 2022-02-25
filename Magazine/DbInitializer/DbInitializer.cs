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

    public void Initialize()
    {
        try
        {
            if (_context.Database.GetPendingMigrations().Count() > 0)
            {
                _context.Database.Migrate();
            }
        }
        catch (Exception ex)
        {

            throw;
        }

        if (!_roleManager.RoleExistsAsync(Constants.RoleAdmin).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(Constants.RoleAdmin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Constants.RoleUser)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin",
                Email = "admin@test.com"
            }, "Pa$$w0rd").GetAwaiter().GetResult();

            IdentityUser admin = _userManager.FindByEmailAsync("admin@test.com").GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(admin, Constants.RoleAdmin).GetAwaiter().GetResult();
        }
        return;
    }
}
