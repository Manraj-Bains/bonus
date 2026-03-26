using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Pages.Authors;

public class IndexModel : PageModel
{
    private readonly BlogContext _context;

    public IndexModel(BlogContext context)
    {
        _context = context;
    }

    public List<Author> Authors { get; set; } = new();

    public async Task OnGetAsync()
    {
        Authors = await _context.Authors
            .Include(a => a.Posts)
            .OrderBy(a => a.Name)
            .ToListAsync();
    }
}

