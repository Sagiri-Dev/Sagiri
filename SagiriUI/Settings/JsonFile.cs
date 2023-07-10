using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sagiri.Settings
{
	public abstract class JsonFile
	{
		protected static async Task<T> LoadAsync<T>(string fileName) where T : JsonFile, new()
		{
			try
			{
				string jsonString = null;
				using var reader = new StreamReader(fileName, Encoding.UTF8);
				jsonString = await reader.ReadToEndAsync();

				var data = JsonConvert.DeserializeObject<T>(jsonString);

				return data;
			}
			catch
			{
				var data = new T();
				await data.SaveAsync(fileName);

				return data;
			}
		}

		protected async Task SaveAsync(string fileName)
		{
			var jsonString = JsonConvert.SerializeObject(
				this, 
				new JsonSerializerSettings {
					StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
				}
			);

			using var writer = new StreamWriter(fileName, false, Encoding.UTF8);
			await writer.WriteAsync(jsonString);
		}
	}
}
