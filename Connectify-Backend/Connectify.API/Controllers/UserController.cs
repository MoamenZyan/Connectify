using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.AWSServicesInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Connectify.API.Controllers
{
    [Route("/api/v{version:apiVersion}/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserApplicationService _userApplicationService;
        public UserController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }


        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            Guid currentUserId = new Guid(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var user = await _userApplicationService.GetCurrentUser(currentUserId);
            if (user == null)
                return NotFound(new {status = false, message = "user not found"});
            return Ok(new {status = true, user = user});
        }

        [HttpPatch]
        [Route("")]
        public async Task<IActionResult> UpdateUser()
        {
            Guid currentUserId = new Guid(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var body = await Request.ReadFormAsync();
            var result = await _userApplicationService.UpdateUser(currentUserId, body);
            if (!result.Item1)
                return NotFound(new { status = false, message = result.Item2 });
            return Ok(new { status = true, message = "user updated successully" });
        }



        [HttpGet]
        [Route("search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            Guid currentUserId = new Guid(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = _userApplicationService.SearchByUserName(name, currentUserId);
            return Ok(new { users = result });
        }

        [HttpGet]
        [Route("all")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllUsersV1()
        {
            Guid currentUserId = new Guid(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var users = await _userApplicationService.GetAllUsers(currentUserId);
            return Ok(users);
        }

        [HttpPost]
        [Route("login")]
        [Authorize(AuthenticationSchemes = "Basic")]
        public async Task<IActionResult> LoginUser()
        {
            Guid currentUserId = new Guid(HttpContext.User.Claims.First().Value);
            var token = await _userApplicationService.LoginUser(currentUserId);

            if (token == null)
                return Unauthorized(new {status = false, message="unvalid credentials"});

            return Ok(new {status = true, token = token});
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterNewUser()
        {
            var form = await Request.ReadFormAsync();
            var result = await _userApplicationService.RegisterUser(form);
            if (!result.Item1)
                return BadRequest(new { status = false, message = result.Item2 });

            return Ok(new {status = true, message = "user created!"});
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteUser()
        {
            Guid currentUserId = new Guid(HttpContext.User.Claims.First().Value);
            var result = await _userApplicationService.DeleteUser(currentUserId);
            if (!result)
                return BadRequest(new { status = false, message = "error in deleting user" });

            return Ok(new { status = true, message = "user deleted!" });
        }

        [HttpPost]
        [Route("friend-request/{receiverId}")]
        public async Task<IActionResult> SendFriendRequest(Guid receiverId)
        {
            var currentUserId = new Guid(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = await _userApplicationService.SendFriendRequest(currentUserId, receiverId);
            if (!result)
                return new JsonResult(new {status = false, message = "error in sending friend request"}) { StatusCode = 500 };

            return Ok(new { status = true, message = "friend request sent!" });
        }

        [HttpDelete]
        [Route("friend-request/{receiverId}")]
        public async Task<IActionResult> DeleteFriendRequest(Guid receiverId)
        {
            var currentUserId = new Guid(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = await _userApplicationService.RemoveFriendRequest(currentUserId, receiverId);
            if (!result)
                return new JsonResult(new { status = false, message = "error in deleting friend request" }) { StatusCode = 500 };

            return Ok(new { status = true, message = "friend request deleted!" });
        }

        [HttpPost]
        [Route("accept-friend-request/{senderId}")]
        public async Task<IActionResult> AcceptFriendRequest(Guid senderId)
        {
            var currentUserId = new Guid(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = await _userApplicationService.AcceptFriendRequest(currentUserId, senderId);
            if (!result)
                return new JsonResult(new { status = false, message = "error in sending friend request" }) { StatusCode = 500 };

            return Ok(new { status = true, message = "friend request sent!" });
        }

        [HttpPost]
        [Route("upload-photo")]
        public async Task<IActionResult> UploadProfilePhoto([FromForm] IFormFile photo)
        {
            var userId = new Guid(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var result = await _userApplicationService.UpdateProfilePhoto(photo, userId);
            if (result)
                return Ok(new { status = true, message = "photo uploaded successfully!" });

            return BadRequest(new { status = true, message = "photo uploading failed" });

        }
    }
}
