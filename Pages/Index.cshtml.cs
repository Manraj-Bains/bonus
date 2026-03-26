using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages;

public class IndexModel : PageModel
{
    private readonly BlogContext _context;

    public IndexModel(BlogContext context)
    {
        _context = context;
    }

    public List<Post> Posts { get; set; } = new();
    public List<Category> CategoriesWithPosts { get; set; } = new();

    public async Task OnGetAsync()
    {
        Posts = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Category)
            .OrderByDescending(p => p.Id)
            .ToListAsync();

        CategoriesWithPosts = await _context.Categories
            .Include(c => c.Posts)
            .OrderBy(c => c.Name)
            .ToListAsync();

        CategoriesWithPosts = CategoriesWithPosts
            .Where(c => c.Posts.Count > 0)
            .ToList();
    }
}
