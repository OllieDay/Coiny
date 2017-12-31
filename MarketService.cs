using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Coiny
{
	internal sealed class MarketService : IDisposable
	{
		private readonly HttpClient _client = new HttpClient();

		public async Task<IEnumerable<Tick>> GetTicksAsync()
		{
			using (var response = await _client.GetAsync("https://api.coinmarketcap.com/v1/ticker?limit=0"))
			{
				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();

					return JsonConvert.DeserializeObject<IEnumerable<Tick>>(json, new TickConverter());
				}

				return Enumerable.Empty<Tick>();
			}
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
