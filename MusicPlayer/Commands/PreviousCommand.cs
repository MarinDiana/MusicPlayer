using MusicPlayer.Controllers;

namespace MusicPlayer.Commands
{
    public class PreviousCommand : IPlayerCommand
    {
        private readonly PlaybackController _controller;

        public PreviousCommand(PlaybackController controller)
        {
            _controller = controller;
        }

        public string Description
        {
            get { return "Previous"; }
        }

        public bool CanUndo
        {
            get { return false; }
        }

        public void Execute()
        {
            _controller.Previous();
        }

        public void Undo()
        {
        }
    }
}