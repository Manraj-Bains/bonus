using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Posts;

public class EditModel : PageModel
{
    private readonly BlogContext _context;

    public EditModel(BlogContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Post Input { get; set; } = new();

    public List<SelectListItem> CategoryOptions { get; set; } = new();
    public List<SelectListItem> AuthorOptions { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        Input = post;
        await PopulateOptionsAsync();

        return Page();
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id != Input.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await PopulateOptionsAsync();
            return Page();
        }

        var postToUpdate = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (postToUpdate == null)
        {
            return NotFound();
        }

        postToUpdate.Title = Input.Title;
        postToUpdate.Content = Input.Content;
        postToUpdate.CategoryId = Input.CategoryId;
        postToUpdate.AuthorId = Input.AuthorId;

        await _context.SaveChangesAsync();

        return RedirectToPage("/Posts/Details", new { id = postToUpdate.Id });
    }
}

