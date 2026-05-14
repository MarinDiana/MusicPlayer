using MusicPlayer.Audio;
using MusicPlayer.Controllers;

namespace MusicPlayer.Observers
{
    public class AutoNextHandler
    {
        private readonly AudioPlayer _audioPlayer;
        private readonly PlaybackController _controller;

        public AutoNextHandler(
            AudioPlayer audioPlayer,
            PlaybackController controller)
        {
            _audioPlayer = audioPlayer;
            _controller = controller;

            _audioPlayer.TrackEnded += AudioPlayer_TrackEnded;
        }

        private void AudioPlayer_TrackEnded(
            object? sender,
            EventArgs e)
        {
            _controller.Next();
        }

        public void Unsubscribe()
        {
            _audioPlayer.TrackEnded -= AudioPlayer_TrackEnded;
        }
    }
}