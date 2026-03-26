using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Categories;

public class DeleteModel : PageModel
{
    private readonly BlogContext _context;

    public DeleteModel(BlogContext context)
    {
        _context = context;
    }

    public Category? Category { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Category = await _context.Categories
            .Include(c => c.Posts)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (Category == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Categories/Index");
    }
}

