using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sagiri.Exceptions;
using Sagiri.Services.Misskey.Interfaces;
using Sagiri.Util.Common;
using Sagiri.Util.Configuration;

using static Sagiri.Util.Common.Constants;

namespace Sagiri.Services.Misskey
{
    /// <summary>
    /// Misskey アクセス用クラス
    /// </summary>
    public class MisskeyService : IMisskeyService
    {
        #region Properties

        private MisskeyCredentialConfig _MisskeyCredentialConfig { get; set; }
        private Lazy<HttpClient> _Client { get; set; } = new();
        private string? _Host { get; set; }
        private string? _AccessToken { get; set; }
        private Logger _Logger { get; set; }

        #endregion Properties

        #region Constructor

        public MisskeyService() => _Logger = Logger.GetInstance;

        public MisskeyService(string host, string accessToken)
        {
            _Host = host;
            _AccessToken = accessToken;
        }

        #endregion Constructor

        #region Public Methods

        async ValueTask<bool> IMisskeyService.InitializeAsync()
        {
            try
            {
                // ここに来るまでにファイルがあることは確定するので、ファイル有無チェックはしない。
                _MisskeyCredentialConfig = await new MisskeyCredentialConfig().LoadCredentialAsync();

                if (_MisskeyCredentialConfig.IsNullOrEmpty())
                    throw new SagiriException();

                _AccessToken = _MisskeyCredentialConfig?.Token;
                _Host = _MisskeyCredentialConfig?.Host;

                _Logger.WriteLog("[Sagiri - MisskeyService] - Finished reading misskey credential info.", Logger.LogLevel.Info);
                return true;
            }
            catch (Exception)
            {
                _Logger.WriteLog("[Sagiri - MisskeyService] - Failed reading credential info...", Logger.LogLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// このユーザーからAPIにリクエストします。
        /// </summary>
        /// <param name="endpoint">エンドポイント名</param>
        /// <param name="ps">パラメーター</param>
        /// <returns>レスポンス</returns>
        async ValueTask<dynamic> IMisskeyService.RequestAsync(string endpoint, Dictionary<string, object?> ps)
        {
            ps.Add("i", _AccessToken);
            return await _RequestAsync(_Host, endpoint, ps);
        }

        /// <summary>
        /// このユーザーからAPIにリクエストします。
        /// </summary>
        /// <param name="endpoint">エンドポイント名</param>
        /// <param name="ps">パラメーター</param>
        /// <returns>レスポンス</returns>
        async ValueTask<dynamic> IMisskeyService.RequestWithBinaryAsync(string endpoint, MultipartFormDataContent ps)
        {
            ps.Add(new StringContent(_AccessToken), "i");
            return await _RequestWithBinaryAsync(_Host, endpoint, ps);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// APIにリクエストします。
        /// </summary>
        /// <param name="host">MisskeyインスタンスのURL</param>
        /// <param name="endpoint">エンドポイント名</param>
        /// <param name="ps">パラメーター</param>
        /// <returns>レスポンス</returns>
        private async ValueTask<dynamic> _RequestAsync(string? host, string endpoint, Dictionary<string, object?> ps)
        {
            var client = _Client.Value;

            var ep = $"{host}/api/{endpoint}";

            var content = new StringContent(
                JsonConvert.SerializeObject(ps),
                Encoding.UTF8, "application/json"
            );

            var res = await client.PostAsync(ep, content);

            var obj = JsonConvert.DeserializeObject<dynamic>(
                await res.Content.ReadAsStringAsync()
            );

            return obj;
        }

        /// <summary>
        /// APIにリクエストします。
        /// </summary>
        /// <param name="host">MisskeyインスタンスのURL</param>
        /// <param name="endpoint">エンドポイント名</param>
        /// <param name="ps">パラメーター</param>
        /// <returns>レスポンス</returns>
        private async ValueTask<dynamic> _RequestWithBinaryAsync(string? host, string endpoint, MultipartFormDataContent ps)
        {
            var client = _Client.Value;

            var ep = $"{host}/api/{endpoint}";

            var res = await client.PostAsync(ep, ps);

            var obj = JsonConvert.DeserializeObject<dynamic>(
                await res.Content.ReadAsStringAsync()
            );

            return obj;
        }

        #endregion Private Methods
    }
}
