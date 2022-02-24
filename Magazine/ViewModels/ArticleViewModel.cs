namespace Magazine.ViewModels;
public class ArticleViewModel
{
    [ValidateNever]
    public IEnumerable<SelectListItem> Categories { get; set; }

    public Article Article { get; set; }
}