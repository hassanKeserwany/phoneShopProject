using API.Dtos.identityDto;
using API.Extenstions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenservice,
            IMapper mapper)
            
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenService = tokenservice;
           this._mapper = mapper;
        }


        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimPrinciple(HttpContext.User);

            if (user == null)
            {
                // If user is not found, return a NotFound result or handle it appropriately.
                return NotFound("User not found.");
            }

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DispalyName = user.DisplayName
            };
        }


        [HttpGet("checkEmailExists")] //check if email already exist
        public async Task<ActionResult<bool>> checkEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email)!=null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByUserByClaimsPrincipleWithAddressAsync(HttpContext.User);

            if (user == null || user.address == null)
            {
                return NotFound(); // Or any other appropriate result for when the user or address is not found.
            }

            var addressDto = _mapper.Map<Address, AddressDto>(user.address);
            return addressDto;
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {

            var user = await _userManager.FindByUserByClaimsPrincipleWithAddressAsync(HttpContext.User);

           
            user.address=_mapper.Map<AddressDto,Address>(address);
            var result = await _userManager.UpdateAsync(user);
            
            if(result.Succeeded)
            {
                return Ok(_mapper.Map<Address,AddressDto>(user.address));
            }
            return BadRequest("problem upadating the user");
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized();
            }
            var results = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!results.Succeeded)
            {

                return Unauthorized();
            }
            return new UserDto
            {
                Email = user.Email,
                DispalyName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };

        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(checkEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return BadRequest("Email address is in use");
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName= registerDto.DisplayName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return new UserDto
            {
                DispalyName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
            };

        }

    }

}


   // {"displayName": "hassan",
 // "email": "hassan@gmail.com",
 // "password": "Pa$$w0rd"}
