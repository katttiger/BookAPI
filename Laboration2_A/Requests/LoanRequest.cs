using Laboration2_A.Models;
using Microsoft.Identity.Client;

namespace Laboration2_A.Requests
{
    public class LoanRequest
    {
        public DateOnly LoanDate { get; set; }
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
    }
}
