namespace Magazine.Controllers;
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IActionResult> Index()
        => View(await _unitOfWork.CategoryRepository.GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }
}
