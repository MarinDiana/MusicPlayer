using MusicPlayer.Controllers;
using MusicPlayer.Strategies;

namespace MusicPlayer.Commands
{
    public class ChangeStrategyCommand : IPlayerCommand
    {
        private readonly PlaybackController _controller;

        private readonly IPlaybackStrategy _newStrategy;

        private IPlaybackStrategy? _oldStrategy;

        public ChangeStrategyCommand(
            PlaybackController controller,
            IPlaybackStrategy newStrategy)
        {
            _controller = controller;
            _newStrategy = newStrategy;
        }

        public string Description
        {
            get { return "Change Strategy"; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public void Execute()
        {
            _oldStrategy = _controller.Strategy;

            _controller.SetStrategy(_newStrategy);
        }

        public void Undo()
        {
            if (_oldStrategy != null)
            {
                _controller.SetStrategy(_oldStrategy);
            }
        }
    }
}