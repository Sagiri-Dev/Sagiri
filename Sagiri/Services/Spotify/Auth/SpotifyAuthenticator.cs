using System;
using System.IO;
using System.Threading.Tasks;

using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;

using Sagiri.Exceptions;
using Sagiri.Util.Configuration;

using static Sagiri.Util.Common.Constants;
using Sagiri.Util.Common;

namespace Sagiri.Services.Spotify.Auth
{
    /// <summary>
    /// Authentication processing to access Spotify.
    /// </summary>
    internal class SpotifyAuthenticator : IDisposable
    {
        #region Properties

        private static SpotifyCredentialConfig _SpotifyCredentialConfig { get; set; }

        private static EmbedIOAuthServer _Server { get; set; }
        private Logger _Logger { get; set; }

        private string _Challenge { get; set; }
        private string _Verifier { get; set; }

        public SpotifyClient SpotifyClient { get; private set; }
        public bool IsAuthorizationCodeReceived { get; set; } = false;

        #endregion Properties

        #region Constructor

        internal SpotifyAuthenticator()
        {
            _SpotifyCredentialConfig = SpotifyCredentialConfig.Instance;
            (_Verifier, _Challenge) = PKCEUtil.GenerateCodes(120);
            _Logger = Logger.GetInstance;
        }

        #endregion Constructor

        #region Private Methods

        private async Task<bool> _PreAuthentication()
        {
            try
            {
                var config = SpotifyClientConfig.CreateDefault();
                var request = new ClientCredentialsRequest(_SpotifyCredentialConfig.ClientId, _SpotifyCredentialConfig.ClientSecret);
                var response = await new OAuthClient(config).RequestToken(request);

                _SpotifyCredentialConfig.AccessToken ??= response.AccessToken;
                _SpotifyCredentialConfig.CreatedAt = response.CreatedAt;
                _SpotifyCredentialConfig.IsExpired = response.IsExpired;

                await _SpotifyCredentialConfig.SaveCredentialAsync();

                if (_SpotifyCredentialConfig.IsNullOrEmpty())
                    throw new Exception();

                return true;
            }
            catch (Exception)
            {
                var logMessage = "Configuration any records of null or empty. please check your tokens info.";
                _Logger.WriteLog($"[Sagiri] - {logMessage}", Logger.LogLevel.Fatal);

                return false;
            }
        }

        private async Task _DisposeAsync()
        {
            if (_Server is not null)
            {
                await _Server.Stop();
                _Server.Dispose();
                _Server = null;
            }
        }

        #endregion Private Methods

        #region Internal Methods

        internal async Task<bool> InitializeAsync()
        {
            if (!File.Exists(GetCredentialFileName("spotify")))
            {
                _SpotifyCredentialConfig = new SpotifyCredentialConfig() { 
                    ClientId = ClientId, 
                    ClientSecret = ClientSecret 
                };
                await _SpotifyCredentialConfig.SaveCredentialAsync();
            }
            else
            {
                _SpotifyCredentialConfig = await new SpotifyCredentialConfig().LoadCredentialAsync();
            }

            var initalizeStatus = await _PreAuthentication();
            _Logger.WriteLog("[Sagiri] - Finished reading spotify credential info.", Logger.LogLevel.Info);

            return initalizeStatus;
        }

        internal async Task AuthenticationAsync()
        {
            _Logger.WriteLog("[Sagiri] - Started spotify authorization process...", Logger.LogLevel.Info);

            _Server = new EmbedIOAuthServer(RedirectUri, PortNo);
            await _Server.Start();

            _Server.AuthorizationCodeReceived += async (_, response) =>
            {
                await _DisposeAsync();

                OAuthClient client = new();
                var tokenResponse = await client.RequestToken(new PKCETokenRequest(_SpotifyCredentialConfig.ClientId, response.Code, RedirectUri, _Verifier));
                PKCEAuthenticator authenticator = new(_SpotifyCredentialConfig.ClientId, tokenResponse);

                var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(authenticator);
                IsAuthorizationCodeReceived = true;
                SpotifyClient = new SpotifyClient(config);
                _Logger.WriteLog("[Sagiri] - Finished spotify authorization process!", Logger.LogLevel.Info);
            };

            var loginRequest = new LoginRequest(_Server.BaseUri, _SpotifyCredentialConfig.ClientId, LoginRequest.ResponseType.Code)
            {
                CodeChallengeMethod = "S256",
                CodeChallenge = _Challenge,
                Scope = new[] {
                    Scopes.AppRemoteControl,
                    Scopes.UserModifyPlaybackState,
                    Scopes.UserReadCurrentlyPlaying,
                    Scopes.UserReadPlaybackState,
                    Scopes.UserReadRecentlyPlayed
                }
            };

            var uri = loginRequest.ToUri();
            try
            {
                BrowserUtil.Open(uri);
            }
            catch (Exception)
            {
                _Logger.WriteLog($"[Sagiri] - Unable to open a browser.  Please manually open: {uri}", Logger.LogLevel.Error);
                throw new SagiriException($"Unable to open a browser.  Please manually open: {uri}");
            }
        }

        public void Dispose()
        {
            SpotifyClient = null;
            _Logger = null;
            _SpotifyCredentialConfig = null;
        }

        #endregion Internal Methods
    }
}
