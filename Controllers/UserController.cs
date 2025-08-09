using EcommerceAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using EcommerceAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService TokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = TokenService;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            var user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.UserName,
            };

            var result = await _userManager.CreateAsync(user,request.Password);

            if (result.Succeeded) {
                
                if (await _userManager.Users.CountAsync() == 1)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync("Customer"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Customer"));
                    }
                    await _userManager.AddToRoleAsync(user, "Customer");
                }

                return Ok(new { Message = "User Created successfully" });
            }

            foreach (var error in result.Errors) {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);  
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) { return Unauthorized(new { Message = "Invalid Username" }); }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) { return Unauthorized(new { Message = "Invalid Passwword" }); }

            var token = await _tokenService.GenerateJwtToken(user);
            return Ok(new { Token = token });

        }
    }
}
