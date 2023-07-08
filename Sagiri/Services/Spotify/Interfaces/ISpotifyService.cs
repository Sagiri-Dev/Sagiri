using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Sagiri.Services.Spotify.Track;

namespace Sagiri.Services.Spotify.Interfaces
{
    public interface ISpotifyService
    {
        event Action<CurrentTrackInfo> CurrentTrackChanged;
        event Action CurrentTrackErrorDetected;

        ValueTask<bool> InitializeAsync();

        ValueTask StartAsync(CancellationToken ct);

        ValueTask<string> GetUserProfileUrl();

        ValueTask<string> GetUserId();

        ValueTask<string> GetUserImageUrl();

        ValueTask<MemoryStream> GetUserImageStream();

        ValueTask Play();

        ValueTask Pause();

        ValueTask Resume();

        ValueTask Stop();

        ValueTask Seek(int positionMs);

        ValueTask SetRepeat(int type);

        ValueTask SetShuffle(bool isShuffle);

        ValueTask SetVolume(int volume);

        ValueTask SkipNext();

        ValueTask SkipPrevious();

        ValueTask<bool> IsPlaying();

        void Dispose();
    }
}
