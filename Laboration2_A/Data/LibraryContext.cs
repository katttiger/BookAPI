using Laboration2_A.Models;
using Microsoft.EntityFrameworkCore;

namespace Laboration2_A.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<Borrower> Borrowers { get; set; } = null!;

        //Server=localhost;Database=TeamAPI;Trusted_Connection=True; TrustServerCertificate=True;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=Books;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
