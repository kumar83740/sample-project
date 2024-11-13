using Microsoft.EntityFrameworkCore;
using UserDemographicsApp.Models;
using UserDemographicsApp.ViewModels;

public class ApplicationDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<State> States { get; set; }

    public DbSet<ResponseViewModel> ResponseViewModel { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>()
            .HasMany(q => q.Options)
            .WithOne(o => o.Question)
            .HasForeignKey(o => o.QuestionId);

        modelBuilder.Entity<Response>()
            .HasOne(r => r.Question)
            .WithMany()
            .HasForeignKey(r => r.QuestionId);

        modelBuilder.Entity<Response>()
            .HasOne(r => r.Option)
            .WithMany()
            .HasForeignKey(r => r.OptionId);

        modelBuilder.Entity<Response>()
            .HasOne(r => r.Country)
            .WithMany()
            .HasForeignKey(r => r.CountryId);

        // Map the ResponseModel to the stored procedure
        modelBuilder.Entity<ResponseViewModel>().HasNoKey().ToView(null);
    }
}
