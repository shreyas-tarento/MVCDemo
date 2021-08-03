using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCDemo.Interfaces;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfile _iProfile;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfileController(IProfile iProfile)
        {
            _iProfile = iProfile;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            return Ok(await _iProfile.GetAllProfiles());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProfile([FromRoute]int id)
        {
            var exists = await _iProfile.GetProfile(id);
            if (exists == null)
                return NotFound($"Profile for id #{id} not found");

            return Ok(exists);
        }

       public class FileUploadApi
        {
            public IFormFile files { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromForm] Profile profile)
        {
            CommonResult result = await _iProfile.CreateProfile(profile);
            if (result.Error.Count > 0)
                return new JsonResult(result)
                {
                    StatusCode = 400,
                };

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute]int id,[FromForm] Profile profile)
        {
            if (id != profile.Id)
                return BadRequest("Id Mismatch");

            var existing = await _iProfile.GetProfile(id);
            if(existing == null)
                return NotFound($"Profile for id #{id} not found");

            CommonResult result = await _iProfile.UpdateProfile(existing, profile);
            if (result.Error.Count > 0)
                return new JsonResult(result)
                {
                    StatusCode = 400,
                };

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var existing = await _iProfile.GetProfile(id);
            if (existing == null)
                return NotFound($"Profile for id #{id} not found");

            await _iProfile.DeleteProfile(existing);
            return Ok(existing);

        }
    }
}
