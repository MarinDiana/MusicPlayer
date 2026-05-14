using MusicPlayer.Audio;
using MusicPlayer.Models;

namespace MusicPlayer.Observers
{
    public class StatisticsTracker
    {
        private readonly AudioPlayer _audioPlayer;

        private readonly Dictionary<string, int> _artistMinutes =
            new Dictionary<string, int>();

        private readonly Dictionary<string, int> _playCount =
            new Dictionary<string, int>();

        public StatisticsTracker(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;

            _audioPlayer.TrackEnded += AudioPlayer_TrackEnded;
        }

        private void AudioPlayer_TrackEnded(
            object? sender,
            EventArgs e)
        {
            Track? track =
                _audioPlayer.CurrentTrack;

            if (track == null)
                return;

            if (!_artistMinutes.ContainsKey(track.Artist))
            {
                _artistMinutes[track.Artist] = 0;
            }

            _artistMinutes[track.Artist] +=
                (int)track.Duration.TotalMinutes;

            if (!_playCount.ContainsKey(track.Title))
            {
                _playCount[track.Title] = 0;
            }

            _playCount[track.Title]++;
        }

        public IReadOnlyDictionary<string, int> ArtistMinutes
        {
            get { return _artistMinutes; }
        }

        public IReadOnlyDictionary<string, int> PlayCount
        {
            get { return _playCount; }
        }

        public void Unsubscribe()
        {
            _audioPlayer.TrackEnded -= AudioPlayer_TrackEnded;
        }
    }
}