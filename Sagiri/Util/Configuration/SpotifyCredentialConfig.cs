using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Sagiri.Util.Configuration
{
    public class SpotifyCredentialConfig
        : AbstractCredentialConfig<SpotifyCredentialConfig>
    {
        #region Properties

        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("is_expired")]
        public bool IsExpired { get; set; }

        #endregion Properties

        #region Public Override Methods

        public override async Task<SpotifyCredentialConfig> LoadCredentialAsync(string tokenName = "spotify") => 
            await base.LoadCredentialAsync(tokenName);

        public override async Task SaveCredentialAsync(string tokenName = "spotify") =>
            await base.SaveCredentialAsync(tokenName);

        public override bool IsExistCredentialFile(string tokenName = "spotify") => 
            base.IsExistCredentialFile(tokenName);

        public override bool IsNullOrEmpty()
        {
            if (string.IsNullOrEmpty(this.ClientId)) return true;
            if (string.IsNullOrEmpty(this.ClientSecret)) return true;
            if (string.IsNullOrEmpty(this.AccessToken)) return true;
            return false;
        }

        #endregion Public Override Methods
    }
}
