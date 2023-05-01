using System;
using System.Diagnostics;
using System.Net;

namespace MetasploitIntegration.Services.Nmap
{
	public class NMapService : INMapService
	{
		public async Task<string> ScanOpenPorts(IPAddress ipAddress)
		{
			var command = $"nmap.exe {ipAddress.ToString()}";
			var cmdsi = new ProcessStartInfo("cmd.exe");
			cmdsi.Arguments = command;
			cmdsi.RedirectStandardOutput = true;
			cmdsi.UseShellExecute = false;
			var cmd = Process.Start(cmdsi);
			var output = new List<string>();

			while (cmd.StandardOutput.Peek() > -1)
			{
				output.Add(cmd.StandardOutput.ReadLine());
			}

			while (cmd.StandardError.Peek() > -1)
			{
				output.Add(cmd.StandardError.ReadLine());
			}
			cmd.WaitForExit();

			return String.Empty;
		}
	}
}
