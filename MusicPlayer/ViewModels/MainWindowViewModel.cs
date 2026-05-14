using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MusicPlayer.Audio;
using MusicPlayer.Commands;
using MusicPlayer.Controllers;
using MusicPlayer.Models;
using MusicPlayer.Observers;
using MusicPlayer.Strategies;
using Microsoft.Win32;
using System.IO;

namespace MusicPlayer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly AudioPlayer _audioPlayer;
        private readonly Playlist _playlist;
        private readonly PlaybackController _controller;
        private readonly CommandHistory _history;

        private readonly PlaybackLogger _logger;
        private readonly StatisticsTracker _statisticsTracker;
        private readonly AutoNextHandler _autoNextHandler;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Track> Tracks
        {
            get { return _playlist.Tracks; }
        }

        public ObservableCollection<string> History
        {
            get { return _history.History; }
        }

        private Track? _selectedTrack;

        public Track? SelectedTrack
        {
            get { return _selectedTrack; }
            set
            {
                _selectedTrack = value;

                if (_selectedTrack != null)
                {
                    _controller.Load(_selectedTrack);
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentTrack));
            }
        }

        public Track? CurrentTrack
        {
            get { return _audioPlayer.CurrentTrack; }
        }

        public PlayerState State
        {
            get { return _audioPlayer.State; }
        }

        public TimeSpan Position
        {
            get { return _audioPlayer.Position; }
        }

        public TimeSpan Duration
        {
            get { return _audioPlayer.Duration; }
        }

        public string StrategyName
        {
            get { return _controller.Strategy.Name; }
        }

        public bool CanUndo
        {
            get { return _history.CanUndo; }
        }

        public bool CanRedo
        {
            get { return _history.CanRedo; }
        }

        public RelayCommand PlayCommand { get; }
        public RelayCommand PauseCommand { get; }
        public RelayCommand NextCommand { get; }
        public RelayCommand PreviousCommand { get; }
        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }

        public RelayCommand SequentialCommand { get; }
        public RelayCommand ShuffleCommand { get; }
        public RelayCommand SmartShuffleCommand { get; }
        public RelayCommand RepeatOneCommand { get; }
        public RelayCommand AddFilesCommand { get; }


        public MainWindowViewModel()
        {
            _audioPlayer = new AudioPlayer();
            _playlist = new Playlist();
            _history = new CommandHistory();

            _controller = new PlaybackController(
                _audioPlayer,
                _playlist,
                new SequentialStrategy());

            _logger = new PlaybackLogger(_audioPlayer);
            _statisticsTracker = new StatisticsTracker(_audioPlayer);
            _autoNextHandler = new AutoNextHandler(_audioPlayer, _controller);

            _audioPlayer.PropertyChanged += AudioPlayer_PropertyChanged;

            AddDemoTracks();

            PlayCommand = new RelayCommand(
                () => _history.Execute(new PlayCommand(_controller)));

            PauseCommand = new RelayCommand(
                () => _history.Execute(new PauseCommand(_controller)));

            NextCommand = new RelayCommand(
                () => _history.Execute(new NextCommand(_controller)));

            PreviousCommand = new RelayCommand(
                () => _history.Execute(new PreviousCommand(_controller)));

            UndoCommand = new RelayCommand(
                () =>
                {
                    _history.Undo();
                    RefreshCommandState();
                },
                () => _history.CanUndo);

            RedoCommand = new RelayCommand(
                () =>
                {
                    _history.Redo();
                    RefreshCommandState();
                },
                () => _history.CanRedo);

            SequentialCommand = new RelayCommand(
                () => ChangeStrategy(new SequentialStrategy()));

            ShuffleCommand = new RelayCommand(
                () => ChangeStrategy(new ShuffleStrategy()));

            SmartShuffleCommand = new RelayCommand(
                () => ChangeStrategy(new SmartShuffleStrategy()));

            RepeatOneCommand = new RelayCommand(
                () => ChangeStrategy(new RepeatOneStrategy()));
            AddFilesCommand = new RelayCommand(AddFiles);
        }

        private void AddDemoTracks()
        {
            _playlist.Add(new Track(
                Guid.NewGuid(),
                "Imagine",
                "John Lennon",
                "Imagine",
                TimeSpan.FromMinutes(3),
                ""));

            _playlist.Add(new Track(
                Guid.NewGuid(),
                "Hey Jude",
                "The Beatles",
                "The Beatles 1967-1970",
                TimeSpan.FromMinutes(7),
                ""));

            _playlist.Add(new Track(
                Guid.NewGuid(),
                "Yesterday",
                "The Beatles",
                "Help!",
                TimeSpan.FromMinutes(2),
                ""));
        }

        private void AddFiles()
        {
            OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Filter =
                "Audio Files|*.mp3;*.wav";

            dialog.Multiselect = true;

            bool? result = dialog.ShowDialog();

            if (result != true)
                return;

            foreach (string filePath in dialog.FileNames)
            {
                string title =
                    Path.GetFileNameWithoutExtension(filePath);

                Track track = new Track(
                    Guid.NewGuid(),
                    title,
                    "Unknown Artist",
                    "Unknown Album",
                    TimeSpan.Zero,
                    filePath);

                _history.Execute(
                    new AddTrackCommand(
                        _playlist,
                        track));
            }

            RefreshCommandState();
        }

        private void ChangeStrategy(IPlaybackStrategy strategy)
        {
            _history.Execute(
                new ChangeStrategyCommand(
                    _controller,
                    strategy));

            OnPropertyChanged(nameof(StrategyName));
            RefreshCommandState();
        }

        private void AudioPlayer_PropertyChanged(
            object? sender,
            PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);

            if (e.PropertyName == nameof(AudioPlayer.CurrentTrack))
            {
                OnPropertyChanged(nameof(CurrentTrack));
            }

            if (e.PropertyName == nameof(AudioPlayer.State))
            {
                OnPropertyChanged(nameof(State));
            }

            if (e.PropertyName == nameof(AudioPlayer.Position))
            {
                OnPropertyChanged(nameof(Position));
            }

            if (e.PropertyName == nameof(AudioPlayer.Duration))
            {
                OnPropertyChanged(nameof(Duration));
            }
        }

        private void RefreshCommandState()
        {
            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(CanRedo));

            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        public void DisposeObservers()
        {
            _logger.Unsubscribe();
            _statisticsTracker.Unsubscribe();
            _autoNextHandler.Unsubscribe();
            _audioPlayer.Dispose();
        }

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}