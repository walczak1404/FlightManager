using FlightManager.Core.Domain.Entities;
using FlightManager.Core.DTO;
using FlightManager.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlightManager.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenService _jwtService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtTokenService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }


        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="registerDTO">Object with new user data</param>
        /// <returns>Registered user's email, tokens and their expiration times</returns>
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> PostRegister(RegisterRequest registerDTO)
        {
            ////Validation
            //if (!ModelState.IsValid)
            //{
            //    string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            //    return Problem(detail: errorMessage, statusCode: StatusCodes.Status400BadRequest);
            //}


            //Create user
            AppUser user = new AppUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                //sign-in
                await _signInManager.SignInAsync(user, isPersistent: false);

                var authenticationResponse = _jwtService.CreateJwtToken(user);
                user.RefreshToken = authenticationResponse.RefreshToken;

                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTimeUTC;
                await _userManager.UpdateAsync(user);


                return Ok(authenticationResponse);
            }
            else
            {
                string errorMessage;
                if (result.Errors.First().Code == "DuplicateUserName")
                {
                    errorMessage = "Email jest ju¿ u¿ywany";
                } else
                {
                    errorMessage = result.Errors.Select(e => e.Description).First();
                }

                return Problem(errorMessage, statusCode: StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// Signs in user
        /// </summary>
        /// <param name="loginDTO">Data of signed in user</param>
        /// <returns>Signed in user's email, tokens and their expiration times</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> PostLogin(LoginRequest loginDTO)
        {
            //Validation
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(detail: errorMessage, statusCode: StatusCodes.Status400BadRequest);
            }


            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                AppUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null)
                {
                    return Problem("Coœ posz³o nie tak", statusCode: StatusCodes.Status500InternalServerError);
                }

                var authenticationResponse = _jwtService.CreateJwtToken(user);

                user.RefreshToken = authenticationResponse.RefreshToken;

                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTimeUTC;
                await _userManager.UpdateAsync(user);


                return Ok(authenticationResponse);
            }

            else
            {
                return Problem("B³êdny email lub has³o", statusCode: StatusCodes.Status400BadRequest);
            }
        }


        /// <summary>
        /// Signs out user
        /// </summary>
        [HttpGet("logout")]
        public async Task<IActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        /// <summary>
        /// Refreshes user's JWT token if valid refresh token is provided
        /// </summary>
        /// <param name="tokenModel">Object containing expired token and refresh token</param>
        /// <returns>Refreshed tokens with expiration dates and user email</returns>
        [HttpPut("refresh-token")]
        public async Task<ActionResult<AuthenticationResponse>> PutToken(TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                return BadRequest("Nie podano tokenów");
            }

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFromJwtToken(tokenModel.Token);
            if (principal == null)
            {
                return BadRequest("B³êdny token");
            }

            string? email = principal.FindFirstValue(ClaimTypes.Email);

            AppUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpirationDateTime <= DateTime.UtcNow)
            {
                return BadRequest("B³êdny token odœwie¿aj¹cy");
            }

            AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user);

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTimeUTC;

            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
        }
    }
}
