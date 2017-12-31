using System;
using System.Collections.Generic;

namespace Coiny
{
	internal sealed class PrintService
	{
		public void PrintTicks(IEnumerable<Tick> ticks)
		{
			PrintHeading();

			foreach (var tick in ticks)
			{
				PrintTick(tick);
			}
		}

		private static void PrintTick(Tick tick)
		{
			PrintSymbol(tick.Symbol);
			PrintPrice(tick.Price);
			PrintChange(tick.Change1H);
			PrintChange(tick.Change24H);
			PrintChange(tick.Change7D);
			Console.WriteLine();
		}

		private static void PrintHeading()
		{
			Console.Write("Price".PadLeft(21));
			Console.Write("1h".PadLeft(14));
			Console.Write("24h".PadLeft(14));
			Console.Write("7d".PadLeft(14));
			Console.WriteLine();
			Console.WriteLine();
		}

		private static void PrintSymbol(string symbol)
		{
			Console.Write(symbol.PadRight(10));
		}

		private static void PrintPrice(decimal price)
		{
			Console.Write(price.ToString("#,##0.00########").PadLeft(11));
		}

		private static void PrintChange(decimal change)
		{
			var oldForegroundColor = Console.ForegroundColor;
			var newForegroundColor = Console.ForegroundColor;

			if (change > 0)
			{
				newForegroundColor = ConsoleColor.Green;
			}
			else if (change < 0)
			{
				newForegroundColor = ConsoleColor.Red;
			}

			Console.ForegroundColor = newForegroundColor;
			Console.Write(change.ToString("+#,##0.00%;-#,##0.00%;0.00%").PadLeft(14));
			Console.ForegroundColor = oldForegroundColor;
		}
	}
}
