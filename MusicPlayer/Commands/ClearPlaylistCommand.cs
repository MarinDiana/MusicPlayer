using MusicPlayer.Models;

namespace MusicPlayer.Commands
{
    public class ClearPlaylistCommand : IPlayerCommand
    {
        private readonly Playlist _playlist;

        private List<Track> _backupTracks =
            new List<Track>();

        public ClearPlaylistCommand(Playlist playlist)
        {
            _playlist = playlist;
        }

        public string Description
        {
            get { return "Clear Playlist"; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public void Execute()
        {
            _backupTracks =
                _playlist.Tracks.ToList();

            _playlist.Clear();
        }

        public void Undo()
        {
            _playlist.Clear();

            foreach (Track track in _backupTracks)
            {
                _playlist.Add(track);
            }
        }
    }
}