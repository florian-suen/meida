using System.ComponentModel;
using System.Linq;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Meida.Desktop.Services;
using SukiUI.Dialogs;

namespace Meida.Desktop.ViewModels;

public partial class BeatItem : ObservableObject
{
    public int Index { get; }
    [ObservableProperty] private bool _isActive;
    [ObservableProperty] private bool _isAccent;
    [ObservableProperty] private bool _isVisible = true;

    public BeatItem(int index)
    {
        Index = index;
    }
}

public partial class MetronomeViewModel : ViewModelBase
{
    private readonly MetronomeService _service;
    [ObservableProperty] private TimingOption[] _timing;
    [ObservableProperty] private int _currentBeat;
    [ObservableProperty] private string _playIconKind;
    private int _lastBeatIndex;

    public int[] BeatIndices => Enumerable.Range(0, _service.BeatsPerMeasure).ToArray();
    public BeatItem[] Beats { get; } = Enumerable.Range(0, 6).Select(i => new BeatItem(i)).ToArray();

    public MetronomeViewModel(ISukiDialog dialog, MetronomeService service)
    {
        Timing = service.Timing;
        _service = service;
        _currentBeat = service.CurrentBeat;
        PlayIconKind = service.PlayIconKind;
        _service.PropertyChanged += ServiceOnPropertyChanged;

        _service.BeatChanged += OnBeatChanged;
        UpdateBeatsVisuals(0);
    }

    private void OnBeatChanged(int beatIndex)
    {
        _lastBeatIndex = beatIndex;

        // Safely update the UI on the UI thread
        Dispatcher.UIThread.Post(() => { UpdateBeatsVisuals(_lastBeatIndex); });
    }


    private void ServiceOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MetronomeService.PlayIconKind)) PlayIconKind = _service.PlayIconKind;
        if (e.PropertyName == nameof(MetronomeService.IsPlaying)) OnPropertyChanged(nameof(IsPlaying));
        if (e.PropertyName == nameof(MetronomeService.CurrentBeat)) OnPropertyChanged(nameof(CurrentBeat));
        if (e.PropertyName == nameof(MetronomeService.Accent)) OnPropertyChanged(nameof(Accent));


        if (e.PropertyName == nameof(MetronomeService.BeatsPerMeasure) ||
            e.PropertyName == nameof(MetronomeService.Accent))
            Dispatcher.UIThread.Post(() => { UpdateBeatsVisuals(_lastBeatIndex); });


        if (e.PropertyName == nameof(MetronomeService.CurrentBeat)) OnPropertyChanged(nameof(BeatIndices));
        if (e.PropertyName == nameof(MetronomeService.IsPlaying)) OnPropertyChanged(nameof(IsPlaying));
    }

    private void UpdateBeatsVisuals(int currentBeat)
    {
        var maxBeats = _service.BeatsPerMeasure;
        var accent = _service.Accent;

        for (var i = 0; i < Beats.Length; i++)
        {
            Beats[i].IsActive = i == currentBeat;
            Beats[i].IsAccent = i == 0 && accent;
            Beats[i].IsVisible = i < maxBeats;
        }
    }

    public TimingOption SelectedTiming
    {
        get => _service.SelectedTiming;
        set
        {
            _service.SelectedTiming = value;
            OnPropertyChanged();
        }
    }


    public bool IsPlaying
    {
        get => _service.IsPlaying;
        set
        {
            _service.IsPlaying = value;
            OnPropertyChanged();
        }
    }

    public int Bpm
    {
        get => _service.Bpm;
        set
        {
            _service.Bpm = value;
            OnPropertyChanged();
        }
    }

    public bool Accent
    {
        get => _service.Accent;
        set
        {
            _service.Accent = value;
            OnPropertyChanged();
        }
    }


    [RelayCommand]
    private void TogglePlay()
    {
        _service.TogglePlay();

        OnPropertyChanged(nameof(IsPlaying));
    }
}