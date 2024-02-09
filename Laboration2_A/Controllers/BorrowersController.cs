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
    public class BorrowersController : ControllerBase
    {
        public readonly LibraryContext _context;
        public BorrowersController(LibraryContext context) => _context = context;
       
        //Skapa en låntagare
        [HttpPost("PostBorrower")]
        public async Task<ActionResult<Borrower>> PostBorrower(BorrowerRequest request)
        {
            Borrower borrower = new Borrower()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                SocialSecurityNumber = request.SocialSecurityNumber,
            };
            var ssn = request.SocialSecurityNumber.ToString();
            if (request.SocialSecurityNumber.ToString().Count() < 9)
                return BadRequest($"The Social Security Nuber must contain 9 digits. You have entered {request.SocialSecurityNumber.ToString().Count()}");
            else if (request.SocialSecurityNumber.ToString().Count() > 9)
                return BadRequest($"The Social Security Number must contain 9 digits. You have entered {request.SocialSecurityNumber.ToString().Count()}");
            else
            {
                foreach (var person in _context.Borrowers)
                {
                    if (person.SocialSecurityNumber == borrower.SocialSecurityNumber)
                        return BadRequest("The SSN is already taken.");
                }
                _context.Add(borrower);
                await _context.SaveChangesAsync();
                return borrower;
            }
        }
        //Fetch all borrowes
        [HttpGet("GetAllBorrowers")]
        public async Task<IEnumerable<Borrower>> GetAllBorrowers() => await _context.Borrowers.ToListAsync();

        //Ta bort en låntagare 
        [HttpDelete("DeleteBorrower")]
        public async Task<ActionResult> DeleteBorrower(BorrowerIdRequest borrowerId)
        {
            //identify borrower
            var borrower = _context.Borrowers.FirstOrDefault(b => b.Id == borrowerId.Id);

            if (borrower == null)
                return NotFound("The person does not exist.");
            else
            {
                _context.Remove(borrower);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
        }
    }
}