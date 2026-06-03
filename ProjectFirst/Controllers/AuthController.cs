using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFirst.Data;
using ProjectFirst.Models;
using ProjectFirst.DTOs.AuthDTO;

namespace ProjectFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AuthController(AppDbContext db)
        {
           this._db = db;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _db.Users.FirstOrDefaultAsync( u => u.Username == loginDTO.name);

            if (user == null) 
            {
                return BadRequest("User Does not Exist");
            }

            
            if (VarifyPassword(loginDTO.Password, user.HashedPassword)) 
            {
                return BadRequest("Username or Passward invalid");
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO userDTO)
        {
            //if(user == null)
            //{
            //    return "All fields are required";
            //}

            //unique email
            if (await _db.Users.AnyAsync( u => u.Useremail == userDTO.email ))
            {
                return BadRequest("user llready exist.");

            }

            //unique mobile
            if (await _db.Users.AnyAsync(u => u.MobileNo == userDTO.MobileNo))
            {
                return BadRequest("user llready exist.");

            }
           

            var user = new User
            {
                Useremail = userDTO.email,
                MobileNo = userDTO.MobileNo,
                Username = userDTO.name,
                HashedPassword = HashPassword(userDTO.Password),

            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return Ok(user);
        }

        private string HashPassword(string passward)
        {
            return BCrypt.Net.BCrypt.HashPassword(passward);
        }

        private bool VarifyPassword(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
    }
}
