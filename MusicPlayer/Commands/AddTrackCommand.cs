using MusicPlayer.Models;

namespace MusicPlayer.Commands
{
    public class AddTrackCommand : IPlayerCommand
    {
        private readonly Playlist _playlist;
        private readonly Track _track;

        public AddTrackCommand(Playlist playlist, Track track)
        {
            _playlist = playlist;
            _track = track;
        }

        public string Description
        {
            get { return "Add Track"; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public void Execute()
        {
            _playlist.Add(_track);
        }

        public void Undo()
        {
            _playlist.Remove(_track);
        }
    }
}