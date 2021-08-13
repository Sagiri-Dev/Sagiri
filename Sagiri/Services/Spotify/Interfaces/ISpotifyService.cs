using System.Threading.Tasks;

namespace Sagiri.Services.Spotify.Interfaces
{
    internal interface ISpotifyService
    {
        internal static readonly string CurrentPlayingRequestUrl =
            "https://api.spotify.com/v1/me/player/currently-playing?market=JP";

        Task Initialize();
        Task Start();
        bool IsExistCredentialFile();
    }
}
