using System.Threading.Tasks;
using Sagiri.Services.Spotify.Player.Interfaces;
using Sagiri.Services.Spotify.Track.Interfaces;
using Sagiri.Services.Spotify.User.Interfaces;

namespace Sagiri.Services.Spotify.Interfaces
{
    internal interface ISpotifyService : IUser, IPlayer, ITrack
    {
        internal static readonly string CurrentPlayingRequestUrl =
            "https://api.spotify.com/v1/me/player/currently-playing?market=JP";

        Task Initialize();
        Task Start();
        bool IsExistCredentialFile();
    }
}
