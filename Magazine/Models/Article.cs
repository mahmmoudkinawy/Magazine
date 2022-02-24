namespace Magazine.Models;
public class Article
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; }

    [Required]
    [MaxLength(10000)]
    public string Content { get; set; }

    [Required]
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; }

    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }
}