using MusicPlayer.Models;

namespace MusicPlayer.Commands
{
    public class MoveTrackCommand : IPlayerCommand
    {
        private readonly Playlist _playlist;

        private readonly int _oldIndex;

        private readonly int _newIndex;

        public MoveTrackCommand(
            Playlist playlist,
            int oldIndex,
            int newIndex)
        {
            _playlist = playlist;

            _oldIndex = oldIndex;

            _newIndex = newIndex;
        }

        public string Description
        {
            get { return "Move Track"; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public void Execute()
        {
            _playlist.Move(
                _oldIndex,
                _newIndex);
        }

        public void Undo()
        {
            _playlist.Move(
                _newIndex,
                _oldIndex);
        }
    }
}