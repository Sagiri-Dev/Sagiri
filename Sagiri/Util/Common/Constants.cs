using System;

namespace Sagiri.Util.Common
{
    internal static class Constants
    {
        /// <remarks>
        /// Do not upload 2 authentication key.
        /// @See https://developer.spotify.com/dashboard/
        /// Login -> Create App -> Copy & Paste:-)
        /// </remarks>
        internal static readonly string ClientId = "";
        internal static readonly string ClientSecret = "";

        internal static readonly Uri RedirectUri = new("http://localhost:5050/callback");
        internal static readonly int PortNo = 5050;

        internal static readonly string CurrentPlayingRequestUrl =
            "https://api.spotify.com/v1/me/player/currently-playing?market=JP";

        internal static string GetSpotifyExePath(string userName) 
            => $"C:/Users/{userName}/AppData/Roaming/Spotify/Spotify.exe";

        internal static string GetCredentialFileName(string fileName) => $"{fileName}.json";
    }
}
