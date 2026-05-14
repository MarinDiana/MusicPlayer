using MusicPlayer.Controllers;

namespace MusicPlayer.Commands
{
    public class PlayCommand : IPlayerCommand
    {
        private readonly PlaybackController _controller;

        public PlayCommand(PlaybackController controller)
        {
            _controller = controller;
        }

        public string Description
        {
            get { return "Play"; }
        }

        public bool CanUndo
        {
            get { return false; }
        }

        public void Execute()
        {
            _controller.Play();
        }

        public void Undo()
        {
        }
    }
}