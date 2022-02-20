namespace Magazine.Controllers;
public class PostsController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public PostsController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IActionResult> Index() =>
        View(await _unitOfWork.PostRepository.GetAllAsync(includeProperties: "Category"));
}
