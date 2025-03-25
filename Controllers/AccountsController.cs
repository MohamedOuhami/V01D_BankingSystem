using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost("{accountNumber}/withdraw")]
        public async Task<IActionResult> WithdrawMoney(string accountNumber, [FromBody] decimal amount)
        {
            // Begin transaction to ensure atomic operations
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Fetch the account using the account number
                    var account = await _context.Accounts
                        .Where(a => a.AccountNumber == accountNumber)
                        .FirstOrDefaultAsync();

                    // Check if the account exists
                    if (account == null)
                        return NotFound("Account not found");

                    // Validate the withdrawal amount
                    if (amount <= 0)
                        return BadRequest("Amount must be positive");

                    // Check if the account has sufficient funds
                    if (account.Balance < amount)
                        return BadRequest("Insufficient funds");

                    // Update the account balance
                    account.Balance -= amount;

                    // Create a new Operation entry for the transaction
                    var operation = new Operation
                    {
                        Amount = amount,
                        Timestamp = DateTime.Now,
                        Type = "Withdrawal",
                        AccountId = account.Id // Ensure the operation is associated with the correct account
                    };

                    // Add the operation to the database
                    await _context.Operations.AddAsync(operation);

                    // Save changes to both the account balance and the operation
                    await _context.SaveChangesAsync();

                    // Commit the transaction to apply the changes
                    await transaction.CommitAsync();

                    // Return success response with the new balance
                    return Ok(new
                    {
                        success = true,
                        newBalance = account.Balance
                    });
                }
                catch (Exception ex)
                {
                    // Rollback transaction if something goes wrong
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }
        }


        [HttpGet("operations/{accountNumber}")]
        public async Task<IActionResult> Get5Operations(string accountNumber)
        {
            // Fetch account along with its operations asynchronously
            var account = await _context.Accounts
                .Include(a => a.Operations)
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            // Check if the account exists
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            // If the account has no operations
            if (account.Operations == null || !account.Operations.Any())
            {
                return NotFound("No operations found for this account.");
            }

            // Retrieve the last 5 operations, ordered by a date field (assuming there's a Date property)
            var last5Operations = account.Operations
                .OrderByDescending(o => o.Timestamp)  // Order by the operation date (or another appropriate field)
                .Take(5)  // Get the last 5 operations
                .ToList();

            // Return the last 5 operations
            return Ok(last5Operations);
        }


        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
