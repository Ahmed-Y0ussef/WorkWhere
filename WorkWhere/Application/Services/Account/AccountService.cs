using Application.DTO.Account;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Account
{
    public class AccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ResultDTO<User[]> _resultDTO;

        public AccountService(UserManager<User> userManager, IMapper mapper, ResultDTO<User[]> resultDTO)
        {
            _userManager = userManager;
            _mapper = mapper;
            _resultDTO = resultDTO;
        }

        // Get all users
        public async Task<ResultDTO<User[]>> GetAllUsers()
        {
            var users = await _userManager.Users.ToArrayAsync(); 
            if (users.Length == 0) 
            {

                _resultDTO.Success = false;
                _resultDTO.Message = "No users found";
                _resultDTO.Title = "Get All Users Failed";
                _resultDTO.Data = null;
                return _resultDTO;
            }


            _resultDTO.Success = true;
            _resultDTO.Message = "Users retrieved successfully";
            _resultDTO.Title = "Get All Users Success";
            _resultDTO.Data = users;
            return _resultDTO;

        }

        // Get profile by ID
        public async Task<ResultDTO<User[]>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "No user found";
                _resultDTO.Title = "Get User Failed";
                _resultDTO.Data = null;
                return _resultDTO;
            }
            var userData = new User[] { user };
            _resultDTO.Success = true;
            _resultDTO.Message = "User retrieved successfully";
            _resultDTO.Title = "Get User Success";
            _resultDTO.Data = userData;
            return _resultDTO;
        }

        // Edit profile
        public async Task<ResultDTO<User[]>> UpdateUser(UpdateUserDTO userToEdit, ClaimsPrincipal userClaim)
        {
            var userEmailClaim = userClaim.FindFirst(ClaimTypes.Email)?.Value;
            User user = null;
            if (!string.IsNullOrEmpty(userEmailClaim))
            {
                user = await _userManager.FindByEmailAsync(userEmailClaim);
                if (user == null)
                {
                    _resultDTO.Success = false;
                    _resultDTO.Message = "No user found";
                    _resultDTO.Title = "Get User Failed";
                    _resultDTO.Data = null;
                    return _resultDTO;
                }
            }
            var currentpersonalimage = user.PersonalImg;
            var currentnimg = user.NImg;

            if (userToEdit.PersonalImg != null)
            {
                user.PersonalImg = await ConvertFileToByteArrayAsync(userToEdit.PersonalImg);

            }
            else if (userToEdit.NImg != null)
            {
                user.NImg = await ConvertFileToByteArrayAsync(userToEdit.NImg);
            }
            else
            {
                // if newpersonalimg is null, retain the existing image
                user.PersonalImg = currentpersonalimage;
                user.NImg = currentnimg;
            }
            user.UserName = userToEdit.UserName != null ? userToEdit.UserName : user.UserName;
            //var data = _mapper.Map<UpdateUserDTO , User>(userToEdit);
            _mapper.Map(userToEdit, user);


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var userData = new User[] { user };
                _resultDTO.Success = true;
                _resultDTO.Message = "User edited successfully";
                _resultDTO.Title = "Edit User Success";
                _resultDTO.Data = userData;
                return _resultDTO;
            }
            else
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "Failed to update user. Please try again later.";
                return _resultDTO;
            }
        }

        //public async Task<ResultDTO<User[]>> UpdateUser(UpdateUserDTO userToEdit, ClaimsPrincipal userClaim)
        //{
        //    var userEmailClaim = userClaim.FindFirst(ClaimTypes.Email)?.Value;
        //    User user = null;
        //    if (!string.IsNullOrEmpty(userEmailClaim))
        //    {
        //        user = await _userManager.FindByEmailAsync(userEmailClaim);
        //        if (user == null)
        //        {
        //            _resultDTO.Success = false;
        //            _resultDTO.Message = "No user found";
        //            _resultDTO.Title = "Get User Failed";
        //            _resultDTO.Data = null;
        //            return _resultDTO;
        //        }
        //    }

        //    // Map the data from UpdateUserDTO to User
        //    var updatedUser = _mapper.Map(userToEdit, user);

        //    // Apply the update
        //    var result = await _userManager.UpdateAsync(updatedUser);

        //    if (result.Succeeded)
        //    {
        //        var userData = new User[] { updatedUser };
        //        _resultDTO.Success = true;
        //        _resultDTO.Message = "User edited successfully";
        //        _resultDTO.Title = "Edit User Success";
        //        _resultDTO.Data = userData;
        //        return _resultDTO;
        //    }
        //    else
        //    {
        //        _resultDTO.Success = false;
        //        _resultDTO.Message = "Failed to update user. Please try again later.";
        //        return _resultDTO;
        //    }
        //}



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

        // delete user
        public async Task<ResultDTO<User[]>> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync($"{id}");
            if (user == null)
            {
                _resultDTO.Success = false;
                _resultDTO.Message = "No user found";
                _resultDTO.Title = "Get User Failed";
                _resultDTO.Data = null;
                return _resultDTO;
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _resultDTO.Success = true;
                _resultDTO.Message = "Account Deleted successfully";
                return _resultDTO;
            }
            else
            {
                _resultDTO.Success = false;
                _resultDTO.Title = "Failed to delete";
                _resultDTO.Message = "Account Delete Failed. Try again later";
                return _resultDTO;
            }

        }


    }
}
