using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Categories;

public class DetailsModel : PageModel
{
    private readonly BlogContext _context;

    public DetailsModel(BlogContext context)
    {
        _context = context;
    }

    public Category? Category { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Category = await _context.Categories
            .Include(c => c.Posts)
            .ThenInclude(p => p.Author)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (Category == null)
        {
            return NotFound();
        }

        return Page();
    }
}

