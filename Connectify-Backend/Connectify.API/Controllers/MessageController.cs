using Connectify.Application.Interfaces.AWSServicesInterfaces;
using Connectify.Infrastructure.Services.AWSService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace Connectify.API.Controllers
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/messages")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        public MessageController(IPhotoService photoService)
        {
            _photoService = photoService;
        }


        [HttpPost]
        [Route("upload-attachment")]
        public async Task<IActionResult> UploadMessageAttachment([FromForm] IFormFile photo)
        {
            try
            {
                var result = await _photoService.UploadMessageAttachment(photo);
                Console.WriteLine(result);
                return Ok(new {status = true, attachment=result});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new { status = false, message = ex.ToString()});
            }
        }
    }
}
