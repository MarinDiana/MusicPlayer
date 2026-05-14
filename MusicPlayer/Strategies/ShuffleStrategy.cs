using MusicPlayer.Models;

namespace MusicPlayer.Strategies
{
    public class ShuffleStrategy : IPlaybackStrategy
    {
        private readonly Random _random =
            new Random();

        private List<Track> _shuffledTracks =
            new List<Track>();

        private int _currentIndex = -1;

        public string Name
        {
            get { return "Shuffle"; }
        }

        public Track? GetNextTrack(
            Playlist playlist,
            Track? currentTrack)
        {
            if (playlist.Count == 0)
                return null;

            if (_shuffledTracks.Count == 0)
            {
                Reset(playlist);
            }

            _currentIndex++;

            if (_currentIndex >= _shuffledTracks.Count)
            {
                return null;
            }

            return _shuffledTracks[_currentIndex];
        }

        public Track? GetPreviousTrack(
            Playlist playlist,
            Track? currentTrack)
        {
            if (_shuffledTracks.Count == 0)
                return null;

            _currentIndex--;

            if (_currentIndex < 0)
            {
                _currentIndex = 0;
            }

            return _shuffledTracks[_currentIndex];
        }

        public void Reset(Playlist playlist)
        {
            _shuffledTracks =
                playlist.Tracks
                    .OrderBy(x => _random.Next())
                    .ToList();

            _currentIndex = -1;
        }
    }
}