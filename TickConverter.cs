using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coiny
{
	internal sealed class TickConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType)
		{
			return typeof(Tick).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var value = JObject.Load(reader);

			return new Tick
			{
				Symbol = (string)value["symbol"],
				Price = ReadDecimal("price_usd"),
				Change1H = ReadPercentage("percent_change_1h"),
				Change24H = ReadPercentage("percent_change_24h"),
				Change7D = ReadPercentage("percent_change_7d"),
			};

			decimal ReadPercentage(string key)
			{
				return ReadDecimal(key) / 100;
			}

			decimal ReadDecimal(string key)
			{
				if (decimal.TryParse((string)value[key], out var result))
				{
					return result;
				}

				// Value is null for currencies with no data
				return default(decimal);
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
