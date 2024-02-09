using Laboration2_A.Models;
using Microsoft.Identity.Client;

namespace Laboration2_A.IdRequest
{
    public class LoanIdRequest
    {
        public int Id { get; set; }
        public DateOnly DateReturned { get; set; }
    }
}
