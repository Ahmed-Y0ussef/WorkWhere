using Application.DTO.Account;
using Application.Services.Account;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

namespace WorkWhere.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _accountService;

        public AuthenticationController(AuthenticationService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var result = await _accountService.Login(model);
            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result);

        }
        [HttpPost("check-email")]
        public async Task<IActionResult> CheckEmail(CheckEmailDTO model)
        {
            var result = await _accountService.CheckEmail(model);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO model)
        {

            var result = await _accountService.Register(model);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);

        }
        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDTO model)
        {
            var result =  await _accountService.ConfirmEmail(model);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);

        }

        [HttpPost("resend-email-confirmation-link/{email}")]
        public async Task<IActionResult> ResendEmailConfirmationLink(string email)
        {
            var result = await _accountService.ResendEmailConfirmationLink(email);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("forgot-email-or-password/{email}")]
        public async Task<IActionResult> ForgotUserNameOrPassword(string email)
        {
            var result = await _accountService.ForgotUserNameOrPassword(email);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var result = await _accountService.ResetPassword(model);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }


        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDTO>> RefreshUserToken()
        {
            var result = await _accountService.RefreshUserToken(User);
            if(!result.Success)
            {
                return Unauthorized(result.Message);  
            }
            return Ok(result);
        }




    }
}
