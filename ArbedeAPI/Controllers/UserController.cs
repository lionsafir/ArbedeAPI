using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ArbedeAPI.Models;
using ArbedeAPI.DTOs;
using ArbedeAPI.Services;

namespace ArbedeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _userService.RegisterAsync(dto.Username);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpPost("login-by-username")]
        public async Task<IActionResult> Login([FromBody] LoginUsernameDto dto)
        {
            var result = await _userService.LoginAsync(dto.Username);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }


        


    }

}