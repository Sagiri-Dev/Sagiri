using System;

namespace Sagiri.Services.Spotify.Track.Interfaces
{
    internal interface ITrack
    {
        #region Events

        event Action<CurrentTrackInfo> CurrentTrackChanged;

        #endregion Events
    }
}
