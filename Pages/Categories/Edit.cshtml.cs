using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Categories;

public class EditModel : PageModel
{
    private readonly BlogContext _context;

    public EditModel(BlogContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Category Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        Input = category;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id != Input.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (categoryToUpdate == null)
        {
            return NotFound();
        }

        categoryToUpdate.Name = Input.Name;
        await _context.SaveChangesAsync();

        return RedirectToPage("/Categories/Details", new { id = categoryToUpdate.Id });
    }
}

