namespace Magazine.Controllers;
public class CategoryController : BaseController
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
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Update(int id)
        => View(await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Delete(int id)
        => View(await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id));

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        //I must redirect to page not found
        //if (id == 0) return;

        var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);

        //I must redirect to page not found
        //if (id == null) return;

        _unitOfWork.CategoryRepository.Delete(category);
        await _unitOfWork.SaveAsync();
        TempData["success"] = "Category Removed Successfully";

        return RedirectToAction(nameof(Index));
    }

}
