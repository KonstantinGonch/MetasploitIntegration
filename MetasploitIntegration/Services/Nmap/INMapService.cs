using System.Net;

namespace MetasploitIntegration.Services.Nmap
{
	public interface INMapService
	{
		public Task<string> ScanOpenPorts(IPAddress ipAddress);
	}
}
