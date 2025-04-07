using ApiPeliculas.Entities;
using ApiPeliculas.Entities.DTOs;
using ApiPeliculas.Exceptions;
using ApiPeliculas.Repositories.Interfaces;
using ApiPeliculas.Services.Interfaces;
using ApiPeliculas.Shared.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ApiPeliculas.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenProvider _tokenProvider;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            RoleManager<IdentityRole> roleManager,
            TokenProvider tokenProvider,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
        }
        public async Task<ReadUserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new NotFoundException($"User with ID {id} not found.", 404);
            var roles = await _userManager.GetRolesAsync(user);
            var userDto = _mapper.Map<ReadUserDto>(user);
            userDto.Role = roles.FirstOrDefault() ?? "No Role Assigned";
            return userDto;
        }

        public async Task<ReadUserDto> CreateUserAsync(RegisterUserDto registerUserDto)
        {
            var existingUser = await _userManager.FindByNameAsync(registerUserDto.UserName);
            if (existingUser != null)
            {
                throw new BadRequestException($"Username '{registerUserDto.UserName}' is already taken.");
            }

            var user = _mapper.Map<AppUser>(registerUserDto);

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"User creation failed: {errors}");
            }

            // Verificar si el rol especificado existe, si no lo creamos
            if (!await _roleManager.RoleExistsAsync(registerUserDto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(registerUserDto.Role));
            }

            // Asignar rol al usuario
            await _userManager.AddToRoleAsync(user, registerUserDto.Role);

            return _mapper.Map<ReadUserDto>(user);
        }


        public async Task<LoginResponseDto> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid username or password.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedException("Invalid username or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
           
            var token = _tokenProvider.GenerateJwtToken(user, roles.ToList());

       
            var response = new LoginResponseDto
            {
                Token = token,
                Expiration = _tokenProvider.GetExpirationTime(),
                User = new ReadUserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Role = roles.FirstOrDefault() ?? "User"
                }
            };

            return response;
        }
    }
}
