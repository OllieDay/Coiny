namespace Coiny
{
	internal sealed class Tick
	{
		public string Symbol { get; set; }
		public decimal Price { get; set; }
		public decimal Change1H { get; set; }
		public decimal Change24H { get; set; }
		public decimal Change7D { get; set; }
	}
}
