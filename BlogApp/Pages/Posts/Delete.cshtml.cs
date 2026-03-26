using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Posts;

public class DeleteModel : PageModel
{
    private readonly BlogContext _context;

    public DeleteModel(BlogContext context)
    {
        _context = context;
    }

    public Post? Post { get; set; }

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

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Posts/Index");
    }
}

