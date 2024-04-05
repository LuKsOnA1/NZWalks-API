using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs.Authentication;
using NZWalks.API.Repositories.Identity.Abstract;

namespace NZWalks.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _repository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            var identityUser = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, model.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest("Something went wrong, please make sure input information is valid!");
            }

            if (model.Roles != null && model.Roles.Length != 0)
            {
                identityResult = await _userManager.AddToRolesAsync(identityUser, model.Roles);

                if (identityResult.Succeeded)
                {
                    return Ok("User was registered! Please login.");
                }
            }

            return BadRequest("Something went wrong, please try again later!");
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);

            if (user == null)
            {
                return BadRequest("Email or Password is not valid. Please try valid one");
            }

            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, model.Password);

            if (checkPasswordResult)
            {
                // Get roles for user
                var roles = await _userManager.GetRolesAsync(user);

                if (roles != null)
                {
                    // Create token
                    var jwtToken = _repository.CreateJWTToken(user, roles.ToList());

                    var response = new LoginResponseDTO
                    {
                        JwtToken = jwtToken
                    };

                    return Ok(response);
                }

                return BadRequest("Something went wrong, please try again later!");
            }

            return BadRequest("Email or Password is not valid. Please try valid one");
        }
    }
}
