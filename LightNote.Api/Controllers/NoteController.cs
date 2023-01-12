using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LightNote.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    [Route(ApiRoutes.BaseRoute)]
    public class NoteController : ControllerBase
    {
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetByIdAsync(int id)
        {
            return Ok("hello");
        }
        [HttpGet]
        public IActionResult GetAllAsync()
        {
            return Ok("hello!!!! You did it!");
        }
        [HttpPost]
        public IActionResult PostAsync()
        {
            return Ok("hello");
        }
    }
}

