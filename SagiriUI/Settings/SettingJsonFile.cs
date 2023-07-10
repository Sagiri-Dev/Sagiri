using Sagiri.Settings;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SagiriUI.Settings
{
	/// <summary>
	/// 設定のJSONファイルを管理します
	/// </summary>
	public class SettingJsonFile : JsonFile
	{
		#region Properties/Fields

		[JsonProperty("format")]
		public string PostingFormat { get; set; }

		public static readonly string PostingFormatDefault = "{TrackNum}. {Title}\r\nArtist: {Artist}\r\nAlbum: {Album}\r\n#nowplaying";

		#endregion Properties/Fields

		#region Methods

		/// <summary>
		/// デフォルト値を設定することで値の整合性を取ります。
		/// </summary>
		/// <param name="target"></param>
		private void _Normalize() => this.PostingFormat ??= PostingFormatDefault;

		/// <summary>
		/// settings.json から設定を読み込みます
		/// <para>settings.json が存在しないときは新規に生成します</para>
		/// </summary>
		public static async Task<SettingJsonFile> LoadAsync()
		{
			var setting = await LoadAsync<SettingJsonFile>("settings.json");
			setting._Normalize();

			return setting;
		}

		/// <summary>
		/// settings.json に設定を保存します
		/// </summary>
		public Task SaveAsync()
		{
			this._Normalize();
			return this.SaveAsync("settings.json");
		}

		#endregion Methods
	}
}
