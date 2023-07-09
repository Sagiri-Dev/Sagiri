using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sagiri.Services.Misskey.Interfaces
{
    public interface IMisskeyService
    {
        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns></returns>
        ValueTask<bool> InitializeAsync();

        /// <summary>
        /// このユーザーからAPIにリクエストします。
        /// </summary>
        /// <param name="endpoint">エンドポイント名</param>
        /// <param name="ps">パラメーター</param>
        /// <returns>レスポンス</returns>
        ValueTask<(dynamic, bool)> RequestAsync(string endpoint, Dictionary<string, object?> ps);

        /// <summary>
        /// このユーザーからAPIにリクエストします。
        /// </summary>
        /// <param name="endpoint">エンドポイント名</param>
        /// <param name="ps">パラメーター</param>
        /// <returns>レスポンス</returns>
        ValueTask<(dynamic, bool)> RequestWithBinaryAsync(string endpoint, MultipartFormDataContent ps);

        /// <summary>
        /// 後始末
        /// </summary>
        void Dispose();
    }
}
