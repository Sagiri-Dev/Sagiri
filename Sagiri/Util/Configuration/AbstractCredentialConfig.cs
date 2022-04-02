using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Sagiri.Util.Common;

namespace Sagiri.Util.Configuration
{
    public abstract class AbstractCredentialConfig<T> 
        where T : AbstractCredentialConfig<T>, new()
    {
        #region Field Variable

        /// <summary> 
        /// Logger
        /// </summary>
        private static readonly Logger _logger = Logger.GetInstance;

        /// <summary>
        /// Child classes singleton-pattern instance.
        /// </summary>
        private static readonly Lazy<T> _lazyInstance =
            new(() => Activator.CreateInstance(typeof(T), true) as T);

        #endregion Field Variable

        #region Property

        /// <summary>
        /// Child classes singleton-pattern instance.
        /// </summary>
        public static T Instance => _lazyInstance.Value;

        #endregion Property

        #region Public Virtual Methods

        /// <summary>
        /// Asynchronously read token information.
        /// </summary>
        /// <param name="tokenName"> token file name prefix. </param>
        /// <returns> Child class tokens. </returns>
        public virtual async Task<T> LoadCredentialAsync(string tokenName)
        {
            try
            {
                using var reader = new StreamReader(Constants.GetCredentialFileName(tokenName), Encoding.UTF8);
                var json = await reader.ReadToEndAsync();

                var tokens = JsonConvert.DeserializeObject<T>(json);
                return tokens;
            }
            catch (Exception)
            {
                var data = new T();
                await data.SaveCredentialAsync(tokenName);
                return data;
            }
        }

        /// <summary>
        /// Asynchronously store credential information.
        /// </summary>
        /// <param name="tokenName"></param>
        public virtual async Task SaveCredentialAsync(string tokenName)
        {
            var json = JsonConvert.SerializeObject(
                this, 
                new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii }
            );

            using var writer = new StreamWriter(
                Constants.GetCredentialFileName(tokenName), 
                false, 
                Encoding.UTF8
            );

            _logger.WriteLog($"SaveTokenAsync() -> {json}", Logger.LogLevel.Debug);
            await writer.WriteAsync(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenName"></param>
        /// <returns></returns>
        public virtual bool IsExistCredentialFile(string tokenName) => 
            File.Exists(Constants.GetCredentialFileName(tokenName));

        #endregion Public Virtual Methods

        #region Public Abstract Methods

        /// <summary>
        /// Child classes menber check null or empty.
        /// </summary>
        /// <returns> null or emply -> true / false </returns>
        public abstract bool IsNullOrEmpty();

        #endregion Public Abstract Methods
    }
}
