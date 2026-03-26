using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany(a => a.Posts)
            .HasForeignKey(p => p.AuthorId);
    }

    public static async Task SeedAsync(BlogContext context)
    {
        // Only seed if there is no data yet.
        if (await context.Categories.AnyAsync())
        {
            return;
        }

        var tech = new Category { Name = "Technology" };
        var travel = new Category { Name = "Travel" };
        var study = new Category { Name = "Study Tips" };

        var alex = new Author { Name = "Alex Kim" };
        var jordan = new Author { Name = "Jordan Patel" };
        var sam = new Author { Name = "Sam Nguyen" };

        context.Categories.AddRange(tech, travel, study);
        context.Authors.AddRange(alex, jordan, sam);
        await context.SaveChangesAsync();

        context.Posts.AddRange(
            new Post
            {
                Title = "Welcome to the Blog",
                Content = "This is a simple student blog using Razor Pages, EF Core, and SQLite. Use the navigation links to manage Posts, Categories, and Authors.",
                CategoryId = tech.Id,
                AuthorId = alex.Id
            },
            new Post
            {
                Title = "Why SQLite is Great for Class Projects",
                Content = "SQLite is easy to set up because it stores everything in one file. EF Core works well with it for simple apps.",
                CategoryId = tech.Id,
                AuthorId = jordan.Id
            },
            new Post
            {
                Title = "A Quick Weekend Travel Idea",
                Content = "Pick a nearby town, plan one activity, and keep the schedule relaxed. You will enjoy the trip more when it is simple.",
                CategoryId = travel.Id,
                AuthorId = sam.Id
            },
            new Post
            {
                Title = "Studying in Small Sessions",
                Content = "Study for 25 minutes, take a 5 minute break, then repeat. Short sessions help you stay focused without burning out.",
                CategoryId = study.Id,
                AuthorId = alex.Id
            },
            new Post
            {
                Title = "Group Projects: Simple Communication Rules",
                Content = "Decide who does what, set a deadline, and check in briefly. Clear communication prevents last-minute stress.",
                CategoryId = study.Id,
                AuthorId = jordan.Id
            }
        );

        await context.SaveChangesAsync();
    }
}

