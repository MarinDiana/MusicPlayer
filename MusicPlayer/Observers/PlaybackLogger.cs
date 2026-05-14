using System.IO;
using MusicPlayer.Audio;

namespace MusicPlayer.Observers
{
    public class PlaybackLogger
    {
        private readonly AudioPlayer _audioPlayer;
        private readonly string _filePath = "playback_log.txt";

        public PlaybackLogger(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;

            _audioPlayer.PropertyChanged += AudioPlayer_PropertyChanged;
            _audioPlayer.TrackEnded += AudioPlayer_TrackEnded;
        }

        private void AudioPlayer_PropertyChanged(
            object? sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State" ||
                e.PropertyName == "CurrentTrack")
            {
                Log(e.PropertyName + " changed");
            }
        }

        private void AudioPlayer_TrackEnded(
            object? sender,
            EventArgs e)
        {
            Log("TrackEnded");
        }

        private void Log(string message)
        {
            File.AppendAllText(
                _filePath,
                DateTime.Now + " - " + message + Environment.NewLine);
        }

        public void Unsubscribe()
        {
            _audioPlayer.PropertyChanged -= AudioPlayer_PropertyChanged;
            _audioPlayer.TrackEnded -= AudioPlayer_TrackEnded;
        }
    }
}