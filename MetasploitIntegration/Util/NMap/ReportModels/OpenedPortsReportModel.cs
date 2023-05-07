namespace MetasploitIntegration.Util.NMap.ReportModels
{
	public class OpenedPortsReportModel
	{
		public string IpAddress { get; set; }
		public string MacAddress { get; set; }
		public bool IsUp { get; set; }
		public DateTime RequestDate { get; set; }
		public IEnumerable<OpenedPortUnit> OpenedPorts { get; set; }

	}

	public class OpenedPortUnit
	{
		public int PortNumber { get; set; }
		public string ServiceName { get; set; }

	}
}
