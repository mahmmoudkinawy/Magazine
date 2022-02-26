namespace Magazine.DbInitializer;
public class DbInitializer : IDbInitializer
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly MagazineDbContext _context;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        MagazineDbContext context,
        ILogger<DbInitializer> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.GetPendingMigrations().Count() > 0)
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

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
