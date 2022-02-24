namespace Magazine.Controllers;
public class ArticlesController : Controller
{
    private readonly IGenericRepository<Article> _articleRepository;
    private readonly IGenericRepository<Category> _categoryRepository;

    public ArticlesController(IGenericRepository<Article> portRepository,
        IGenericRepository<Category> categoryRepository)
    {
        _articleRepository = portRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index() =>
        View(await _articleRepository.GetAllAsync(includeProperties: "Category"));

    public async Task<IActionResult> Create()
    {
        var categories = await _categoryRepository.GetAllAsync();

        var articleViewModel = new ArticleViewModel
        {
            Article = new Article(),
            Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
        };

        return View(articleViewModel);
    }

    [HttpPost, ActionName("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult CreatePost(ArticleViewModel articleViewModel)
    {
        if (ModelState.IsValid)
        {
            articleViewModel.Article.CreatedDate = DateTime.Now;
            _articleRepository.Add(articleViewModel.Article);
            TempData["success"] = "Article Created Successfully";
            return RedirectToAction(nameof(Index));
        }

        return View(articleViewModel);
    }

    public async Task<IActionResult> Update(int id)
    {
        var categories = await _categoryRepository.GetAllAsync();

        var articleViewModel = new ArticleViewModel
        {
            Article = await _articleRepository.GetAsync(p => p.Id == id),
            Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
        };

        return View(articleViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(ArticleViewModel articleViewModel)
    {
        if (ModelState.IsValid)
        {
            _articleRepository.Update(articleViewModel.Article);
            TempData["success"] = "Article Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(articleViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        //Can't control this flow at all

        //if (id == 0) return;

        var article = await _articleRepository.GetAsync(p => p.Id == id, "Category");

        //if (post = null) return;

        return View(article);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        var article = await _articleRepository.GetAsync(p => p.Id == id, "Category");

        _articleRepository.Delete(article);
        TempData["success"] = "Article Deleted Successfully";

        return RedirectToAction(nameof(Index));
    }

}
