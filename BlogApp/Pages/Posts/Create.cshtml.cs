using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Posts;

public class CreateModel : PageModel
{
    private readonly BlogContext _context;

    public CreateModel(BlogContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Post Input { get; set; } = new();

    public List<SelectListItem> CategoryOptions { get; set; } = new();
    public List<SelectListItem> AuthorOptions { get; set; } = new();

    public async Task OnGetAsync()
    {
        await PopulateOptionsAsync();
    }

    private async Task PopulateOptionsAsync()
    {
        CategoryOptions = await _context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToListAsync();

        AuthorOptions = await _context.Authors
            .OrderBy(a => a.Name)
            .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name })
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await PopulateOptionsAsync();
            return Page();
        }

        _context.Posts.Add(Input);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Posts/Index");
    }
}

