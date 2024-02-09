using Laboration2_A.Data;
using Laboration2_A.IdRequest;
using Laboration2_A.Models;
using Laboration2_A.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Laboration2_A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly LibraryContext _context;
        public BookController(LibraryContext context) => _context = context;

        //Create items
        //Skapa en bok
        [HttpPost("PostBook")]
        public async Task<ActionResult<Book>> PostBook(BookRequest request)
        {
            Book book = new Book()
            {
                Title = request.Title,
                ISBN = request.ISBN,
                ReleaseYear = request.ReleaseYear,
            };

            if (book.ISBN.ToString().Count() < 9)
                return BadRequest($"The ISBN must have 9 digits. You have entered {book.ISBN.ToString().Count()}.");
            else if (book.ISBN.ToString().Count() > 9)
                return BadRequest($"The ISBN must have 9 digits. You have entered {book.ISBN.ToString().Count()}.");
            else
            {
                foreach (var b in _context.Books)
                {
                    if (b.ISBN == book.ISBN)
                        return BadRequest("The ISBN is already taken.");
                }
                _context.Add(book);
                await _context.SaveChangesAsync();
                return book;
            }
        }
        [HttpGet("GetAllBooks")]
        public async Task<IEnumerable<Book>> GetAllBooks() => await _context.Books.ToListAsync();

        //Hämta information om en specifik bok
        [HttpGet("GetBookById")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.FindAsync<Book>(id);
            if (book == null)
                return NotFound("The book does not exist.");
            return book;

        }

        //Remove items
        //Ta bort en bok 
        [HttpDelete("DeleteBook")]
        public async Task<ActionResult> DeleteBook(BookIdRequest bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId.Id);
            if (book == null)
                return NotFound("The book does not exist.");
            else
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
        }
    }
}