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
        private readonly ILogger<AccountController> _logger;

        public AccountController(UniversityDBContext context, JwtSettings jwtSettings, ILogger<AccountController> logger)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _logger = logger;
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
            _logger.LogWarning($"{nameof(AccountController)} - {nameof(GetToken)} Warning Level Log");
            _logger.LogError($"{nameof(AccountController)} - {nameof(GetToken)} Error Level Log");
            _logger.LogCritical($"{nameof(AccountController)} - {nameof(GetToken)} Critical Level Log");
            try
            {

                var Token = new UserTokens();
                // Search a user in cotext with LINQ
                var searchUser = (from user in _context.Users
                                 where user.Name == userLogin.UserName && user.Password == userLogin.Password select user).FirstOrDefault();
             
                //var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if (searchUser == null)
                {
                    //var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
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
        public IActionResult userVerification(UserLogins userLogins)       {
                        

            var verifiedUser = (from user in _context.Users where userLogins.Equals(user.Name) && userLogins.Equals(user.LastName) select user).FirstOrDefault();
            return (IActionResult)verifiedUser;
        } 

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult GetUserList()
        {          
            
            _logger.LogWarning($"{nameof(AccountController)} - {nameof(GetUserList)} Warning Level Log");
            _logger.LogError($"{nameof(AccountController)} - {nameof(GetUserList)} Error Level Log");
            _logger.LogCritical($"{nameof(AccountController)} - {nameof(GetUserList)} Critical Level Log");

            return Ok(Logins);
        }
        }
    } 
