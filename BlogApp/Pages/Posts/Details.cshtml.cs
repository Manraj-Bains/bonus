using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Posts;

public class DetailsModel : PageModel
{
    private readonly BlogContext _context;

    public DetailsModel(BlogContext context)
    {
        _context = context;
    }

    public Post? Post { get; set; }
    public List<Post> RelatedPosts { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Post = await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (Post == null)
        {
            return NotFound();
        }

        RelatedPosts = await _context.Posts
            .Where(p => p.CategoryId == Post.CategoryId && p.Id != id)
            .OrderByDescending(p => p.Id)
            .ToListAsync();

        return Page();
    }
}

