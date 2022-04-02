using System;

namespace Sagiri.Services.Spotify.Track
{
    internal class CurrentTrackInfoEventArgs : EventArgs
    {
        #region Properties

        internal CurrentTrackInfo CurrentTrackInfo { get; private set; }

        #endregion

        #region Constructor

        internal CurrentTrackInfoEventArgs(CurrentTrackInfo currentTrackInfo) =>
            this.CurrentTrackInfo = currentTrackInfo;

        #endregion Constructor
    }
}
