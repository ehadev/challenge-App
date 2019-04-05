using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Challenge_App.Data;
using Challenge_App.Repo.DTO.User;
using Challenge_App.Repo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Challenge_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository repo, IConfiguration configuration)
        {
            _configuration = configuration;
            _repo = repo;
        }

        // GET: api/Challenges
        [HttpGet]
        public async Task<IActionResult> GetAuth()
        {
            return StatusCode(201);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistrationDTO userForRegistration)
        {

            //TO:DO Validate request

            userForRegistration.Username = userForRegistration.Username.ToLower();

            if (await _repo.UserExists(userForRegistration.Username))
                return BadRequest("Username already exists");

            var newUser = new User
            {
                Username = userForRegistration.Username
            };

            var createdUser = await _repo.Register(newUser, userForRegistration.Password);
            _repo.SaveChanges(); 

            //TO:DO CreatedAtRoute
            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLogin)
        {
            var user = await _repo.Login(userForLogin.Username.ToLower(), userForLogin.Password);

            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            User userForStorage = new User()
            {
                Username = userForLogin.Username
            };

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                userForStorage
            });

        }
    }
}