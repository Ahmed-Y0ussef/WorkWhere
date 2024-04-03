using Application.DTO.Account;
using Application.Services.Account;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WorkWhere.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly AccountService _accountService;

        public AccountController(UserManager<User> userManager, IMapper mapper, AccountService accountService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _accountService = accountService;
        }

        // Get all users
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _accountService.GetAllUsers();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // Get profile by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await _accountService.GetUser(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // Edit profile

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO userToEdit)
        {
            
            var result = await _accountService.UpdateUser(userToEdit, User);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        // delete user
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _accountService.DeleteUser(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }


    }
}
