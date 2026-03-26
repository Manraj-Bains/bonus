using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Authors;

public class EditModel : PageModel
{
    private readonly BlogContext _context;

    public EditModel(BlogContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Author Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (author == null)
        {
            return NotFound();
        }

        Input = author;
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

        var authorToUpdate = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        if (authorToUpdate == null)
        {
            return NotFound();
        }

        authorToUpdate.Name = Input.Name;
        await _context.SaveChangesAsync();

        return RedirectToPage("/Authors/Details", new { id = authorToUpdate.Id });
    }
}

