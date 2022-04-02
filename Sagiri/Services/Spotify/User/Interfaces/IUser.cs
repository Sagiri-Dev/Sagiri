using System.IO;
using System.Threading.Tasks;

namespace Sagiri.Services.Spotify.User.Interfaces
{
    internal interface IUser
    {
        #region Interface Method

        Task<string> GetUserProfileUrl();
        Task<string> GetUserId();
        Task<string> GetUserImageUrl();
        Task<MemoryStream> GetUserImageStream();

        #endregion Interface Method
    }
}
