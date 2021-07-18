using System.IO;
using System.Linq;
using System.Net;

using SpotifyAPI.Web;

namespace Sagiri.Services.Spotify.Track
{
    public class CurrentTrackInfo
    {
        private string _ArtworkUrl { get; set; }
        private static FullTrack _CurrentTrack { get; set; }
        private static MemoryStream _ArtworkData { get; set; }
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
        public MemoryStream Artwork
        {
            get => _ArtworkData;
            private set => _ArtworkData = new MemoryStream(new WebClient().DownloadData(_ArtworkUrl));
        }

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
            _ArtworkUrl = artworkUrl;
            _ArtworkData = new MemoryStream();
        }

        internal static CurrentTrackInfo GetCurrentTrackInfo(CurrentlyPlaying currentlyPlaying)
        {
            CurrentTrackInfo currentTrackinfo = default!;
            if (currentlyPlaying?.Item is FullTrack track)
            {
                var trackTitle = track.Name;
                var trackNumber = track.DiscNumber.ToString();
                var trackId = track.Album.Id;

                (string trackDuration, int trackRawDuration) = _GetDuration(track);
                var artist = track.Artists.Select(x => x.Name).FirstOrDefault();
                var album = track.Album.Name;
                var artworkUrl = track.Album.Images.Select(x => x.Url).FirstOrDefault();
                var releaseDate = track.Album.ReleaseDate;
                var previewUrl = track.PreviewUrl;

                currentTrackinfo = new CurrentTrackInfo(
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
                _CurrentTrack = track;
            }

            return currentTrackinfo;
        }

        private static (string, int) _GetDuration(FullTrack track)
        {
            var totalSec = track.DurationMs / 1000;
            var min = totalSec / 60;
            var sec = totalSec % 60;
            return ($"{min:D2}:{sec:D2}", totalSec);
        }
    }
}
