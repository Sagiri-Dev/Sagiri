using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Sagiri.Util.Configuration
{
    public class TwitterCredentialConfig 
        : AbstractCredentialConfig<TwitterCredentialConfig>
    {
        #region Properties

        [JsonProperty("ConsumerKey")]
        public string ConsumerKey { get; set; }
        [JsonProperty("ConsumerSecret")]
        public string ConsumerSecret { get; set; }
        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("AccessTokenSecret")]
        public string AccessTokenSecret { get; set; }

        #endregion Properties

        #region Public Override Methods

        public override async Task<TwitterCredentialConfig> LoadCredentialAsync(string tokenName = "twitter") =>
            await base.LoadCredentialAsync(tokenName);

        public override async Task SaveCredentialAsync(string tokenName = "twitter") =>
            await base.SaveCredentialAsync(tokenName);

        public override bool IsExistCredentialFile(string tokenName = "twitter") =>
            base.IsExistCredentialFile(tokenName);

        public override bool IsNullOrEmpty()
        {
            if (string.IsNullOrEmpty(this.ConsumerKey)) return true;
            if (string.IsNullOrEmpty(this.ConsumerSecret)) return true;
            if (string.IsNullOrEmpty(this.AccessToken)) return true;
            if (string.IsNullOrEmpty(this.AccessTokenSecret)) return true;
            return false;
        }

        #endregion Public Override Methods
    }
}
