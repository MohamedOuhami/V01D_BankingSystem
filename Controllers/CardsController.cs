using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CardsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.
            Include(c => c.Accounts)
            .ToListAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{cardNumber}")]
        public async Task<ActionResult<Card>> GetCard(string cardNumber, [FromQuery] string cardPin)
        {
            string decryptedPINString;
            var card = await _context.Cards.Include(c => c.Accounts).FirstOrDefaultAsync(card => card.CardNumber == cardNumber);

            // Getting the private Key
            string privateKey = Encryption.privateKey;

            using (RSA rsa = RSA.Create())
            {

                rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
                byte[] decryptedPINByte = rsa.Decrypt(Convert.FromBase64String(cardPin),RSAEncryptionPadding.OaepSHA256);
                decryptedPINString = Encoding.UTF8.GetString(decryptedPINByte);

            }

            if ( decryptedPINString != null && card.PIN == decryptedPINString)
            {

                return Ok(card);
            }
            else
            {
                return BadRequest("Invalid PIN");
            }

        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(CardInputModel input)
        {
            // Fetch the preexisting accounts
            var existingAccounts = await _context.Accounts
                .Where(a => input.AccountsNumbers.Contains(a.AccountNumber))
                .ToListAsync();

            // Check if all accounts exist
            if (existingAccounts.Count != input.AccountsNumbers.Count)
            {
                return BadRequest("One or more accounts do not exist.");
            }

            // Create the card
            var card = new Card
            {
                CardNumber = input.CardNumber,
                ExpiryDate = input.ExpiryDate,
                Type = input.Type,
                PIN = input.PIN,
                CVV = input.CVV,
                Status = input.Status,
                Accounts = existingAccounts // Link the preexisting accounts
            };

            // Add the card to the database
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            // Return the created card
            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
