# Sagiri

Sagiri makes full use of Spotify-WebAPI It's third party app.
Getting general spotify current track information tracks.
Let's enjoy! 

### ○Features

* ✅ Supports posting current track information at Twitter.
* ✅ Supports `.NET 5.
* ❌ Logging supported.
* ❌ Spotify Control Module UI fixed.

### ○Capture Images

* MainWindow  
![Sagiri-MainWindow](Sagiri-MainWindow.png)

* InfoWindow  
![Sagiri-InfoWindow](Sagiri-InfoWindow.png)

### ○Docs and Usage

```csharp
// Before Settings.
// Sagiri > Util > Constants.cs
internal static readonly string ClientId = "your spotify-development application client id.";
internal static readonly string ClientSecret = "your spotify-development application client secret.";

// WinForms
using Sagiri.Services.Spotify;
using Sagiri.Services.Spotify.Track;

public async void Form1_Load(object sender, EventArgs e)
{
    SpotifyService spotifyService = new();
    var currentTrackInfo = new CurrentTrackInfo();

    // registar for callback function (gettting currentTrackInfo.) 
    spotifyService.CurrentTrackChanged += _OnSpotifyCurrentlyPlayingChanged;

    if (spotifyService.IsExistCredentialFile())
    {
        await spotifyService.Initialize();
        await spotifyService.Start().ConfigureAwait(false);
    }

    // Be sure to explicitly initialize.
    _OnSpotifyCurrentlyPlayingChanged(_CurrentTrackInfo);

    // Getting and Setting Spotify current playing track albumart. 
    var accountImageStream = await spotifyService.GetUserImageStream();
    AccountPanel.BackgroundImage = Image.FromStream(accountImageStream) ?? Resources.account;
}
```
