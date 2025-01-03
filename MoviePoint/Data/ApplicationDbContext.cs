using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviePoint.Models;

namespace MoviePoint.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<CinemaReq> CinemaReqs { get; set; }
        public DbSet<Cart> Carts { get; set; }

        //Old
        public ApplicationDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MoreTickets;Integrated Security=True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<ActorMovie>()
                .HasKey(e => new { e.MovieId, e.ActorId });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(e => e.Actor)
                .WithMany(e => e.ActorMovies)
                .HasForeignKey(e => e.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(e => e.Movie)
                .WithMany(e => e.ActorMovies)
                .HasForeignKey(e => e.MovieId);
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.status)
                .HasConversion<string>()
                .HasDefaultValue(CStatus.Pending);

            modelBuilder.Entity<CinemaReq>()
                .Property(e => e.Status)
                .HasConversion<string>()
                .HasDefaultValue(CStatus.Pending);
        }
    }
}
