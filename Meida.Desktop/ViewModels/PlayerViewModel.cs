using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sortable.Avalonia;

namespace Meida.Desktop.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isPlaying;
    [ObservableProperty] private bool _isRepeating;
    [ObservableProperty] private double _playheadX;
    [ObservableProperty] private double _boardWidth = 800;
    [ObservableProperty] private int _currentChordIndex = -1;
    private CancellationTokenSource? _cts;
    public ObservableCollection<FingerPosition> AllTabNotes { get; } = new();
    private const double PixelsPerChord = 200;
    private const double BeatDurationMs = 1000;
    public ObservableCollection<ChordsDiagramViewModel> SelectedChordsOrder { get; set; }
    [ObservableProperty] private string _selectedItem;
    public AvaloniaList<string> BackgroundMusic { get; } =
        ["G Major", "A Major", "B Major", "C Major"];


    public PlayerViewModel()
    {
        SelectedChordsOrder = new ObservableCollection<ChordsDiagramViewModel>();
        SelectedChordsOrder.CollectionChanged += OnChordsChanged;
    }

    private void OnChordsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RebuildTabBoard();
    }

    private void RebuildTabBoard()
    {
        AllTabNotes.Clear();
        const double stringSpacing = 25;
        const double topMargin = 20;

        // Calculate total board width based on how many chords we have
        BoardWidth = Math.Max(800, SelectedChordsOrder.Count * PixelsPerChord + 200);

        for (var i = 0; i < SelectedChordsOrder.Count; i++)
        {
            var chord = SelectedChordsOrder[i];

            // The X position is the chord's index * width, plus 100 to center it in the block
            var noteX = i * PixelsPerChord + 100;

            foreach (var finger in chord.Fingers)
            {
                // Calculate Y: String 1 is top, String 6 is bottom
                var y = topMargin + (finger.StringNumber - 1) * stringSpacing;

                finger.TimelineX = noteX;
                finger.TabY = y;

                AllTabNotes.Add(finger);
            }
        }
    }
    
    public IRelayCommand<ChordsDiagramViewModel>? SelectChordCommand { get; set; }


    [RelayCommand]
    private async Task Play()
    {
        if (IsPlaying)
        {
            Stop();
            return;
        }

        if (SelectedChordsOrder.Count == 0) return;

        IsPlaying = true;
        _cts = new CancellationTokenSource();
        PlayheadX = 0; // Start at the very beginning of the board

        try
        {
            while (IsPlaying && !_cts.IsCancellationRequested)
            {
                // Loop through each chord block
                for (var i = 0; i < SelectedChordsOrder.Count; i++)
                {
                    if (_cts.IsCancellationRequested || !IsPlaying) break;

                    CurrentChordIndex = i;
                    // TODO: PlayChordSound(...)

                    // ✅ Animate across exactly ONE chord block (200px)
                    await AnimatePlayhead(PixelsPerChord, _cts.Token);
                }

                if (!IsRepeating)
                {
                    Stop();
                    break;
                }

                PlayheadX = 0;
                await Task.Delay(500, _cts.Token);
            }
        }
        catch (TaskCanceledException)
        {
        }
        finally
        {
            Stop();
        }
    }


    [RelayCommand]
    private void Stop()
    {
        _cts?.Cancel();
        IsPlaying = false;
        CurrentChordIndex = -1;
        PlayheadX = 0;
    }

    private async Task AnimatePlayhead(double distanceToMove, CancellationToken token)
    {
        const int frames = 60;
        var step = distanceToMove / frames;
        var frameDelay = BeatDurationMs / frames;

        for (var f = 0; f < frames; f++)
        {
            if (token.IsCancellationRequested) return;
            PlayheadX += step;
            await Task.Delay((int)frameDelay, token);
        }
    }

    private void PlayChordSound(string chordName)
    {
        Console.WriteLine("PLAY");
    }
    

 
    [RelayCommand]
    private void UpdateChordOrder(SortableUpdateEventArgs e)
    {
        e.ApplyUpdateMutation();
    }
    
    
    
    
}
