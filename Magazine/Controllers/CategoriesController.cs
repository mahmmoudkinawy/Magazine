namespace Magazine.Controllers;
public class CategoriesController : BaseController
{
    private readonly IGenericRepository<Category> _categoryRepository;

    public CategoriesController(IGenericRepository<Category> categoryRepository)
        => _categoryRepository = categoryRepository;

    public async Task<IActionResult> Index()
        => View(await _categoryRepository.GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            await _categoryRepository.Add(category);
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Update(int id)
        => View(await _categoryRepository.GetAsync(c => c.Id == id));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Category category)
    {
        if (ModelState.IsValid)
        {
            await _categoryRepository.Update(category);
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Delete(int id)
        => View(await _categoryRepository.GetAsync(c => c.Id == id));

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        //I must redirect to page not found
        //if (id == 0) return;

        var category = await _categoryRepository.GetAsync(c => c.Id == id);

        //I must redirect to page not found
        //if (id == null) return;

        await _categoryRepository.Delete(category);
        TempData["success"] = "Category Removed Successfully";

        return RedirectToAction(nameof(Index));
    }

}
