using MetasploitIntegration.Models;
using MetasploitIntegration.Util;
using Microsoft.AspNetCore.Mvc;

namespace MetasploitIntegration.Controllers
{
	[ApiController]
	[Route("api/resources")]
	public class ResourcesController : ControllerBase
	{
		private AppDataContext _context;
		public ResourcesController(AppDataContext ctx)
		{
			_context = ctx;
		}

		[HttpPost]
		[Route("add")]
		public async Task<IActionResult> AddResource(Resource resource)
		{
			await _context.Resources.AddAsync(resource);
			await _context.SaveChangesAsync();
			return Ok(resource);
		}
	}
}
