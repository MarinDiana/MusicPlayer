using System.Collections.ObjectModel;

namespace MusicPlayer.Models
{
    public class Playlist
    {
        private readonly ObservableCollection<Track> _tracks =
            new ObservableCollection<Track>();

        public ObservableCollection<Track> Tracks
        {
            get { return _tracks; }
        }

        public void Add(Track track)
        {
            _tracks.Add(track);
        }

        public void Remove(Track track)
        {
            _tracks.Remove(track);
        }

        public void Move(int oldIndex, int newIndex)
        {
            _tracks.Move(oldIndex, newIndex);
        }

        public void Clear()
        {
            _tracks.Clear();
        }

        public int Count
        {
            get { return _tracks.Count; }
        }

        public Track this[int index]
        {
            get { return _tracks[index]; }
        }
    }
}