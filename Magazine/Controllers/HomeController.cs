namespace Magazine.Controllers;
public class HomeController : Controller
{
    private readonly IGenericRepository<Post> _postRepository;

    public HomeController(IGenericRepository<Post> postRepository)
        => _postRepository = postRepository;

    public async Task<IActionResult> Index()
       => View(await _postRepository.GetAllAsync(includeProperties: "Category"));

    public async Task<IActionResult> Details(int id)
        => View(await _postRepository.GetAsync(a => a.Id == id,
                    includeProperties: "Category"));

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
