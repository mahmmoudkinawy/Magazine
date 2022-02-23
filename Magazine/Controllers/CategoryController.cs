namespace Magazine.Controllers;
public class CategoryController : Controller
{
    private readonly IGenericRepository<Category> _repository;

    public CategoryController(IGenericRepository<Category> repository) =>
        _repository = repository;

    public async Task<IActionResult> Index()
        => View(await _repository.GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _repository.Add(category);
            await _repository.SaveChangesAsync();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Update(int id)
        => View(await _repository.GetAsync(c => c.Id == id));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Category category)
    {
        if (ModelState.IsValid)
        {
            _repository.Update(category);
            await _repository.SaveChangesAsync();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Delete(int id)
        => View(await _repository.GetAsync(c => c.Id == id));

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        //I must redirect to page not found
        //if (id == 0) return;

        var category = await _repository.GetAsync(c => c.Id == id);

        //I must redirect to page not found
        //if (id == null) return;

        _repository.Delete(category);
        await _repository.SaveChangesAsync();
        TempData["success"] = "Category Removed Successfully";

        return RedirectToAction(nameof(Index));
    }

}
