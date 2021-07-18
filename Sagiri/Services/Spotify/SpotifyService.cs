using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Sagiri.Services.Spotify.Auth;
using Sagiri.Services.Spotify.Interfaces;
using Sagiri.Services.Spotify.Player.Enum;
using Sagiri.Services.Spotify.Track;
using Sagiri.Util.Common;

using SpotifyAPI.Web;

namespace Sagiri.Services.Spotify
{
    public class SpotifyService : ISpotifyService
    {
        #region Field / Members

        private static SpotifyClient _spotifyClient;
        private readonly SpotifyAuthenticator _spotifyAuthenticator;
        private CurrentlyPlaying _CurrentlyPlaying { get; set; }

        #endregion Field / Members

        #region Events

        public event Action<CurrentTrackInfo> CurrentTrackChanged;

        #endregion Events

        #region Constructor

        public SpotifyService()
        {
            _ExecuteSpotify();
            _spotifyAuthenticator = new();
        }

        #endregion Constructor

        #region Private Method

        private void _ExecuteSpotify()
        {
            if (Process.GetProcessesByName("Spotify").Length <= 0)
                Process.Start(new ProcessStartInfo(Constants.GetSpotifyExePath(Environment.UserName)));
        }

        #endregion Private Method

        #region Public Method

        public async Task Initialize()
        {
            await _spotifyAuthenticator.Initialize();
            await _spotifyAuthenticator.AuthenticationAsync();
        }

        public bool IsExistCredentialFile() => File.Exists(Constants.TokenName);

        public async Task Start()
        {
            while (!_spotifyAuthenticator.IsAuthorizationCodeReceived) 
            {
                // wait for authorization code received response.
                await Task.Delay(500);
            }
            _spotifyClient = _spotifyAuthenticator.SpotifyClient;
            _CurrentlyPlaying = await _spotifyClient?.Player.GetCurrentlyPlaying(new());

            // Rx で曲情報の取得、購読先へ通知まで一括実施
            // 重複する情報はストリームに放流しない
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(async _ => _CurrentlyPlaying = await _spotifyClient?.Player.GetCurrentlyPlaying(new()))
                .Where(_ => _CurrentlyPlaying.IsPlaying)
                .Select(_ => CurrentTrackInfo.GetCurrentTrackInfo(_CurrentlyPlaying))
                .DistinctUntilChanged()
                .Subscribe(track => CurrentTrackChanged?.Invoke(track));
        }

        public async Task<string> GetUserProfileUrl()
        {
            var user = await _spotifyClient?.UserProfile.Current();
            return user.ExternalUrls.Values.First();
        }

        public async Task<string> GetUserId()
        {
            var user = await _spotifyClient?.UserProfile.Current();
            return user.Id;       
        }

        public async Task<string> GetUserImageUrl()
        {
            var user = await _spotifyClient?.UserProfile.Current();
            var imageUrl = user.Images.Select(x => x.Url).FirstOrDefault();
            return imageUrl;
        }

        public async Task<MemoryStream> GetUserImageStream()
        {
            var user = await _spotifyClient?.UserProfile.Current();
            var imageUrl = user.Images.Select(x => x.Url).FirstOrDefault();
            return new MemoryStream(new WebClient().DownloadData(imageUrl));
        }

        public async Task Play()
        {
            var result = await _spotifyClient?.Player.ResumePlayback();
            if (!result) Constants.DebugLog();
        }

        public async Task Pause()
        {
            var result = await _spotifyClient?.Player.PausePlayback();
            if (!result) Constants.DebugLog();
        }

        public async Task Resume()
        {
            var result = await _spotifyClient?.Player.ResumePlayback();
            if (!result) Constants.DebugLog();
        }

        public async Task Stop()
        {
            var result = await _spotifyClient?.Player.ResumePlayback();
            if (!result) Constants.DebugLog();
        }

        public async Task Seek(int positionMs)
        {
            var result = await _spotifyClient?.Player.SeekTo(new (positionMs));
            if (!result) Constants.DebugLog();
        }

        public async Task SetRepeat(int type)
        {
            var result = await _spotifyClient?.Player.SetRepeat(new (PlayerRepeatState.ConvertRepeatState(type)));
            if (!result) Constants.DebugLog();
        }

        public async Task SetShuffle(bool isShuffle)
        {
            var result = await _spotifyClient?.Player.SetShuffle(new (isShuffle));
            if (!result) Constants.DebugLog();
        }

        public async Task SetVolume(int volume)
        {
            var result = await _spotifyClient?.Player.SetVolume(new (volume));
            if (!result) Constants.DebugLog();
        }

        public async Task SkipNext()
        {
            var result = await _spotifyClient?.Player.SkipNext();
            if (!result) Constants.DebugLog();
        } 

        public async Task SkipPrevious()
        {
            var result = await _spotifyClient?.Player.SkipPrevious();
            if (!result) Constants.DebugLog();
         }

        public async Task<bool> IsPlaying()
        {
            var result = await _spotifyClient?.Player.GetCurrentPlayback();
            return result?.IsPlaying ?? false;
        }

        #endregion Public Method
    }
}

