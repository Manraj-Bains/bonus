using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Authors;

public class DetailsModel : PageModel
{
    private readonly BlogContext _context;

    public DetailsModel(BlogContext context)
    {
        _context = context;
    }

    public Author? Author { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Author = await _context.Authors
            .Include(a => a.Posts)
            .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (Author == null)
        {
            return NotFound();
        }

        return Page();
    }
}

