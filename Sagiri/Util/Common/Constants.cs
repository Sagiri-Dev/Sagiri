using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sagiri.Util.Common
{
    internal static class Constants
    {
        internal static readonly string TokenName = "spotify.json";

        internal static readonly string ClientId = "";
        internal static readonly string ClientSecret = "";

        internal static readonly Uri RedirectUri = new("http://localhost:5050/callback");
        internal static readonly int PortNo = 5050;

        internal static void DebugLog([CallerMemberName] string funcName = "", string msg = "") => Debug.WriteLine($"【DEBUG】funcName = {nameof(funcName)}, msg = {msg}...");
        internal static string GetSpotifyExePath(string userName) => $"C:/Users/{userName}/AppData/Roaming/Spotify/Spotify.exe";
    }
}
