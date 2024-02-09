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
    public class LoanController : ControllerBase
    {
        public readonly LibraryContext _context;
        public LoanController(LibraryContext context) => _context = context;

        //Create items
        //Skapa en bok

        //Handle loans
        [HttpPost("PostLoan")]
        public async Task<ActionResult<Loan>> PostLoan(LoanRequest request)
        {
            Loan loan = new Loan()
            {
                BookId = request.BookId,
                BorrowerId = request.BorrowerId,
                LoanDate = request.LoanDate,
                //ReturnDate = request.ReturnDate,
            };
            var books = await _context.Books.FirstOrDefaultAsync(b => b.Id == loan.BookId);
            var borrower = await _context.Borrowers.FirstOrDefaultAsync(b => b.Id == loan.BorrowerId);

            if (books == null && borrower == null)
                return NotFound("Either book or person does not exist.");
            else if (books == null)
                return NotFound("The book does not exist.");
            else if (borrower == null)
                return NotFound("The person does not exist.");
            else if (books.IsBoorowed != false)
                return NotFound($"The book \"{books.Title}\" is borrowed and not yet returned.");
            else
            {
                books.IsBoorowed = loan.IsActive;
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return loan;
            }
        }

        [HttpGet("GetAllLoans")]
        public async Task<IEnumerable<Loan>> GetAllLoans() => await _context.Loans.ToListAsync();

        [HttpPatch("ReturnBook")]
        public async Task<ActionResult> AddReturnDate(LoanIdRequest loanId)
        {
            var loan = _context.Loans.FirstOrDefault(l => l.Id == loanId.Id);
            if (loan is null)
                return NotFound("Either the loan does not exist or you have entered an invalid id. \nPlease enter the id of the loan.");
            if (loan.IsActive == false)
                return NotFound($"The book is not borrowed.");

            else
            {
                loan.ReturnDate = loanId.DateReturned;
                loan.IsActive = false;

                var book = _context.Books.FirstOrDefault(b => b.Id == loan.BookId);
                book.IsBoorowed = loan.IsActive;

                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
        }
        //Proposed edit: Replace loanId with bookId so that the 
        //database updates the book and not the loan.

        //Proposed edit: The database only shows active loans.
        //When the book has been returned the loan is deleted from the table.

        //Print information on books
        //Lista alla böcker 
    }
}