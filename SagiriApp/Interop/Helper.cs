﻿using System.Linq;
using System.Text;
using System.Windows;
using Sagiri.Services.Spotify.Track;

namespace SagiriApp.Interop
{
    internal static class Helper
    {
        internal static Window? GetActiveWindow => 
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

        internal static string GenerateTrackText(string text, CurrentTrackInfo trackInfo)
        {
            StringBuilder sb = new(text);

            sb = sb.Replace("{Title}", "{0}");
            sb = sb.Replace("{Artist}", "{1}");
            sb = sb.Replace("{Album}", "{2}");
            sb = sb.Replace("{TrackNum}", "{3:D2}");

            return string.Format(
                sb.ToString(),
                trackInfo.TrackTitle,
                trackInfo.Artist,
                trackInfo.Album,
                trackInfo.TrackNumber
            );
        }

        internal static string RenderPreview(string text)
        {
            CurrentTrackInfo trackInfo = new()
            {
                Album = "メルト 10th ANNIVERSARY MIX",
                Artist = "ryo (supercell) - やなぎなぎ",
                TrackTitle = "メルト 10th ANNIVERSARY MIX",
                TrackNumber = "1",
                ReleaseDate = "2017/12/24",
            };

            return GenerateTrackText(text, trackInfo);
        }
    }
}
