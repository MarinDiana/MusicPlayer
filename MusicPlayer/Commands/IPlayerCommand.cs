namespace MusicPlayer.Commands
{
    public interface IPlayerCommand
    {
        string Description { get; }

        bool CanUndo { get; }

        void Execute();

        void Undo();
    }
}