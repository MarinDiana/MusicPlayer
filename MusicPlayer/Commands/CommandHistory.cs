using System.Collections.ObjectModel;

namespace MusicPlayer.Commands
{
    public class CommandHistory
    {
        private readonly Stack<IPlayerCommand> _undoStack =
            new Stack<IPlayerCommand>();

        private readonly Stack<IPlayerCommand> _redoStack =
            new Stack<IPlayerCommand>();

        private const int MaxHistoryCount = 50;

        public ObservableCollection<string> History { get; } =
            new ObservableCollection<string>();

        public bool CanUndo
        {
            get { return _undoStack.Count > 0; }
        }

        public bool CanRedo
        {
            get { return _redoStack.Count > 0; }
        }

        public void Execute(IPlayerCommand command)
        {
            command.Execute();

            AddToHistory(command.Description);

            _redoStack.Clear();

            if (command.CanUndo)
            {
                _undoStack.Push(command);
            }
        }

        public void Undo()
        {
            if (!CanUndo)
                return;

            IPlayerCommand command =
                _undoStack.Pop();

            command.Undo();

            _redoStack.Push(command);

            AddToHistory("Undo: " + command.Description);
        }

        public void Redo()
        {
            if (!CanRedo)
                return;

            IPlayerCommand command =
                _redoStack.Pop();

            command.Execute();

            _undoStack.Push(command);

            AddToHistory("Redo: " + command.Description);
        }

        private void AddToHistory(string description)
        {
            History.Add(description);

            while (History.Count > MaxHistoryCount)
            {
                History.RemoveAt(0);
            }
        }
    }
}