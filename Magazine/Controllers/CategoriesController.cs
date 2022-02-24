namespace Magazine.Controllers;
public class CategoriesController : Controller
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly MagazineDbContext _context;

    public CategoriesController(IGenericRepository<Category> categoryRepository,
        MagazineDbContext context)
    {
        _categoryRepository = categoryRepository;
        _context = context;
    } 

    public async Task<IActionResult> Index()
        => View(await _categoryRepository.GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryRepository.Add(category);
            await _context.SaveChangesAsync();
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
            _categoryRepository.Update(category);
            await _context.SaveChangesAsync();
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

        _categoryRepository.Delete(category);
        await _context.SaveChangesAsync();
        TempData["success"] = "Category Removed Successfully";

        return RedirectToAction(nameof(Index));
    }

}
