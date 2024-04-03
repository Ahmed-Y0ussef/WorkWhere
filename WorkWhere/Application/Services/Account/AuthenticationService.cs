using Application.DTO.Account;
using Application.Services.Email;
using Application.Services.Jwt;
using Core.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Application.Services.Account
{
    public class AuthenticationService
    {
        private readonly JWTService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly EmailService _emailService;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ResultDTO<UserDTO> _resultDTO;

        public AuthenticationService(JWTService jwtService, SignInManager<User> signInManager, UserManager<User> userManager, EmailService emailServe, IConfiguration config, RoleManager<IdentityRole> roleManager, ResultDTO<UserDTO> resultDTO)
        {
            _jwtService = jwtService;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailServe;
            _config = config;
            _roleManager = roleManager;
            _resultDTO = resultDTO;
        }
        public async Task<ResultDTO<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Invalid Email or Password";
                _resultDTO.Title = "Login Failed";
                return _resultDTO;
            }
            if (!user.EmailConfirmed)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Please confirm your email";
                _resultDTO.Title = "Login Failed";
                return _resultDTO;

            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Invalid Email or Password";
                _resultDTO.Title = "Login Failed";
                return _resultDTO;
            }

            var userDto = CreateApplicationUserDto(user);

            _resultDTO.Success = true;
            _resultDTO.Message = "Login successfully";
            _resultDTO.Title = "Login success";
            _resultDTO.Data = userDto;
            return _resultDTO;

        }
        
        public async Task<ResultDTO<UserDTO>> CheckEmail(CheckEmailDTO model)
        {
            if (await CheckEmailExistsAsync(model.Email) || await CheckNIDExistsAsync(model.NId))
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Email or National ID already registered";
                return _resultDTO;
            }
            else
            {
                _resultDTO.Success = true;
                return _resultDTO;
            }
        }

        public async Task<ResultDTO<UserDTO>> Register(RegisterDTO model)
        {
            if (await CheckEmailExistsAsync(model.Email))
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Email or Username already registered";
                return _resultDTO;
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Password is required";
                return _resultDTO;
            }

            var userToAdd = new User
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                Email = model.Email.ToLower(),
                UserName = Guid.NewGuid().ToString(),
                PhoneNumber = model.PhoneNumber.ToLower(),
                NId = model.NId,
            };
            string personalImg = model.PersonalImg.Split(',')[1];
            string nationalId = model.NImg.Split(',')[1];

            userToAdd.PersonalImg = Convert.FromBase64String(personalImg); ;
            userToAdd.NImg = Convert.FromBase64String(nationalId);

            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if (!result.Succeeded)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Failed to create user";
                return _resultDTO;
            }

            var roleName = model.IsAdmin ? "Admin" : "Guest";
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = $"Role '{roleName}' not found";
                return _resultDTO;
            }

            var roleAssignResult = await _userManager.AddToRoleAsync(userToAdd, roleName);
            if (!roleAssignResult.Succeeded)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = $"Failed to assign role '{roleName}' to the user";
                return _resultDTO;
            }

            try
            {
                if (await SendConfirmEmailAsync(userToAdd))
                {
                    _resultDTO.Success = true;
                    _resultDTO.Message = "Account created successfully, please confirm your email address";
                    return _resultDTO;
                }
                _resultDTO.Success = false;
                _resultDTO.Message = "Failed to send email, please contact admin";
                return _resultDTO;
            }
            catch (Exception)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Failed to send email, please contact admin";
                return _resultDTO;
            }
        }

        private async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    return stream.ToArray();
                }
            }
            return null;
        }

        public async Task<ResultDTO<UserDTO>> ConfirmEmail(ConfirmEmailDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user == null)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "this email has not registered yet";
                return _resultDTO;
            }
            if (user.EmailConfirmed == true)
            {

                _resultDTO.Success = false;
                _resultDTO.Title = "Email Already confirmed";
                _resultDTO.Message = "Your Email address is confirmed before. You can login Now";
                return _resultDTO;
            }
            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
                if (result.Succeeded)
                {

                    _resultDTO.Success = true;
                    _resultDTO.Title = "Email confirmed";
                    _resultDTO.Message = "Your Email address is confirmed. You can login Now";

                    return _resultDTO;
                }

                _resultDTO.Success = false;
                _resultDTO.Title = "Error Happen";
                _resultDTO.Message = "Invalid token. Please try again.";
                return _resultDTO;
            }
            catch (Exception)
            {
                _resultDTO.Success = false;
                _resultDTO.Title = "Error Happen";
                _resultDTO.Message = "Invalid token. Please try again.";
                return _resultDTO;
            }

        }


        public async Task<ResultDTO<UserDTO>> ResendEmailConfirmationLink(string email)
        {
            if (string.IsNullOrEmpty(email))
            {

                _resultDTO.Success = false;
                _resultDTO.Message = "Invalid email address";

                return _resultDTO;
            }
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _resultDTO.Success = false;
                _resultDTO.Title = "Not Registered Email";
                _resultDTO.Message = "This email address has not been registered yet";
                return _resultDTO;
            }
            if (user.EmailConfirmed == true)
            {

                _resultDTO.Success = false;
                _resultDTO.Title = "Error Happen";
                _resultDTO.Message = "This email address was confirmed before. Please login now";

                return _resultDTO;
            }

            try
            {
                if (await SendConfirmEmailAsync(user))
                {

                    _resultDTO.Success = true;
                    _resultDTO.Title = "Email confirmed";
                    _resultDTO.Message = "Your Email address is confirmed. You can login Now";
                    return _resultDTO;
                }

                _resultDTO.Success = false;
                _resultDTO.Title = "Send Failed";
                _resultDTO.Message = "Failed to send email. Please contact the admin";

                return _resultDTO;
            }
            catch (Exception)
            {
                _resultDTO.Success = false;
                _resultDTO.Title = "Send Failed";
                _resultDTO.Message = "Failed to send email. Please contact the admin";
                return _resultDTO;
            }

        }

        public async Task<ResultDTO<UserDTO>> ForgotUserNameOrPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {

                _resultDTO.Success = false;
                _resultDTO.Message = "Invalid email address";
                return _resultDTO;
            }
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _resultDTO.Success = false;
                _resultDTO.Title = "Not Registered Email";
                _resultDTO.Message = "This email address has not been registered yet";
                return _resultDTO;
            }
            if (user.EmailConfirmed == false)
            {

                _resultDTO.Success = false;
                _resultDTO.Message = "Please confirm your email first";
               _resultDTO.Title = "Login Failed";
                return _resultDTO;
            }

            try
            {
                if (await SendForgotEmailOrPassword(user))
                {
                    _resultDTO.Success = true;
                    _resultDTO.Title = "Forgot username or password email sent";
                    _resultDTO.Message = "Please check you email";
                    return _resultDTO;
                }

                _resultDTO.Success = false;
                _resultDTO.Title = "Send Failed";
                _resultDTO.Message = "Failed to send email. Please contact the admin";
                return _resultDTO;

            }
            catch (Exception)
            {

                _resultDTO.Success = false;
                _resultDTO.Title = "Send Failed";
                _resultDTO.Message = "Failed to send email. Please contact the admin";
                return _resultDTO;
            }


        }

        public async Task<ResultDTO<UserDTO>> ResetPassword(ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user == null)
            {

                _resultDTO.Success = false;
                _resultDTO.Title = "Not Registered Email";
                _resultDTO.Message = "This email address has not been registered yet";
                return _resultDTO;
            }
            if (user.EmailConfirmed == false)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Please confirm your email first";
                _resultDTO.Title = "Login Failed";
                return _resultDTO;
            }

            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
                if (result.Succeeded)
                {
                    _resultDTO.Success = true;
                    _resultDTO.Title = "Password reset success";
                   _resultDTO.Message = "Your password has been reset";
                    return _resultDTO;
                }

                _resultDTO.Success = false;
                _resultDTO.Title = "Error Happen";
                _resultDTO.Message = "Invalid token. Please try again.";
                return _resultDTO;
            }
            catch (Exception)
            {

                _resultDTO.Success = false;
                _resultDTO.Title = "Error Happen";
               _resultDTO.Message = "Invalid token. Please try again.";
                return _resultDTO;
            }


        }

        public async Task<ResultDTO<UserDTO>> RefreshUserToken(ClaimsPrincipal userClaim)
        {
            var userEmailClaim = userClaim.FindFirst(ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(userEmailClaim))
            {
                var user = await _userManager.FindByEmailAsync(userEmailClaim);
                if (user != null)
                {
                    var CreatedUser = CreateApplicationUserDto(user);

                    _resultDTO.Success = true;
                    _resultDTO.Data = CreatedUser;
                    return _resultDTO;
                }
            }

            _resultDTO.Message = "Please Login Again";
            return _resultDTO;
        }







        #region Private Helper Methods
        private UserDTO CreateApplicationUserDto(User user)
        {

            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = _jwtService.CreateJWT(user),
            };
        }
        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }
        private async Task<bool> CheckNIDExistsAsync(string nId)
        {
            return await _userManager.Users.AnyAsync(x => x.NId == nId.ToLower());
        }
        private async Task<bool> CheckUsernameExistsAsync(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
        private async Task<bool> SendConfirmEmailAsync(User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var url = $"{_config["JWT:ClientUrl"]}/{_config["Email:ConfirmEmailPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
                "<p>Please confirm your email address by clicking on the following link.</p>" +
                $"<p><a href=\"{url}\">Click here</a></p>" +
                "<p>Thank you,</p>" +
                $"<br>{_config["Email:ApplicationName"]}";

            var emailSend = new EmailSendDTO(user.Email, "Confirm your email", body);

            return await _emailService.SendEmailAsync(emailSend);
        }

        private async Task<bool> SendForgotEmailOrPassword(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var url = $"{_config["JWT:ClientUrl"]}/{_config["Email:ResetPasswordPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
               $"<p>Email Address: {user.Email}</p>" +
               "<p>In order to reset your password, please click on the following link.</p>" +
               $"<p><a href=\"{url}\">Click here</a></p>" +
               "<p>Thank you,</p>" +
               $"<br>{_config["Email:ApplicationName"]}";

            var emailSend = new EmailSendDTO(user.Email, "Forgot email or password", body);

            return await _emailService.SendEmailAsync(emailSend);


        }

        #endregion
    }
}
