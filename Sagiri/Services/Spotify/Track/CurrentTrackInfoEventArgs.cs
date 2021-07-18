using System;

namespace Sagiri.Services.Spotify.Track
{
    internal class CurrentTrackInfoEventArgs : EventArgs
    {
        internal CurrentTrackInfo CurrentTrackInfo { get; private set; }
        internal CurrentTrackInfoEventArgs(CurrentTrackInfo currentTrackInfo) =>
            this.CurrentTrackInfo = currentTrackInfo;

    }
}
