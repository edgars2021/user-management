using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Core;
using UserManagementApp.Core.Models;
using UserManagementApp.Core.Services;

namespace UserManagementApp.HttpApi
{
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/users")]
    public class UsersController :  ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentException(nameof(userService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBy(int id)
        {
            var user = await _userService.GetByAsync(id);

            if (user == null) return NotFound();    
            
            return Ok(user);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserModelDto user)
        {
            var updatedUser = await _userService.UpdateAsync(id, user);
            
            if (updatedUser == null) return NotFound();
            
            return Ok(updatedUser);
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> Create([FromBody] UserModelDto user)
        {
            var createdUser = await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(GetBy), new { id = createdUser.Id}, createdUser);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _userService.DeleteAsync(id);

            if (isDeleted) return NoContent();

            return NotFound();
        }
    }
}