using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Sagiri.Exceptions;
using Sagiri.Util;

using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;

using static Sagiri.Util.Common.Constants;

namespace Sagiri.Services.Spotify.Auth
{
    internal class SpotifyAuthenticator
    {
        private static JsonConfig _json = new();

        private static EmbedIOAuthServer _Server { get; set; }
        private static HttpClient _Client { get; set; }
        private string _Challenge { get; set; }
        private string _Verifier { get; set; }

        public SpotifyClient SpotifyClient { get; private set; }
        public bool IsAuthorizationCodeReceived { get; private set; } = false;

        internal SpotifyAuthenticator()
        {
            (_Verifier, _Challenge) = PKCEUtil.GenerateCodes(120);
            _Client = new HttpClient(new HttpClientHandler());
        }

        internal async Task Initialize()
        {
            if (!File.Exists(TokenName))
            {
                _json = new JsonConfig() { ClientId = ClientId, ClientSecret = ClientSecret };
                await _json.SaveTokenAsync();
            }
            else
            {
                _json = await new JsonConfig().LoadTokenAsync();
            }
            await _PreAuthentication();
        }

        private async Task _PreAuthentication()
        {
            try
            {
                var config = SpotifyClientConfig.CreateDefault();
                var request = new ClientCredentialsRequest(_json.ClientId, _json.ClientSecret);
                var response = await new OAuthClient(config).RequestToken(request);

                _json.AccessToken ??= response.AccessToken;
                _json.CreatedAt = response.CreatedAt;
                _json.IsExpired = response.IsExpired;
                await _json.SaveTokenAsync();

                if (_json.IsNullOrEmpty())
                    throw new SagiriException("Configuration any records of null or empty. please check your tokens info.");
            }
            catch (Exception)
            {
                throw new SagiriException("Exception is _PreAuthentication()");
            }
        }

        internal async Task AuthenticationAsync()
        {
            Debug.WriteLine("Started the Spotify authorization process...");

            _Server = new EmbedIOAuthServer(RedirectUri, PortNo);
            await _Server.Start();

            _Server.AuthorizationCodeReceived += async (_, response) =>
            {
                await _DisposeAsync();

                OAuthClient client = new();
                var tokenResponse = await client.RequestToken(new PKCETokenRequest(_json.ClientId, response.Code, RedirectUri, _Verifier));
                var authenticator = new PKCEAuthenticator(_json.ClientId, tokenResponse);

                var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(authenticator);
                IsAuthorizationCodeReceived = true;
                SpotifyClient = new SpotifyClient(config);
            };

            var loginRequest = new LoginRequest(_Server.BaseUri, _json.ClientId, LoginRequest.ResponseType.Code)
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
                throw new SagiriException($"Unable to open a browser.  Please manually open: {uri}");
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
    }
}
