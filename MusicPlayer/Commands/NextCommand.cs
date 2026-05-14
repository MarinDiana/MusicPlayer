using MusicPlayer.Controllers;

namespace MusicPlayer.Commands
{
    public class NextCommand : IPlayerCommand
    {
        private readonly PlaybackController _controller;

        public NextCommand(PlaybackController controller)
        {
            _controller = controller;
        }

        public string Description
        {
            get { return "Next"; }
        }

        public bool CanUndo
        {
            get { return false; }
        }

        public void Execute()
        {
            _controller.Next();
        }

        public void Undo()
        {
        }
    }
}