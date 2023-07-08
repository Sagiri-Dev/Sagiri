using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using SpotifyAPI.Web;

using Sagiri.Services.Spotify.Auth;
using Sagiri.Services.Spotify.Interfaces;
using Sagiri.Services.Spotify.Player.Interfaces;
using Sagiri.Services.Spotify.Track;
using Sagiri.Services.Spotify.User.Interfaces;
using Sagiri.Util.Common;
using Sagiri.Exceptions;

namespace Sagiri.Services.Spotify
{
    public class SpotifyService : ISpotifyService
    {
        #region Field / Members

        private SpotifyClient _spotifyClient;
        private readonly SpotifyAuthenticator _spotifyAuthenticator = new();

        private IUser _IUser { get; set; }
        private IPlayer _IPlayer { get; set; }
        private Logger _Logger { get; set; }
        private CurrentlyPlaying _CurrentlyPlaying { get; set; }

        #endregion Field / Members

        #region Events

        private event Action<CurrentTrackInfo> _CurrentTrackChanged;
        event Action<CurrentTrackInfo> ISpotifyService.CurrentTrackChanged
        {
            add => _CurrentTrackChanged += value;
            remove => _CurrentTrackChanged -= value;
        }

        private event Action _CurrentTrackErrorDetected;
        event Action ISpotifyService.CurrentTrackErrorDetected
        {
            add => _CurrentTrackErrorDetected += value;
            remove => _CurrentTrackErrorDetected -= value;
        }

        #endregion Events

        #region Constructor

        public SpotifyService() => _Logger = Logger.GetInstance;

        #endregion Constructor

        #region Public Methods

        async ValueTask<SpotifyService> ISpotifyService.BuildSpotifyServiceAsync()
        {
            //await this._initialize();
            return this;
        }

        async ValueTask ISpotifyService.InitializeAsync()
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

        async ValueTask ISpotifyService.StartAsync(CancellationToken ct)
        {
            // Get song information and subscribe with Rx.
            // Do not release duplicate information to the stream.
            await Task.Run(() =>
            {
                try
                {
                    var observer = Observable.Interval(TimeSpan.FromSeconds(1))
                        .Where(_ => _CurrentlyPlaying is not null && _CurrentlyPlaying.IsPlaying)
                        .Select(async _ => _CurrentlyPlaying = await _spotifyClient?.Player?.GetCurrentlyPlaying(new()))
                        .Select(track => CurrentTrackInfo.GetCurrentTrackInfo(_CurrentlyPlaying))
                        .Distinct()
                        .Where(track => track is not null)
                        .Subscribe(track => _CurrentTrackChanged?.Invoke(track));

                    if (ct.IsCancellationRequested)
                    {
                        observer.Dispose();
                        _CurrentTrackErrorDetected.Invoke();
                    }

                    _Logger.WriteLog("[SpotifyService] - Polling Start....", Logger.LogLevel.Debug);
                }
                catch (Exception ex)
                {
                    throw new SagiriException(ex.Message);
                }
            }, ct);
        }
        
        #region User Info

        async ValueTask<string> ISpotifyService.GetUserProfileUrl() => await _IUser.GetUserProfileUrl();

        async ValueTask<string> ISpotifyService.GetUserId() => await _IUser.GetUserId(); 

        async ValueTask<string> ISpotifyService.GetUserImageUrl() => await _IUser.GetUserImageUrl();

        async ValueTask<MemoryStream> ISpotifyService.GetUserImageStream() => await _IUser.GetUserImageStream();

        #endregion User Info

        #region Track

        async ValueTask ISpotifyService.Play() => await _IPlayer.Play();

        async ValueTask ISpotifyService.Pause() => await _IPlayer.Pause();

        async ValueTask ISpotifyService.Resume() => await _IPlayer.Resume();

        async ValueTask ISpotifyService.Stop() => await _IPlayer.Stop();
         
        async ValueTask ISpotifyService.Seek(int positionMs) => await _IPlayer.Seek(positionMs);

        async ValueTask ISpotifyService.SetRepeat(int type) => await _IPlayer.SetRepeat(type);

        async ValueTask ISpotifyService.SetShuffle(bool isShuffle) => await _IPlayer.SetShuffle(isShuffle);

        async ValueTask ISpotifyService.SetVolume(int volume) => await _IPlayer.SetVolume(volume);

        async ValueTask ISpotifyService.SkipNext() => await _IPlayer.SkipNext();

        async ValueTask ISpotifyService.SkipPrevious() => await _IPlayer.SkipPrevious();

        async ValueTask<bool> ISpotifyService.IsPlaying() => await _IPlayer.IsPlaying();

        #endregion Track

        void ISpotifyService.Dispose()
        {
            _spotifyAuthenticator.Dispose();
            _IUser.Dispose();
            _IPlayer.Dispose();

            _CurrentlyPlaying = null;
            _spotifyClient = null;
            _Logger = null;
        }

        #endregion Public Methods
    }
}

 