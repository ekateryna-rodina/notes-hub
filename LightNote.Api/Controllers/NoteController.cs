using System;
using Microsoft.AspNetCore.Mvc;

namespace LightNote.Api.Controllers
{
	[ApiVersion("1.0")]
	[ApiController]
	[Route("api/v{version:apiVersion}/{controller}")]
	public class NoteController : ControllerBase
	{
		[Route("{id}")]
		[HttpGet]
		public IActionResult GetById(int id) {
			return Ok("hello");
		}
	}
}

