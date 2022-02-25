namespace Magazine.Controllers;

[AllowAnonymous]
public class HomeController : BaseController
{
    private readonly IGenericRepository<Article> _articleRepository;

    public HomeController(IGenericRepository<Article> articleRepository)
        => _articleRepository = articleRepository;

    public async Task<IActionResult> Index()
       => View(await _articleRepository.GetAllAsync(includeProperties: "Category"));

    public async Task<IActionResult> Details(int id)
        => View(await _articleRepository.GetAsync(a => a.Id == id,
                    includeProperties: "Category"));

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
