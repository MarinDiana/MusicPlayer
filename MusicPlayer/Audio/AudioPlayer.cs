using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using MusicPlayer.Models;
using NAudio.Wave;

namespace MusicPlayer.Audio
{
    public class AudioPlayer :
        INotifyPropertyChanged,
        IDisposable
    {
        private WaveOutEvent? _waveOut;
        private AudioFileReader? _reader;

        private readonly DispatcherTimer _timer;

        private Track? _currentTrack;
        private PlayerState _state;
        private TimeSpan _position;
        private TimeSpan _duration;
        private float _volume = 1.0f;

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler? TrackEnded;

        public AudioPlayer()
        {
            _timer = new DispatcherTimer();

            _timer.Interval =
                TimeSpan.FromMilliseconds(200);

            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_reader == null)
                return;

            Position = _reader.CurrentTime;

            if (Position >= Duration &&
                State == PlayerState.Playing)
            {
                TrackEnded?.Invoke(this, EventArgs.Empty);
            }
        }

        public Track? CurrentTrack
        {
            get { return _currentTrack; }
            private set
            {
                _currentTrack = value;
                OnPropertyChanged();
            }
        }

        public PlayerState State
        {
            get { return _state; }
            private set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Position
        {
            get { return _position; }
            private set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            private set
            {
                _duration = value;
                OnPropertyChanged();
            }
        }

        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;

                if (_reader != null)
                {
                    _reader.Volume = value;
                }

                OnPropertyChanged();
            }
        }

        public void Load(Track track)
        {
            DisposeAudio();

            _reader =
                new AudioFileReader(track.FilePath);

            _waveOut =
                new WaveOutEvent();

            _waveOut.Init(_reader);

            _reader.Volume = Volume;

            CurrentTrack = track;

            Duration = _reader.TotalTime;

            Position = TimeSpan.Zero;

            State = PlayerState.Stopped;
        }

        public void Play()
        {
            if (_waveOut == null)
                return;

            _waveOut.Play();

            State = PlayerState.Playing;

            _timer.Start();
        }

        public void Pause()
        {
            if (_waveOut == null)
                return;

            _waveOut.Pause();

            State = PlayerState.Paused;
        }

        public void Stop()
        {
            if (_waveOut == null)
                return;

            _waveOut.Stop();

            if (_reader != null)
            {
                _reader.CurrentTime =
                    TimeSpan.Zero;
            }

            Position = TimeSpan.Zero;

            State = PlayerState.Stopped;

            _timer.Stop();
        }

        public void Seek(TimeSpan position)
        {
            if (_reader == null)
                return;

            _reader.CurrentTime = position;

            Position = position;
        }

        private void DisposeAudio()
        {
            _waveOut?.Dispose();
            _reader?.Dispose();

            _waveOut = null;
            _reader = null;
        }

        public void Dispose()
        {
            DisposeAudio();

            _timer.Stop();
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