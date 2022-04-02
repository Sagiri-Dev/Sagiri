using System.Threading.Tasks;

namespace Sagiri.Services.Spotify.Interfaces
{
    internal interface ISpotifyService
    {
        Task Initialize();
        Task Start();
    }
}
