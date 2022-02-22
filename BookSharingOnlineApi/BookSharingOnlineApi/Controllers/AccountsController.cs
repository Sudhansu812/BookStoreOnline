using BookSharingOnlineApi.Models.Dto.UserDto;
using BookSharingOnlineApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookSharingOnlineApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManagementService _service;

        public AccountsController(IAccountManagementService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<bool> Register(UserRegisterDto userRegisterDto)
        {
            return await _service.Register(userRegisterDto);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(UserLogInDto userLogInDto)
        {
            if(userLogInDto == null)
            {
                return BadRequest("Invalid client request");
            }
            UserReadDto user = await _service.LogIn(userLogInDto);
            if(user == null)
            {
                return Unauthorized();
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("verySuperSecretKey@123"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5001",
                audience: "http://localhost:5001",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(new { User=user.UserId, Token = tokenString, LogInStatus=true });
        }
    }
}