using Sagiri.Exceptions;
using SpotifyAPI.Web;

namespace Sagiri.Services.Spotify.Player.Enum
{
    internal static class PlayerRepeatState
    {
        internal static PlayerSetRepeatRequest.State ConvertRepeatState(int type) => type switch
        {
            0 => PlayerSetRepeatRequest.State.Track,
            1 => PlayerSetRepeatRequest.State.Context,
            2 => PlayerSetRepeatRequest.State.Off,
            _ => throw new SagiriException($"Not expected type value: {type}"),
        };
    }
}
