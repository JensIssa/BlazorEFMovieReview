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
    }

    public DbSet<Movie> MovieTable { get; set; }
    public DbSet<Review> ReviewTable { get; set; }
}