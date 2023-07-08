using System.IO;
using System.Linq;
using System.Threading.Tasks;

using SpotifyAPI.Web;

using Sagiri.Services.Spotify.User.Interfaces;
using System.Net.Http;

namespace Sagiri.Services.Spotify.User
{
    internal class User : IUser
    {
        #region Properties

        private SpotifyClient _SpotifyClient { get; set; }

        #endregion Properties

        #region Constructor

        internal User(SpotifyClient spotifyClient) => _SpotifyClient = spotifyClient;

        #endregion Constructor

        #region Public Interface Methods

        async Task<string> IUser.GetUserId()
        {
            var user = await _SpotifyClient?.UserProfile.Current();
            return user.Id;
        }

        async Task<MemoryStream> IUser.GetUserImageStream()
        {
            var user = await _SpotifyClient?.UserProfile.Current();
            var imageUrl = user.Images.Select(x => x.Url).FirstOrDefault();
            var userImageStream = await new HttpClient(new HttpClientHandler()).GetByteArrayAsync(imageUrl);
            return new MemoryStream(userImageStream);
        }

        async Task<string> IUser.GetUserImageUrl()
        {
            var user = await _SpotifyClient?.UserProfile.Current();
            var imageUrl = user.Images.Select(x => x.Url).FirstOrDefault();
            return imageUrl;
        }

        async Task<string> IUser.GetUserProfileUrl()
        {
            var user = await _SpotifyClient?.UserProfile.Current();
            return user.ExternalUrls.Values.First();
        }

        void IUser.Dispose()
        {
            _SpotifyClient = null;
        }

        #endregion Public Methods
    }
}
