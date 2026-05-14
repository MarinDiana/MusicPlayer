using MusicPlayer.Models;

namespace MusicPlayer.Commands
{
    public class RemoveTrackCommand : IPlayerCommand
    {
        private readonly Playlist _playlist;

        private readonly Track _track;

        private int _originalIndex;

        public RemoveTrackCommand(
            Playlist playlist,
            Track track)
        {
            _playlist = playlist;

            _track = track;
        }

        public string Description
        {
            get { return "Remove Track"; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public void Execute()
        {
            _originalIndex =
                _playlist.Tracks.IndexOf(_track);

            _playlist.Remove(_track);
        }

        public void Undo()
        {
            _playlist.Tracks.Insert(
                _originalIndex,
                _track);
        }
    }
}