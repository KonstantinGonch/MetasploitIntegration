using MetasploitIntegration.Util.NMap.ReportModels;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace MetasploitIntegration.Services.Nmap
{
	public class NMapService : INMapService
	{
		private const int NmapLinesLimitIfFailed = 6;
		private const int NmapLineNumberOpenedPorts = 5;
		public async Task<OpenedPortsReportModel> ScanOpenPorts(IPAddress ipAddress)
		{
			var processInfo = new ProcessStartInfo()
			{
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				FileName = "C:\\Program Files (x86)\\Nmap\\nmap.exe",
				Arguments = $"{ipAddress.ToString()}"
			};

			StringBuilder sb = new StringBuilder();
			Process p = Process.Start(processInfo);
			p.OutputDataReceived += (sender, args) => sb.AppendLine(args.Data);
			p.BeginOutputReadLine();
			p.WaitForExit();
			return ParseResponse(ipAddress, sb.ToString());
		}

		private OpenedPortsReportModel ParseResponse(IPAddress ip, string response)
		{
			var lines = response.Split("\n");
			if (lines.Length < NmapLinesLimitIfFailed)
			{
				return new OpenedPortsReportModel
				{
					IsUp = false,
					IpAddress = ip.ToString(),
					RequestDate = DateTime.Now
				};
			}

			var report = new OpenedPortsReportModel
			{
				IsUp = true,
				IpAddress = ip.ToString(),
				RequestDate = DateTime.Now
			};
			var macLineNo = Array.IndexOf(lines, lines.FirstOrDefault(l => l.StartsWith("MAC")));
			var openedPorts = new List<OpenedPortUnit>();
			for (var i = NmapLineNumberOpenedPorts; i < macLineNo; i++)
			{
				var info = lines[i].Split().Where(el => el != String.Empty).ToArray();
				var portInfo = info[0];
				var serviceName = info[2];
				var port = portInfo.Split("/")[0];
				openedPorts.Add(new OpenedPortUnit
				{
					PortNumber = int.TryParse(port, out int v) ? v : 0,
					ServiceName = serviceName,
				});
			}
			report.OpenedPorts = openedPorts;

			var mac = lines[macLineNo].Split(":", 2)[1].Trim();
			report.MacAddress = mac;

			return report;
		}
	}
}
