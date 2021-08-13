using System.Threading.Tasks;

using SpotifyAPI.Web;

using Sagiri.Services.Spotify.Player.Interfaces;
using Sagiri.Util.Common;
using Sagiri.Services.Spotify.Player.Enum;

namespace Sagiri.Services.Spotify.Player
{
    internal class Player : IPlayer
    {
        private SpotifyClient _SpotifyClient { get; set; }

        internal Player(SpotifyClient spotifyClient) => _SpotifyClient = spotifyClient;

        async Task IPlayer.Play()
        {
            var result = await _SpotifyClient?.Player.ResumePlayback();
            if (!result) Constants.DebugLog();
        }

        async Task IPlayer.Pause()
        {
            var result = await _SpotifyClient?.Player.PausePlayback();
            if (!result) Constants.DebugLog();
        }

        async Task IPlayer.Resume()
        {
            var result = await _SpotifyClient?.Player.ResumePlayback();
            if (!result) Constants.DebugLog();
        }

        async Task IPlayer.Stop()
        {
            var result = await _SpotifyClient?.Player.ResumePlayback();
            if (!result) Constants.DebugLog();
        }

        async Task IPlayer.Seek(int positionMs)
        {
            var result = await _SpotifyClient?.Player.SeekTo(new(positionMs));
            if (!result) Constants.DebugLog();
        }

        async Task IPlayer.SetRepeat(int type)
        {
            var result = await _SpotifyClient?.Player.SetRepeat(new(PlayerRepeatState.ConvertRepeatState(type)));
            if (!result) Constants.DebugLog();
        }

        async Task IPlayer.SetShuffle(bool isShuffle)
        {
            var result = await _SpotifyClient?.Player.SetShuffle(new(isShuffle));
            if (!result) Constants.DebugLog();
        }

        async Task IPlayer.SetVolume(int volume)
        {
            var result = await _SpotifyClient?.Player.SetVolume(new(volume));
            if (!result) Constants.DebugLog();
        }

        async Task IPlayer.SkipNext()
        {
            var result = await _SpotifyClient?.Player.SkipNext();
            if (!result) Constants.DebugLog();
        }

        async Task IPlayer.SkipPrevious()
        {
            var result = await _SpotifyClient?.Player.SkipPrevious();
            if (!result) Constants.DebugLog();
        }

        async Task<bool> IPlayer.IsPlaying()
        {
            var result = await _SpotifyClient?.Player.GetCurrentPlayback();
            return result?.IsPlaying ?? false;
        }
    }
}
