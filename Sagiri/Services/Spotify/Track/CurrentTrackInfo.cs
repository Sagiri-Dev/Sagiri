using System;
using System.Linq;
using SpotifyAPI.Web;

using Logger = Sagiri.Util.Common.Logger;

namespace Sagiri.Services.Spotify.Track
{
    public class CurrentTrackInfo : IEquatable<CurrentTrackInfo>
    {
        #region Properties

        private static Logger _Logger { get; set; } = Logger.GetInstance;
        private static CurrentTrackInfo _CurrentTrackInfo { get; set; } = new();
        public string TrackTitle { get; private set; }
        public string TrackNumber { get; private set; }
        public string TrackId { get; private set; }
        public string TrackDuration { get; private set; }
        public int TrackRawDuration { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public string ArtworkUrl { get; private set; }
        public string ReleaseDate { get; private set; }
        public string PreviewUrl { get; private set; }
        public static bool IsCurrentTrackChanged { get; private set; } = false;

        #endregion Properties

        #region Constructor

        public CurrentTrackInfo(
            string trackTitle = "-", 
            string trackNumber = "--.",
            string trackId = "0000",
            string trackDuration = "--:--",
            int trackRawDuration = 0,
            string artist = "-",
            string album = "-", 
            string artworkUrl = "",
            string releaseDate = "1900/1/1",
            string previewUrl = "")
        {
            TrackTitle = trackTitle;
            TrackNumber = trackNumber;
            TrackId = trackId;
            TrackDuration = trackDuration;
            TrackRawDuration = trackRawDuration;
            Artist = artist;
            Album = album;
            ArtworkUrl = artworkUrl;
            ReleaseDate = releaseDate;
            PreviewUrl = previewUrl;
        }

        #endregion Constructor

        #region Private Methods

        private static (string, int) _GetDuration(FullTrack track)
        {
            var totalSec = track.DurationMs / 1000;
            var min = totalSec / 60;
            var sec = totalSec % 60;
            return ($"{min:D2}:{sec:D2}", totalSec);
        }

        private static (string, int) _GetDuration(int durationMs)
        {
            var totalSec = durationMs / 1000;
            var min = totalSec / 60;
            var sec = totalSec % 60;
            return ($"{min:D2}:{sec:D2}", totalSec);
        }

        #endregion Private Methods

        #region Internal Methods

        internal static CurrentTrackInfo GetCurrentTrackInfo(CurrentlyPlaying currentlyPlaying)
        {
            CurrentTrackInfo currentTrackInfo = default!;
            if (currentlyPlaying?.Item is FullTrack track)
            {
                _Logger.WriteLog("[CurrentTrackInfo - GetCurrentTrackInfo()] FullTrack statements called...", Logger.LogLevel.Debug);

                var trackTitle = track.Name;
                var trackNumber = track.DiscNumber.ToString();
                var trackId = track.Album.Id;

                (string trackDuration, int trackRawDuration) = _GetDuration(track);
                var artist = track.Artists.Select(x => x.Name).FirstOrDefault();
                var album = track.Album.Name;
                var artworkUrl = track.Album.Images.Select(x => x.Url).FirstOrDefault();
                var releaseDate = track.Album.ReleaseDate;
                var previewUrl = track.PreviewUrl;

                currentTrackInfo = new CurrentTrackInfo(
                    trackTitle: trackTitle,
                    trackNumber: trackNumber,
                    trackId: trackId,
                    trackDuration: trackDuration,
                    trackRawDuration: trackRawDuration,
                    artist: artist,
                    album: album,
                    artworkUrl: artworkUrl,
                    releaseDate: releaseDate,
                    previewUrl: previewUrl
                );
                _CurrentTrackInfo = currentTrackInfo;
            }
            else if ( currentlyPlaying?.Item is PlaylistTrack<FullTrack> playlistTrack )
            {
                _Logger.WriteLog("[CurrentTrackInfo - GetCurrentTrackInfo()] PlaylistTrack<FullTrack> statements called...", Logger.LogLevel.Info);
                var trackTitle = playlistTrack.Track.Name;
                var trackNumber = playlistTrack.Track.DiscNumber.ToString();
                var trackId = playlistTrack.Track.Album.Id;

                (string trackDuration, int trackRawDuration) = _GetDuration(playlistTrack.Track);
                var artist = playlistTrack.Track.Artists.Select(x => x.Name).FirstOrDefault();
                var album = playlistTrack.Track.Album.Name;
                var artworkUrl = playlistTrack.Track.Album.Images.Select(x => x.Url).FirstOrDefault();
                var releaseDate = playlistTrack.Track.Album.ReleaseDate;
                var previewUrl = playlistTrack.Track.PreviewUrl;

                currentTrackInfo = new CurrentTrackInfo(
                    trackTitle: trackTitle,
                    trackNumber: trackNumber,
                    trackId: trackId,
                    trackDuration: trackDuration,
                    trackRawDuration: trackRawDuration,
                    artist: artist,
                    album: album,
                    artworkUrl: artworkUrl,
                    releaseDate: releaseDate,
                    previewUrl: previewUrl
                );
                _CurrentTrackInfo = currentTrackInfo;
            }
            return currentTrackInfo;
        }

        public new bool Equals(CurrentTrackInfo trackInfo) => _CurrentTrackInfo == trackInfo;

        public void Dispose()
        {
            _CurrentTrackInfo = null;
            _Logger = null;
        }

        #endregion Internal Methods
    }
}
