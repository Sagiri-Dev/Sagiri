using System;
using System.Threading.Tasks;

namespace Sagiri.Services.Spotify.Player.Interfaces
{
    internal interface IPlayer
    {
        Task Play();
        Task Pause();
        Task Resume();
        Task Stop();
        Task Seek(int positionMs);
        Task SetRepeat(int type);
        Task SetShuffle(bool isShuffle);
        Task SetVolume(int volume);
        Task SkipNext();
        Task SkipPrevious();
        Task<bool> IsPlaying();
        void Dispose();
    }
}
