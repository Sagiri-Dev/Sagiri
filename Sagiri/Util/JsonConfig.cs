using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using static Sagiri.Util.Common.Constants;

namespace Sagiri.Util
{
    internal class JsonConfig
    {
        [JsonProperty("client_id")]
        internal string ClientId { get; set; }
        [JsonProperty("client_secret")]
        internal string ClientSecret { get; set; }
        [JsonProperty("access_token")]
        internal string AccessToken { get; set; }
        [JsonProperty("created_at")]
        internal DateTime CreatedAt { get; set; }
        [JsonProperty("is_expired")]
        internal bool IsExpired { get; set; }

        internal async Task<JsonConfig> LoadTokenAsync()
        {
            try
            {
                using var reader = new StreamReader(TokenName, Encoding.UTF8);
                var json = await reader.ReadToEndAsync();

                var tokens = JsonConvert.DeserializeObject<JsonConfig>(json);
                return tokens;
            }
            catch (Exception)
            {
                var tokens = new JsonConfig();
                await tokens.SaveTokenAsync();

                return tokens;
            }
        }

        internal async Task SaveTokenAsync()
        {
            var json = JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
            });

            using var writer = new StreamWriter(TokenName, false, Encoding.UTF8);
            await writer.WriteAsync(json);
        }

        internal bool IsNullOrEmpty()
        {
            if (string.IsNullOrEmpty(this.ClientId)) return true;
            if (string.IsNullOrEmpty(this.ClientSecret)) return true;
            if (string.IsNullOrEmpty(this.AccessToken)) return true;
            return false;
        }
    }
}
