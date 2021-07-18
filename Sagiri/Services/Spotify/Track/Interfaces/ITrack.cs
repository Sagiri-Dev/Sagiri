using System;

namespace Sagiri.Services.Spotify.Track.Interfaces
{
    internal interface ITrack
    {
        event Action<CurrentTrackInfo> CurrentTrackChanged;
    }
}
