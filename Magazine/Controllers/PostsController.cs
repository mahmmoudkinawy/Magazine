namespace Magazine.Controllers;
public class PostsController : Controller
{
    private readonly IGenericRepository<Post> _postRepository;
    private readonly IGenericRepository<Category> _categoryRepository;

    public PostsController(IGenericRepository<Post> portRepository,
        IGenericRepository<Category> categoryRepository)
    {
        _postRepository = portRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index() =>
        View(await _postRepository.GetAllAsync(includeProperties: "Category"));

    public async Task<IActionResult> Create()
    {
        var categories = await _categoryRepository.GetAllAsync();

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
            _postRepository.Add(postViewModel.Post);
            TempData["success"] = "Post Created Successfully";
            return RedirectToAction(nameof(Index));
        }

        return View(postViewModel);
    }

    public async Task<IActionResult> Update(int id)
    {
        var categories = await _categoryRepository.GetAllAsync();

        var postViewModel = new PostViewModel
        {
            Post = await _postRepository.GetAsync(p => p.Id == id),
            Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
        };

        return View(postViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(PostViewModel postViewModel)
    {
        if (ModelState.IsValid)
        {
            _postRepository.Update(postViewModel.Post);
            TempData["success"] = "Post Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(postViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        //Can't control this flow at all

        //if (id == 0) return;

        var post = await _postRepository.GetAsync(p => p.Id == id, "Category");

        //if (post = null) return;

        return View(post);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _postRepository.GetAsync(p => p.Id == id, "Category");

        _postRepository.Delete(post);
        TempData["success"] = "Post Deleted Successfully";

        return RedirectToAction(nameof(Index));
    }

}
