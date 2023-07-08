using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Sagiri.Util.Configuration
{
    public class MisskeyCredentialConfig 
        : AbstractCredentialConfig<MisskeyCredentialConfig>
    {
        #region Properties

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("Host")]
        public string Host { get; set; }

        #endregion Properties

        #region Public Override Methods

        public override async Task<MisskeyCredentialConfig> LoadCredentialAsync(string tokenName = "misskey") =>
            await base.LoadCredentialAsync(tokenName);

        public override async Task SaveCredentialAsync(string tokenName = "misskey") =>
            await base.SaveCredentialAsync(tokenName);

        public override bool IsExistCredentialFile(string tokenName = "misskey") =>
            base.IsExistCredentialFile(tokenName);

        public override bool IsNullOrEmpty()
        {
            if (string.IsNullOrEmpty(this.Token)) return true;
            if (string.IsNullOrEmpty(this.Host)) return true;
            return false;
        }

        #endregion Public Override Methods
    }
}
