# MusicPlayer

Laborator MAP – Music Player WPF

## Descriere

Aplicatia implementeaza un music player desktop folosind:

- WPF
- MVVM
- NAudio
- Pattern-uri comportamentale

Aplicatia permite:
- redare audio MP3/WAV
- playlist
- schimbare strategie playback
- undo/redo pentru comenzi
- logging si statistics
- history UI

---

# Pattern-uri implementate

## Strategy

Strategiile de playback sunt:

- SequentialStrategy
- ShuffleStrategy
- SmartShuffleStrategy
- RepeatOneStrategy

Acestea implementeaza:

```text
IPlaybackStrategy

si pot fi schimbate runtime fara modificarea playerului.

Command

Comenzile aplicatiei implementeaza:

IPlayerCommand

Comenzi implementate:

PlayCommand
PauseCommand
NextCommand
PreviousCommand
AddTrackCommand
RemoveTrackCommand
MoveTrackCommand
ClearPlaylistCommand
ChangeStrategyCommand

Undo/Redo este implementat prin:

CommandHistory
Observer

Observer este folosit prin:

INotifyPropertyChanged
ObservableCollection
event-uri TrackEnded

Observeri implementati:

PlaybackLogger
StatisticsTracker
AutoNextHandler
Arhitectura
Functionalitati
Add Files MP3/WAV
Play
Pause
Next
Previous
Undo
Redo
schimbare strategie playback
logging
statistics
history UI
Tehnologii utilizate
C#
WPF
.NET 10
NAudio
MVVM
Rulare
Deschide solutia in Visual Studio
Restore NuGet Packages
Ruleaza proiectul
Apasa Add Files
Selecteaza fisiere MP3/WAV
Samples

Folderul:

samples/

contine fisiere audio pentru testare.
