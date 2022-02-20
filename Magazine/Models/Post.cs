namespace Magazine.Models;
public class Post
{
    public int Id { get; set; }

    [Required]
    [MaxLength(10000)]
    public string Content { get; set; }

    [Required]
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}