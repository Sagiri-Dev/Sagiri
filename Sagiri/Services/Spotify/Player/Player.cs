using System.Threading.Tasks;

using SpotifyAPI.Web;

using Sagiri.Services.Spotify.Player.Interfaces;
using Sagiri.Util.Common;
using Sagiri.Services.Spotify.Player.Enum;
using System.Reflection;

namespace Sagiri.Services.Spotify.Player
{
    internal class Player : IPlayer
    {
        #region Properties

        private SpotifyClient _SpotifyClient { get; set; }
        private Logger _Logger { get; set; } = Logger.GetInstance;

        #endregion Properties

        #region Constructor

        internal Player(SpotifyClient spotifyClient) => _SpotifyClient = spotifyClient;

        #endregion Constructor

        #region Public Interface Methods

        async Task IPlayer.Play()
        {
            var result = await _SpotifyClient?.Player.ResumePlayback();
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task IPlayer.Pause()
        {
            var result = await _SpotifyClient?.Player.PausePlayback();
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task IPlayer.Resume()
        {
            var result = await _SpotifyClient?.Player.ResumePlayback();
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task IPlayer.Stop()
        {
            var result = await _SpotifyClient?.Player.ResumePlayback();
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task IPlayer.Seek(int positionMs)
        {
            var result = await _SpotifyClient?.Player.SeekTo(new(positionMs));
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task IPlayer.SetRepeat(int type)
        {
            var result = await _SpotifyClient?.Player.SetRepeat(new(PlayerRepeatState.ConvertRepeatState(type)));
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task IPlayer.SetShuffle(bool isShuffle)
        {
            var result = await _SpotifyClient?.Player.SetShuffle(new(isShuffle));
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task IPlayer.SetVolume(int volume)
        {
            var result = await _SpotifyClient?.Player.SetVolume(new(volume));
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task IPlayer.SkipNext()
        {
            var result = await _SpotifyClient?.Player.SkipNext();
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task IPlayer.SkipPrevious()
        {
            var result = await _SpotifyClient?.Player.SkipPrevious();
            if (!result) _Logger.WriteLog($"Failed Spotify -> {MethodBase.GetCurrentMethod().Name}", Logger.LogLevel.Debug);
        }

        async Task<bool> IPlayer.IsPlaying()
        {
            var result = await _SpotifyClient?.Player.GetCurrentPlayback();
            return result?.IsPlaying ?? false;
        }

        #endregion Interface Methods
    }
}
