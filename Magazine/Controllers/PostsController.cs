namespace Magazine.Controllers;
public class PostsController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public PostsController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IActionResult> Index() =>
        View(await _unitOfWork.PostRepository.GetAllAsync(includeProperties: "Category"));

    public async Task<IActionResult> Create()
    {
        var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

        var postViewModel = new PostViewModel
        {
            Post = new Post(),
            Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
        };

        return View(postViewModel);
    }

    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePost(PostViewModel postViewModel)
    {
        if (ModelState.IsValid)
        {
            postViewModel.Post.CreatedDate = DateTime.Now;
            _unitOfWork.PostRepository.Add(postViewModel.Post);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Post Created Successfully";
            return RedirectToAction(nameof(Index));
        }

        return View(postViewModel);
    }
}
