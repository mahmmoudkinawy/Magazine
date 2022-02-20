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

    public async Task<IActionResult> Update(int id)
    {
        var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

        var postViewModel = new PostViewModel
        {
            Post = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id),
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
            _unitOfWork.PostRepository.Update(postViewModel.Post);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Post Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(postViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        //Can't control this flow at all

        //if (id == 0) return;

        var post = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id, "Category");

        //if (post = null) return;

        return View(post);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _unitOfWork.PostRepository.GetAsync(p => p.Id == id, "Category");

        _unitOfWork.PostRepository.Delete(post);
        await _unitOfWork.SaveAsync();
        TempData["success"] = "Post Deleted Successfully";

        return RedirectToAction(nameof(Index));
    }

}
