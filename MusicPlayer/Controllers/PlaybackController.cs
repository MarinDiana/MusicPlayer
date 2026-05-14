using MusicPlayer.Audio;
using MusicPlayer.Models;
using MusicPlayer.Strategies;

namespace MusicPlayer.Controllers
{
    public class PlaybackController
    {
        private readonly AudioPlayer _audioPlayer;

        private IPlaybackStrategy _strategy;

        public Playlist Playlist { get; }

        public AudioPlayer AudioPlayer
        {
            get { return _audioPlayer; }
        }

        public IPlaybackStrategy Strategy
        {
            get { return _strategy; }
        }

        public PlaybackController(
            AudioPlayer audioPlayer,
            Playlist playlist,
            IPlaybackStrategy strategy)
        {
            _audioPlayer = audioPlayer;

            Playlist = playlist;

            _strategy = strategy;

            _audioPlayer.TrackEnded +=
                AudioPlayer_TrackEnded;
        }

        private void AudioPlayer_TrackEnded(
            object? sender,
            EventArgs e)
        {
            Next();
        }

        public void SetStrategy(
            IPlaybackStrategy strategy)
        {
            _strategy = strategy;

            _strategy.Reset(Playlist);
        }

        public void Play()
        {
            _audioPlayer.Play();
        }

        public void Pause()
        {
            _audioPlayer.Pause();
        }

        public void Stop()
        {
            _audioPlayer.Stop();
        }

        public void Load(Track track)
        {
            _audioPlayer.Load(track);
        }

        public void Next()
        {
            Track? nextTrack =
                _strategy.GetNextTrack(
                    Playlist,
                    _audioPlayer.CurrentTrack);

            if (nextTrack == null)
                return;

            _audioPlayer.Load(nextTrack);

            _audioPlayer.Play();
        }

        public void Previous()
        {
            Track? previousTrack =
                _strategy.GetPreviousTrack(
                    Playlist,
                    _audioPlayer.CurrentTrack);

            if (previousTrack == null)
                return;

            _audioPlayer.Load(previousTrack);

            _audioPlayer.Play();
        }
    }
}