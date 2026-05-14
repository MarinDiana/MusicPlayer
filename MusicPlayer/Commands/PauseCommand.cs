using MusicPlayer.Controllers;

namespace MusicPlayer.Commands
{
    public class PauseCommand : IPlayerCommand
    {
        private readonly PlaybackController _controller;

        public PauseCommand(PlaybackController controller)
        {
            _controller = controller;
        }

        public string Description
        {
            get { return "Pause"; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public void Execute()
        {
            _controller.Pause();
        }

        public void Undo()
        {
            _controller.Play();
        }
    }
}