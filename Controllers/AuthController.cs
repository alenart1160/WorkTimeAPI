//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//[ApiController]
//[Route("api/[controller]")]
//public class AuthController : ControllerBase
//{
//    private readonly string jwtKey;

//    public AuthController(IConfiguration configuration)
//    {
//        jwtKey = configuration["Jwt:Key"];
//    }

//    [HttpPost("login")]
//    public IActionResult Login([FromBody] LoginRequest request)
//    {

//        if (request.Username == "admin" && request.Password == "password")
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.UTF8.GetBytes(jwtKey);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.Name, request.Username)
//                }),
//                Expires = DateTime.UtcNow.AddHours(1),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            var jwt = tokenHandler.WriteToken(token);

//            return Ok(new { token = jwt });
//        }

//        return Unauthorized();
//    }

//    [HttpPost("register")]
//    [AllowAnonymous]
//    public IActionResult Register([FromBody] RegisterRequest request)
//    {
//        // Tu zapisz użytkownika do DB
//        return Ok(new { message = "User registered (mock)" });
//    }
//}

//public class LoginRequest
//{
//    public string Username { get; set; }
//    public string Password { get; set; }
//}

//public class RegisterRequest
//{
//    public string Username { get; set; }
//    public string Password { get; set; }
//}
