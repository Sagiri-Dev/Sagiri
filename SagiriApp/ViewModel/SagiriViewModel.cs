using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Prism.Mvvm;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using Sagiri.Services.Spotify.Track;
using SagiriApp.Models;

namespace SagiriApp.ViewModel
{
    internal class SagiriViewModel : BindableBase, INotifyPropertyChanged, IDisposable
    {
        public AsyncReactiveCommand SpotifySubscribeCommand { get; }
        public AsyncReactiveCommand NowPlayingCommand { get; }
        public ReactiveCommand AlbumArtClickCommand { get; } = new();
        public ReactiveCommand SettingJsonSaveCommand { get; } = new();

        public ReactiveProperty<CurrentTrackInfo>? CurrentTrackInfo { get; set; }
        public ReactiveProperty<bool> IsSpotifyPlaying { get; set; }
        public ReactiveProperty<string> PostingFormat { get; set; }

        private readonly SagiriModel _SagiriModel = new();
        private readonly CompositeDisposable _cd = new();

        internal SagiriViewModel()
        {
            SpotifySubscribeCommand = new AsyncReactiveCommand().WithSubscribe(async () =>
            {
                await _SagiriModel.InitializeAsync();
                await _SagiriModel.StartAsync();

                // Wait updating CurrentTrack.
                await Task.Delay(100);

                CurrentTrackInfo = _SagiriModel.ToReactivePropertyAsSynchronized(m => m.CurrentTrackInfo.Value).AddTo(_cd);
                IsSpotifyPlaying = _SagiriModel.ToReactivePropertyAsSynchronized(m => m.IsSpotifyPlaying.Value).AddTo(_cd);
                PostingFormat = _SagiriModel.ToReactivePropertyAsSynchronized(m => m.PostingFormat.Value).AddTo(_cd);

                RaisePropertyChanged(nameof(CurrentTrackInfo));
                RaisePropertyChanged(nameof(IsSpotifyPlaying));
                RaisePropertyChanged(nameof(PostingFormat));
            }).AddTo(_cd);

            NowPlayingCommand = new AsyncReactiveCommand().WithSubscribe(async () => await _SagiriModel.PostMisskeyAsync()).AddTo(_cd);
            AlbumArtClickCommand.Subscribe(_ => SagiriModel.ViewAlbumArt()).AddTo(_cd);
            SettingJsonSaveCommand.Subscribe(_ => _SagiriModel.SaveSetting()).AddTo(_cd);
        }

        public void Dispose() => _cd.Dispose();
    }
}