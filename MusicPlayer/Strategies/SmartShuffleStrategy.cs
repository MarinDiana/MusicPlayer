using MusicPlayer.Models;

namespace MusicPlayer.Strategies
{
    public class SmartShuffleStrategy : IPlaybackStrategy
    {
        private readonly Random _random =
            new Random();

        private readonly Queue<Track> _history =
            new Queue<Track>();

        public string Name
        {
            get { return "Smart Shuffle"; }
        }

        public Track? GetNextTrack(
            Playlist playlist,
            Track? currentTrack)
        {
            if (playlist.Count == 0)
                return null;

            List<Track> availableTracks =
                playlist.Tracks
                    .Where(t => !_history.Contains(t))
                    .ToList();

            if (availableTracks.Count == 0)
            {
                _history.Clear();

                availableTracks =
                    playlist.Tracks.ToList();
            }

            Track selectedTrack =
                availableTracks[
                    _random.Next(availableTracks.Count)];

            _history.Enqueue(selectedTrack);

            while (_history.Count > 5)
            {
                _history.Dequeue();
            }

            return selectedTrack;
        }

        public Track? GetPreviousTrack(
            Playlist playlist,
            Track? currentTrack)
        {
            return currentTrack;
        }

        public void Reset(Playlist playlist)
        {
            _history.Clear();
        }
    }
}