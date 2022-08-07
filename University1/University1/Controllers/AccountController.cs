using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University1.DataAccess;
using University1.Helpers;
using University1.Models.DataModels;

namespace University1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly JwtSettings _jwtSettings;

        public AccountController(UniversityDBContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }
        // TODO: Change to real users in the DB
        // These are hardcoded users
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "marting@imaginagroup.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id = 2,
                Email = "pepe@imaginagroup.com",
                Name = "User1",
                Password = "pepe"
            }
        };
        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {

                var Token = new UserTokens();
                // tenemos que buscar una lógica más avanzada para obtener el usuario pasándole el id o pasándole el nombre en este caso ->
                //var searchUser = await _context.Users. ;
                var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if (Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid(),

                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Creedentials");
                }
                return Ok(Token);
            } catch (Exception exception)
            {
                throw new Exception("Get Token Error", exception);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin , User")]
        public IActionResult userVerification(UserLogins userLogins)
        {
           
            var verifiedUser = from user in _context.Users where userLogins.Equals(user.Name) && userLogins.Equals(user.LastName) select user;
            return (IActionResult)verifiedUser;
        } 

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
        }
    } 
