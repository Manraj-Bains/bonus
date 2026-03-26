using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Authors;

public class DeleteModel : PageModel
{
    private readonly BlogContext _context;

    public DeleteModel(BlogContext context)
    {
        _context = context;
    }

    public Author? Author { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Author = await _context.Authors
            .Include(a => a.Posts)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (Author == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Authors/Index");
    }
}

