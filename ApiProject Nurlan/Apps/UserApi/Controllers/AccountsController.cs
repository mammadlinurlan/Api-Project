using ApiProject_Nurlan.Apps.UserApi.DTOs.AccountDTOs;
using ApiProject_Nurlan.Data.Entities;
using ApiProject_Nurlan.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Apps.UserApi.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
          
            _configuration = configuration;
        }

        //[HttpGet("roles")]
        //public async Task<IActionResult> CreateRoles()
        //{
        //    //var result = await _roleManager.CreateAsync(new IdentityRole("Member"));
        //    //result = await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    //result = await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

        //    AppUser admin = new AppUser
        //    {
        //        FullName = "Super Admin",
        //        UserName = "SuperAdmin",
        //    };

        //    var resultAdmin = await _userManager.CreateAsync(admin, "Admin123");

        //    var resultRole = await _userManager.AddToRoleAsync(admin, "SuperAdmin");

        //    return Ok();
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user = await _userManager.FindByNameAsync(registerDto.UserName);

            if (user != null)
                return StatusCode(409);

            user = new AppUser
            {
                UserName = registerDto.UserName,
                FullName = registerDto.FullName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded)
                return BadRequest(result.Errors);


            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
                return NotFound();

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return NotFound();


            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("FullName",user.FullName),

            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList());

            string keyStr = "76a19d4f-b334-4952-8cae-31c6233cefe6";

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyStr));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: creds,
                    expires: DateTime.Now.AddDays(3),
                    issuer: "https://localhost:44305/",
                    audience: "https://localhost:44305/"
                );

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

           


            return Ok(new { token = tokenStr });
        }


        [Authorize(Roles = "Member")]
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            AccountGetDto accountDto = _mapper.Map<AccountGetDto>(user);

            return Ok(accountDto);
        }



    }
}
