namespace Laboration2_A.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SocialSecurityNumber { get; set; }

        //En låntagare kan ha flera lån
        //I början har personen inga lån
       public ICollection<Loan> Loans { get; set; } = null!;

    }
}
