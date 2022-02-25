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
            var roles = new List<IdentityRole>
            {
                new IdentityRole(Constants.RoleAdmin),
                new IdentityRole(Constants.RoleUser)
            };

            foreach (var role in roles)
                await _roleManager.CreateAsync(role);

            var admin = new IdentityUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com"
            };

            await _userManager.CreateAsync(admin, "Pa$$w0rd");
            await _userManager.AddToRoleAsync(admin, Constants.RoleAdmin);

            var user = new IdentityUser
            {
                UserName = "bob@test.com",
                Email = "bob@test.com"
            };

            await _userManager.CreateAsync(user, "Pa$$w0rd");
            await _userManager.AddToRoleAsync(user, Constants.RoleUser);
        }
        return;
    }
}
