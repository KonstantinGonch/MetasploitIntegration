using MetasploitIntegration.Util.NMap.ReportModels;
using System.Net;

namespace MetasploitIntegration.Services.Nmap
{
	public interface INMapService
	{
		public Task<OpenedPortsReportModel> ScanOpenPorts(IPAddress ipAddress);
	}
}
