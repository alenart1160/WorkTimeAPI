using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkTimeAPI.Data;
using WorkTimeAPI.Model;

namespace WorkTimeAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        private readonly string? jwtKey;

        public UsersController(UserContext context, IConfiguration configuration) : base()
        {
            _context = context;
            jwtKey = configuration["Jwt:Key"];
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/reset/{email}")]
        public async Task<IActionResult> PutNewPassword(string email, User user)
        {
            if (email != user.Email)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEmailExists(email))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var checkUserExist = await _context.Users.Where(x => x.Login == user.Login).ToListAsync();
            if (checkUserExist.Count() == 0)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            return Conflict();


        }
        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/login")]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            PasswordHasher<User> _passwordHasher = new();
            if (!_context.Users.Any())
            {
                return NotFound();
            }
            User? checkUserExist = await _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login);
            if (checkUserExist == null)
            {
                return NotFound();
            }

            var result = PasswordVerificationResult.Failed;
            if (checkUserExist != null && checkUserExist.Password != null && user.Password != null)
            {
                result = _passwordHasher.VerifyHashedPassword(user, checkUserExist.Password, user.Password);
            }
            if (result == PasswordVerificationResult.Failed)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(jwtKey))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Brak klucza JWT w konfiguracji.");
            }
            if (result == PasswordVerificationResult.Success && checkUserExist != null)
            {

                User? userFinded = _context.Users.Find(checkUserExist.Id);
                await _context.SaveChangesAsync();
                if (userFinded == null)
                {
                    return NotFound();
                }

                if (userFinded.Id > 0)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(jwtKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.Email)
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwt = tokenHandler.WriteToken(token);

                    return Ok(new { token = jwt, id = userFinded.Id });
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }

        }
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        private bool UserEmailExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }
    }
}
