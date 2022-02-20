namespace Magazine.Controllers;
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IActionResult> Index()
    {
        return View(await _unitOfWork.CategoryRepository.GetAllAsync());
    }
}
