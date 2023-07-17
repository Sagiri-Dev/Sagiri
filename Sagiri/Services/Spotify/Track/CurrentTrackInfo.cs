using System.Linq;
using System.Text;
using Sagiri.Util.Common;
using SpotifyAPI.Web;
using static SpotifyAPI.Web.PlaylistRemoveItemsRequest;

namespace Sagiri.Services.Spotify.Track
{
    public class CurrentTrackInfo
    {
        #region Properties

        private static Logger _Logger = Logger.GetInstance;

        public string TrackTitle { get; set; }
        public string TrackNumber { get; set; }
        public string TrackId { get; set; }
        public string TrackDuration { get; set; }
        public int TrackRawDuration { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string ArtworkUrl { get; set; }
        public string ReleaseDate { get; set; }
        public string PreviewUrl { get; set; }
        public bool IsPlaying { get; set; }

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
            string previewUrl = "",
            bool isPlaying = false)
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
            IsPlaying = isPlaying;
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

        #endregion Private Methods

        #region Internal Methods

        internal static CurrentTrackInfo GetCurrentTrackInfo(CurrentlyPlaying currentlyPlaying)
        {
            CurrentTrackInfo currentTrackInfo = default!;
            if (currentlyPlaying?.Item is FullTrack track)
            {
                var trackTitle = track.Name;
                var trackNumber = track.DiscNumber.ToString();
                var trackId = track.Album.Id;

                (string trackDuration, int trackRawDuration) = _GetDuration(track);
                
                var artists = track.Artists.Select(x => x.Name);
                StringBuilder sb = new();
                foreach (var (item, index) in artists.Select((item, index) => (item, index)))
                {
                    if (artists.Count() > 1)
                    {
                        if (index != artists.Count() - 1)
                            sb.Append($"{item}, ");
                        else
                            sb.Append($"{item}");
                    }
                    else
                        sb.Append(item);
                }

                var album = track.Album.Name;
                var artworkUrl = track.Album.Images.Select(x => x.Url).FirstOrDefault();
                var releaseDate = track.Album.ReleaseDate;
                var previewUrl = track.PreviewUrl;
                var isPlaying = currentlyPlaying.IsPlaying;

                currentTrackInfo = new CurrentTrackInfo(
                    trackTitle: trackTitle,
                    trackNumber: trackNumber,
                    trackId: trackId,
                    trackDuration: trackDuration,
                    trackRawDuration: trackRawDuration,
                    artist: sb.ToString(),
                    album: album,
                    artworkUrl: artworkUrl,
                    releaseDate: releaseDate,
                    previewUrl: previewUrl,
                    isPlaying: isPlaying
                );
            }
            return currentTrackInfo;
        }

        #endregion Internal Methods
    }
}
