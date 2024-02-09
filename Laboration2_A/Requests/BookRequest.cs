using Laboration2_A.Models;
using Microsoft.EntityFrameworkCore;

namespace Laboration2_A.Requests
{
    public class BookRequest
    {
        public string Title { get; set; }
        public int ISBN { get; set; }
        public int ReleaseYear { get; set; }
    }
}
