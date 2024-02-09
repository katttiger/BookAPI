namespace Laboration2_A.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ISBN { get; set; }
        public int ReleaseYear { get; set; }
        public bool IsBoorowed { get; set; }
      
        //Ett lån kan bara ha en bok
       public Loan Loan { get; set; }
    }
}
