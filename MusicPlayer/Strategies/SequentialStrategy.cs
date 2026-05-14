using MusicPlayer.Models;

namespace MusicPlayer.Strategies
{
    public class SequentialStrategy : IPlaybackStrategy
    {
        public string Name
        {
            get { return "Sequential"; }
        }

        public Track? GetNextTrack(
            Playlist playlist,
            Track? currentTrack)
        {
            if (playlist.Count == 0)
                return null;

            if (currentTrack == null)
                return playlist[0];

            int index =
                playlist.Tracks.IndexOf(currentTrack);

            if (index < 0)
                return playlist[0];

            if (index + 1 >= playlist.Count)
                return null;

            return playlist[index + 1];
        }

        public Track? GetPreviousTrack(
            Playlist playlist,
            Track? currentTrack)
        {
            if (playlist.Count == 0)
                return null;

            if (currentTrack == null)
                return playlist[0];

            int index =
                playlist.Tracks.IndexOf(currentTrack);

            if (index <= 0)
                return null;

            return playlist[index - 1];
        }

        public void Reset(Playlist playlist)
        {
        }
    }
}