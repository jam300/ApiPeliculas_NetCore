using ApiPeliculas.Entities;
using ApiPeliculas.Entities.DTOs;
using ApiPeliculas.Services;
using ApiPeliculas.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiPeliculas.Controllers
{
    
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}", Name = "GetUserByIdAsync")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisUserAsync([FromBody] RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
                return BadRequest("The object registerUserDto cannot be null.");

            var user = await _userService.CreateUserAsync(registerUserDto);
            return CreatedAtRoute("GetUserByIdAsync", new { id = user.Id }, user);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
                return BadRequest("The object loginUserDto cannot be null.");

            var user = await _userService.LoginAsync(loginUserDto);
            return Ok(user);
        }    

    }
}
