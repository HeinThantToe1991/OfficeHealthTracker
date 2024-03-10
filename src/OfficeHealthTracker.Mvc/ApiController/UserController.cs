using OfficeHealthTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Interfaces;
using System;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Mvc.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing.");
            }

            try
            {
                _userService.RegisterUser(user);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Invalid username or password.");
            }
           
            var token =GenerateToken.GenerateJwtToken(loginModel.Username);
            return Ok(new { token });
        }
    }
}
