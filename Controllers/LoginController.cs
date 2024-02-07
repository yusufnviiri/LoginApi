using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;


    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        
        private readonly ApplicationDbContext _db;

        private readonly UserActions _userActions = new();

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult>Login([FromBody] User user)
        {
         var verifiedUser = await _userActions.GetUser(user, _db);
            if (verifiedUser is null)
            {
                return BadRequest("Invalid credentials");
            }
            if(verifiedUser.Password.ToLower()==user.Password.ToLower()) {

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretoftheuniverse"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claimer = new[] {new Claim(ClaimTypes.NameIdentifier,user.UserName),new Claim(ClaimTypes.Role,user.Password)};
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims:claimer,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new UserToken { Token = tokenString });

            }
            return Unauthorized();

        }
     
    }

