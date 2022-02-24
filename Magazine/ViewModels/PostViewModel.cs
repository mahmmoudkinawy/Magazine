namespace Magazine.ViewModels;
public class PostViewModel
{
    [ValidateNever]
    public IEnumerable<SelectListItem> Categories { get; set; }

    public Article Post { get; set; }
}