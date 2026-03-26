using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.Pages.Authors;

public class CreateModel : PageModel
{
    private readonly BlogContext _context;

    public CreateModel(BlogContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Author Input { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Authors.Add(Input);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Authors/Index");
    }
}

