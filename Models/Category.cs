using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    public List<Post> Posts { get; set; } = new();
}

