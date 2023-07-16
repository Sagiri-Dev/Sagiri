using System.Text;

using Sagiri.Services.Spotify.Track;

namespace SagiriApp.Interop
{
    internal static class Helper
    {
        internal static string GenerateTrackText(string text, CurrentTrackInfo trackInfo)
        {
            StringBuilder sb = new(text);

            sb = sb.Replace("{Title}", "{0}");
            sb = sb.Replace("{Artist}", "{1}");
            sb = sb.Replace("{Album}", "{2}");
            sb = sb.Replace("{TrackNum}", "{3:D2}");

            return string.Format(
                sb.ToString(),
                trackInfo.TrackTitle,
                trackInfo.Artist,
                trackInfo.Album,
                trackInfo.TrackNumber
            );
        }
    }
}
