using System;
using System.IO;
using System.Threading;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibVLCSharp.Shared;
using Timer = System.Timers.Timer;

namespace Meida.Desktop.Services;

public record TimingOption(string Time, string Name, int Beats);

public partial class MetronomeService : ObservableObject
{
    private static readonly LibVLC LibVlc = new();

    private static readonly string ClickPath =
        Path.Combine(AppContext.BaseDirectory, "Assets", "Sounds", "Metronome.wav");

    private static readonly string
        AccentPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Sounds", "MetronomeUp.wav");

    private readonly Media _clickMedia;
    private readonly Media _accentMedia;
    private readonly MediaPlayer _mediaPlayerInstance = new(LibVlc);
    public event Action<int>? BeatChanged;

    [ObservableProperty]
    public partial TimingOption[] Timing { get; set; } =
    {
        new("4/4", "Common", 4),
        new("3/4", "Waltz", 3),
        new("6/8", "Compound", 6)
    };

    [ObservableProperty] public partial TimingOption? SelectedTiming { get; set; }

    public string PlayIconKind => IsPlaying ? "Pause" : "PlayArrow";

    [ObservableProperty] public partial int Bpm { get; set; } = 120;

    [ObservableProperty] public partial int CurrentBeat { get; set; } = 0;

    private Timer? _metronomeTimer;
    [ObservableProperty] public partial int BeatsPerMeasure { get; set; } = 4;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PlayIconKind))]
    public partial bool IsPlaying { get; set; }

    [ObservableProperty] public partial bool Accent { get; set; } = true;

    public MetronomeService()
    {
        _clickMedia = new Media(LibVlc, ClickPath, FromType.FromPath, ":file-caching=0", ":no-video");
        _accentMedia = new Media(LibVlc, AccentPath, FromType.FromPath, ":file-caching=0", ":no-video");
        SelectedTiming = Timing[0];
        UpdateBeatsPerMeasure();
    }

    private void StartMetronome()
    {
        if (!File.Exists(ClickPath)) return;

        CurrentBeat = 0;
        UpdateBeatsPerMeasure();

        _metronomeTimer = new Timer(60.0 / Bpm * 1000);
        _metronomeTimer.Elapsed += OnTimerElapsed;
        _metronomeTimer.AutoReset = true;
        _metronomeTimer.Start();

        IsPlaying = true;
        OnTimerElapsed(null, null);
    }

    private void StopMetronome()
    {
        if (_metronomeTimer != null)
        {
            _metronomeTimer.Stop();
            _metronomeTimer.Elapsed -= OnTimerElapsed;
            _metronomeTimer.Dispose();
            _metronomeTimer = null;
        }

        _mediaPlayerInstance.Stop();
        IsPlaying = false;

        CurrentBeat = 0;
        BeatChanged?.Invoke(0);
    }

    private readonly object _timerLock = new();

    private void OnTimerElapsed(object? sender, ElapsedEventArgs? e)
    {
        if (!Monitor.TryEnter(_timerLock)) return;

        try
        {
            var beatToPlay = CurrentBeat;
            var isAccentBeat = beatToPlay == 0 && Accent;
            PlayClick(isAccentBeat);
            CurrentBeat = (beatToPlay + 1) % BeatsPerMeasure;
            BeatChanged?.Invoke(beatToPlay);
        }
        finally
        {
            Monitor.Exit(_timerLock);
        }
    }


    private void PlayClick(bool isAccent)
    {
        lock (_mediaPlayerInstance)
        {
            var mediaToPlay = isAccent && File.Exists(AccentPath) ? _accentMedia : _clickMedia;
            _mediaPlayerInstance.Stop();
            _mediaPlayerInstance.Play(mediaToPlay);
        }
    }

    partial void OnBpmChanged(int value)
    {
        if (IsPlaying && _metronomeTimer != null) _metronomeTimer.Interval = 60.0 / value * 1000;
    }

    [RelayCommand]
    public void TogglePlay()
    {
        if (IsPlaying) StopMetronome();
        else StartMetronome();
    }

    public void UpdateBeatsPerMeasure()
    {
        if (SelectedTiming != null) BeatsPerMeasure = SelectedTiming.Beats;
    }


    public void Toggle()
    {
        IsPlaying = !IsPlaying;
    }

    public void Dispose()
    {
        StopMetronome();
        _mediaPlayerInstance.Dispose();
        _clickMedia.Dispose();
        _accentMedia.Dispose();
        LibVlc.Dispose();
    }
}