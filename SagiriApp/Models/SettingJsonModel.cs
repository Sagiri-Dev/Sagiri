using System.IO;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SagiriApp.Models
{
    internal class SettingJsonModel
    {
        #region Properties/Fields

        [JsonProperty("format")]
        public string PostingFormat { get; set; }

        public readonly string PostingFormatDefault = "\ud83c\udfb5 {Title}\r\n\ud83c\udf99 {Artist}\r\n\ud83d\udcbf {Album}\r\n#nowplaying #Spotify #Sagiri";

        #endregion Properties/Fields

        #region Methods

        /// <summary>
        /// settings.json から設定を読み込みます
        /// <para>settings.json が存在しないときは新規に生成します</para>
        /// </summary>
        public async Task<SettingJsonModel> LoadAsync()
        {
            var fileName = "settings.json";
            try
            {
                using var reader = new StreamReader(fileName, Encoding.UTF8);
                string jsonString = await reader.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<SettingJsonModel>(jsonString);

                data?._Normalize();
                return data ??= (SettingJsonModel)MemberwiseClone();
            }
            catch
            {
                await _SaveAsync(fileName);
                return (SettingJsonModel)MemberwiseClone();
            }
        }

        /// <summary>
        /// settings.json に設定を保存します
        /// </summary>
        public Task SaveAsync()
        {
            _Normalize();
            return _SaveAsync("settings.json");
        }

        /// <summary>
        /// デフォルト値を設定することで値の整合性を取ります。
        /// </summary>
        /// <param name="target"></param>
        private void _Normalize() => PostingFormat ??= PostingFormatDefault;

        private async Task _SaveAsync(string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(
                this,
                new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii }
            );

            using var writer = new StreamWriter(fileName, false, Encoding.UTF8);
            await writer.WriteAsync(jsonString);
        }

        #endregion Methods
    }
}
