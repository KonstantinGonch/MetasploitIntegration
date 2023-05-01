namespace MetasploitIntegration.Models
{
	public class Resource
	{
		public long Id { get; set; }
		public long EnvironmentId { get; set; }
		public Environment? Environment { get; set; }
		public string Name { get; set; }
		public string ResourceReference { get; set; }
	}
}
