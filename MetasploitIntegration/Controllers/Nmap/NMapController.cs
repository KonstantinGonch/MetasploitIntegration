using MetasploitIntegration.Services.Nmap;
using MetasploitIntegration.Util;
using MetasploitIntegration.Util.NMap.ReportModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MetasploitIntegration.Controllers.Nmap
{
	[ApiController]
	[Route("api/nmap")]
	public class NMapController : ControllerBase
	{
		private INMapService _nmapService;
		private AppDataContext _context;

		public NMapController(INMapService _nmap, AppDataContext ctx)
		{
			_nmapService = _nmap;
			_context = ctx;
		}

		[HttpGet]
		[Route("openPorts")]
		public async Task<IActionResult> GetOpenPorts()
		{
			var response = new List<OpenedPortsReportModel>();
			var environment = _context.Environments.FirstOrDefault(e => e.IsActive);
			if (environment == null)
				return BadRequest("No active environment");

			var resources = _context.Resources.Where(e => e.EnvironmentId == environment.EnvironmentId);
			foreach(var resource in resources)
			{
				var result = await _nmapService.ScanOpenPorts(IPAddress.Parse(resource.ResourceReference));
				response.Add(result);
			}
			return Ok(response);
		}
	}
}
