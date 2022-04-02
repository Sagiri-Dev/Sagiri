using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using SpotifyAPI.Web;

using Sagiri.Services.Spotify.Auth;
using Sagiri.Services.Spotify.Interfaces;
using Sagiri.Services.Spotify.Player.Interfaces;
using Sagiri.Services.Spotify.Track;
using Sagiri.Services.Spotify.User.Interfaces;
using Sagiri.Util.Common;

namespace Sagiri.Services.Spotify
{
    public class SpotifyService : ISpotifyService
    {
        #region Field / Members

        private static SpotifyClient _spotifyClient;
        private readonly SpotifyAuthenticator _spotifyAuthenticator;

        private IUser _IUser { get; set; }
        private IPlayer _IPlayer { get; set; }

        private CurrentlyPlaying _CurrentlyPlaying { get; set; }

        #endregion Field / Members

        #region Events

        public event Action<CurrentTrackInfo> CurrentTrackChanged;

        #endregion Events

        #region Constructor

        public SpotifyService() => _spotifyAuthenticator = new();

        #endregion Constructor

        #region Public Methods

        public async Task Initialize()
        {
            if (Process.GetProcessesByName("Spotify").Length <= 0)
                Process.Start(new ProcessStartInfo(Constants.GetSpotifyExePath(Environment.UserName)));

            await _spotifyAuthenticator.Initialize();
            await _spotifyAuthenticator.AuthenticationAsync();

            while (!_spotifyAuthenticator.IsAuthorizationCodeReceived)
            {
                // wait for authorization code received response.
                await Task.Delay(1000);
            }
            _spotifyClient = _spotifyAuthenticator.SpotifyClient;
            _IUser = new User.User(_spotifyClient);
            _IPlayer = new Player.Player(_spotifyClient);
            _CurrentlyPlaying = await _spotifyClient?.Player.GetCurrentlyPlaying(new());
        }

        public async Task Start()
        {
            // Get song information and subscribe with Rx.
            // Do not release duplicate information to the stream.
            Observable.Interval(TimeSpan.FromSeconds(2))
                .Select(async _ => _CurrentlyPlaying = await _spotifyClient?.Player?.GetCurrentlyPlaying(new()))
                .Where(_ => _CurrentlyPlaying.IsPlaying)
                .Select(_ => CurrentTrackInfo.GetCurrentTrackInfo(_CurrentlyPlaying))
                .DistinctUntilChanged()
                .Subscribe(track => CurrentTrackChanged?.Invoke(track));
        }

        #region User Info

        public async Task<string> GetUserProfileUrl() => await _IUser.GetUserProfileUrl();

        public async Task<string> GetUserId() => await _IUser.GetUserId();

        public async Task<string> GetUserImageUrl() => await _IUser.GetUserImageUrl();

        public async Task<MemoryStream> GetUserImageStream() => await _IUser.GetUserImageStream();

        #endregion User Info

        #region Track

        public async Task Play() => await _IPlayer.Play();

        public async Task Pause() => await _IPlayer.Pause();

        public async Task Resume() => await _IPlayer.Resume();

        public async Task Stop() => await _IPlayer.Stop();
         
        public async Task Seek(int positionMs) => await _IPlayer.Seek(positionMs);

        public async Task SetRepeat(int type) => await _IPlayer.SetRepeat(type);

        public async Task SetShuffle(bool isShuffle) => await _IPlayer.SetShuffle(isShuffle);

        public async Task SetVolume(int volume) => await _IPlayer.SetVolume(volume);

        public async Task SkipNext() => await _IPlayer.SkipNext();

        public async Task SkipPrevious() => await _IPlayer.SkipPrevious();

        public async Task<bool> IsPlaying() => await _IPlayer.IsPlaying();

        #endregion Track

        #endregion Public Methods
    }
}

