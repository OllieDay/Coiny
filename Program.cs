using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coiny
{
	internal static class Program
	{
		private static readonly MarketService MarketService = new MarketService();
		private static readonly PrintService PrintService = new PrintService();

		private static int Main(string[] args)
		{
			return RunAsync(args).GetAwaiter().GetResult();
		}

		private static async Task<int> RunAsync(string[] args)
		{
			var currencies = args.Where(arg => !arg.StartsWith("-")).Select(arg => arg.ToUpper());

			if (!args.Any() || args.First() == "-h" || args.First() == "--help" || !currencies.Any())
			{
				return Usage();
			}

			if (args.Contains("-w") || args.Contains("--watch"))
			{
				return await Watch(currencies);
			}

			return await Fetch(currencies);
		}

		private static int Usage()
		{
			Console.WriteLine("Coiny - Cryptocurrency market data from your terminal");
			Console.WriteLine();
			Console.WriteLine("Usage:");
			Console.WriteLine("  coiny -h|--help");
			Console.WriteLine("  coiny [-w|--watch] <currency> <currency>...");
			Console.WriteLine();
			Console.WriteLine("Options:");
			Console.WriteLine("  -h, --help     Show this screen.");
			Console.WriteLine("  -w, --watch    Refresh market data at regular intervals.");
			Console.WriteLine();
			Console.WriteLine("Currencies:");
			Console.WriteLine("  BTC            Bitcoin");
			Console.WriteLine("  ETH            Ethereum");
			Console.WriteLine("  LTC            Litecoin");
			Console.WriteLine("  ...            ...");
			Console.WriteLine();

			return 1;
		}

		private static async Task<int> Watch(IEnumerable<string> currencies)
		{
			while (true)
			{
				Console.Clear();

				await Fetch(currencies);
				await Task.Delay(TimeSpan.FromMinutes(5));
			}
		}

		private static async Task<int> Fetch(IEnumerable<string> currencies)
		{
			var ticks = await MarketService.GetTicksAsync();
			var filteredTicks = ticks.Where(tick => currencies.Contains(tick.Symbol));

			PrintService.PrintTicks(filteredTicks);

			return 0;
		}}
}
