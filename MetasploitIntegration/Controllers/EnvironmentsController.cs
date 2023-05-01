using MetasploitIntegration.Models;
using MetasploitIntegration.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Environment = MetasploitIntegration.Models.Environment;

namespace MetasploitIntegration.Controllers
{
	[ApiController]
	[Route("api/environments")]
	public class EnvironmentsController : ControllerBase
	{
		private AppDataContext _context;
		public EnvironmentsController (AppDataContext ctx)
		{
			_context = ctx;
		}

		[HttpGet]
		[Route("getItem")]
		public async Task<IActionResult> GetItem(long environmentId)
		{
			var item = await _context.Environments.FirstOrDefaultAsync(e => e.EnvironmentId == environmentId);
			if (item != null)
				return Ok(item);
			return NotFound();
		}

		[HttpGet]
		[Route("list")]
		public async Task<IActionResult> GetList()
		{
			return Ok(_context.Environments);
		}

		[HttpPost]
		[Route("add")]
		public async Task<IActionResult> AddItem(Environment environment)
		{
			var item = await _context.Environments.AddAsync(environment);
			await _context.SaveChangesAsync();
			return Ok(environment);
		}

		[HttpPost]
		[Route("activate")]
		public async Task<IActionResult> AddItem(long environmentId)
		{
			var item = await _context.Environments.FindAsync(environmentId);
			if (item != null)
			{
				item.IsActive = true;
				await _context.Environments.Where(e => e.EnvironmentId != environmentId).ForEachAsync(e => e.IsActive = false);
				await _context.SaveChangesAsync();
				return Ok(item);
			}
			else
			{
				return NotFound();
			}
		}

		[HttpDelete]
		[Route("delete")]
		public async Task<IActionResult> DeleteItem(long environmentId)
		{
			var item = await _context.Environments.FirstOrDefaultAsync(e => e.EnvironmentId == environmentId);
			if (item != null)
			{
				_context.Environments.Remove(item);
				await _context.SaveChangesAsync();
				return Ok(item);
			}
			return NotFound();
		}
	}
}
