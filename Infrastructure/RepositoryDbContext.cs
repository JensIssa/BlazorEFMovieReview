using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class RepositoryDbContext : Microsoft.EntityFrameworkCore.DbContext
{

    public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options, ServiceLifetime serviceLifetime) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        List<Review> mockReviews = new List<Review>();
        Review newReview = new Review();
        mockReviews.Add(newReview);
        
        //Generating new ID everytime a movie object gets added
        modelBuilder.Entity<Movie>().
            Property(f => f.Id).
            ValueGeneratedOnAdd();
        //Generating new ID everytime a Review object gets added
        modelBuilder.Entity<Review>().
            Property(f => f.Id).
            ValueGeneratedOnAdd();
        //Generating a many to one relation with a movie to a lot of reviews
        modelBuilder.Entity<Review>().HasOne(review => review.Movie).
            WithMany(movie => movie.Reviews)
            .HasForeignKey(review => review.MovieId).
            OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Movie>().HasData(new Movie
        {
            BoxOfficeSumInMillions = 42, Id = -1, ReleaseYear = 1999, Reviews = mockReviews, Summary = "super gewd",
            Title = "Dark, The Batman Knight" });
    }

    public DbSet<Movie> MovieTable { get; set; }
    public DbSet<Review> ReviewTable { get; set; }
}