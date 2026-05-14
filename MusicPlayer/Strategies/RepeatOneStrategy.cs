using MusicPlayer.Models;

namespace MusicPlayer.Strategies
{
    public class RepeatOneStrategy : IPlaybackStrategy
    {
        public string Name
        {
            get { return "Repeat One"; }
        }

        public Track? GetNextTrack(
            Playlist playlist,
            Track? currentTrack)
        {
            return currentTrack;
        }

        public Track? GetPreviousTrack(
            Playlist playlist,
            Track? currentTrack)
        {
            return currentTrack;
        }

        public void Reset(Playlist playlist)
        {
        }
    }
}