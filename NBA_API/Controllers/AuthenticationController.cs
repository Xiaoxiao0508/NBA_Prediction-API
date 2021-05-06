using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NBA_API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace NBA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        //checked the user exit or not,create the user if not exist,create the roles if not exist in the database,add user as user or admin

        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExist = await userManager.FindByNameAsync(model.Username);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "Unsuccessful" });
            ApplicationUser user = new ApplicationUser()
            {
                //Email = model.Email,
                //what is the stamp?
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "Unsuccessful" });
            }
            //if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //if (!await roleManager.RoleExistsAsync(UserRoles.User))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            //if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await userManager.AddToRoleAsync(user, UserRoles.User);
            return Ok(new AuthResponse { Status = "Success", Message = "User created Successfully" });
        }
        //[HttpPost]
        //[Route("RegisterAdmin")]

        //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        //{
        //    var userExist = await userManager.FindByNameAsync(model.Username);
        //    if (userExist != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Unsuccessful" });
        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        Email = model.Email,
        //        //what is the stamp?
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Username
        //    };
        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Unsuccessful" });
        //    }
        //    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //    if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        //    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        //        await userManager.AddToRoleAsync(user, UserRoles.Admin);
        //    return Ok(new Response { Status = "Success", Message = "User created Successfully" });
        //}

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                //var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                //foreach (var userRole in userRoles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                //}
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(15),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                 );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = user.UserName
                });
            }
            return Unauthorized();

        }
    }
}
