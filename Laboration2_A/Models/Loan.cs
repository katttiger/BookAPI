using System.Diagnostics.CodeAnalysis;

namespace Laboration2_A.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public DateOnly LoanDate { get; set; }
        public DateOnly? ReturnDate { get; set; } = null;
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public bool IsActive { get; set; } = true;

        //Ett lån kan bara ha en bok
        //Ett lån kan bara ha en låntagare
        public Book Book { get; set; }
        public Borrower Borrower { get; set; } = null!;
    }
}
