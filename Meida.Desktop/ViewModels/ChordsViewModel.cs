using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Meida.Desktop.ViewModels;

public partial class ChordsViewModel : ViewModelBase
{
    [ObservableProperty] private double _topPanelProportion = 0.15;

    public ChordsViewModel()
    {
        MajChords =
        [
            new ChordsDiagramViewModel
            {
                ChordName = "C Chord",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 1, StringNumber = 2, FretNumber = 1 },
                    new FingerPosition { FingerNumber = 2, StringNumber = 4, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 5, FretNumber = 3 }
                ]
            },
            new ChordsDiagramViewModel
            {
                ChordName = "G Chord",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 2, StringNumber = 6, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 5, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 1, FretNumber = 3 }
                ]
            },
            new ChordsDiagramViewModel
            {
                ChordName = "D Chord",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 1, StringNumber = 3, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 2, StringNumber = 1, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 2, FretNumber = 3 }
                ]
            },
            new ChordsDiagramViewModel
            {
                ChordName = "A Chord",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 1, StringNumber = 4, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 2, StringNumber = 3, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 2, FretNumber = 2 }
                ]
            },
            new ChordsDiagramViewModel
            {
                ChordName = "E Chord",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 2, StringNumber = 5, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 4, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 3, FretNumber = 1 }
                ]
            },
            new ChordsDiagramViewModel
            {
                ChordName = "F Chord",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 1, StringNumber = 6, FretNumber = 1 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 5, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 4, StringNumber = 4, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 2, StringNumber = 3, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 2, FretNumber = 1 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 1, FretNumber = 1 }
                ]
            }
        ];


        MinChords =
        [
            new ChordsDiagramViewModel
            {
                BaseFret = 3,
                ChordName = "C Minor",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 1, StringNumber = 5, FretNumber = 1 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 4, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 4, StringNumber = 3, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 2, StringNumber = 2, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 1, FretNumber = 1 }
                ]
            },
            new ChordsDiagramViewModel
            {
                BaseFret = 3,
                ChordName = "G Minor",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 1, StringNumber = 6, FretNumber = 1 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 5, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 4, StringNumber = 4, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 3, FretNumber = 1 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 2, FretNumber = 1 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 1, FretNumber = 1 }
                ]
            },
            new ChordsDiagramViewModel
            {
                ChordName = "D Minor",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 2, StringNumber = 3, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 2, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 1, FretNumber = 1 }
                ]
            },
            new ChordsDiagramViewModel
            {
                ChordName = "A Minor",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 2, StringNumber = 4, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 3, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 2, FretNumber = 1 }
                ]
            },
            new ChordsDiagramViewModel
            {
                ChordName = "E Minor",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 2, StringNumber = 5, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 4, FretNumber = 2 }
                ]
            },
            new ChordsDiagramViewModel
            {
                ChordName = "F Minor",
                Fingers =
                [
                    new FingerPosition { FingerNumber = 1, StringNumber = 6, FretNumber = 1 },
                    new FingerPosition { FingerNumber = 3, StringNumber = 5, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 4, StringNumber = 4, FretNumber = 3 },
                    new FingerPosition { FingerNumber = 2, StringNumber = 3, FretNumber = 2 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 2, FretNumber = 1 },
                    new FingerPosition { FingerNumber = 1, StringNumber = 1, FretNumber = 1 }
                ]
            }
        ];
    }

    public ObservableCollection<ChordsDiagramViewModel> MajChords { get; set; }
    public ObservableCollection<ChordsDiagramViewModel> MinChords { get; set; }
    public PlayerViewModel PlayerVM { get; } = new();

    [RelayCommand]
    private void ToggleTopPanel()
    {
        TopPanelProportion = TopPanelProportion >= 0.4 ? 0.15 : 0.5;
    }
}