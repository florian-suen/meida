using System.Collections.ObjectModel;
using Avalonia.Collections;

namespace Meida.Desktop.ViewModels;

public class ScalesViewModel : ViewModelBase
{
    private static readonly string[] ChromaticNotes =
    {
        "C",
        "C#",
        "D",
        "D#",
        "E",
        "F",
        "F#",
        "G",
        "G#",
        "A",
        "A#",
        "B",
    };

    private static readonly int[] OpenStringPitchClasses = { 0, 4, 11, 7, 2, 9, 4 };
    private static readonly int[] PositionOffsets = { 0, 2, 4, 7, 9 };

    private ScaleKey _selectedKey;

    public ScalesViewModel()
    {
        MajPentatonic = new ObservableCollection<ScalesDiagramViewModel>
        {
            CreateDiagram("1"),
            CreateDiagram("2"),
            CreateDiagram("3"),
            CreateDiagram("4"),
            CreateDiagram("5"),
        };

        _selectedKey = ScaleKey[4];
        UpdateMajPentatonic();
    }

    public ScaleKey SelectedKey
    {
        get => _selectedKey;
        set
        {
            if (_selectedKey == value)
                return;
            _selectedKey = value;
            OnPropertyChanged();
            UpdateMajPentatonic();
        }
    }

    public ObservableCollection<ScalesDiagramViewModel> MajPentatonic { get; set; }

    public AvaloniaList<ScaleKey> ScaleKey { get; set; } =
    [
        new() { Key = "G#", StartFretNumber = 1 },
        new() { Key = "A", StartFretNumber = 2 },
        new() { Key = "A#", StartFretNumber = 3 },
        new() { Key = "B", StartFretNumber = 4 },
        new() { Key = "C", StartFretNumber = 5 },
        new() { Key = "C#", StartFretNumber = 6 },
        new() { Key = "D", StartFretNumber = 7 },
        new() { Key = "D#", StartFretNumber = 8 },
        new() { Key = "E", StartFretNumber = 9 },
        new() { Key = "F", StartFretNumber = 10 },
        new() { Key = "F#", StartFretNumber = 11 },
        new() { Key = "G", StartFretNumber = 12 },
        new() { Key = "G# (High)", StartFretNumber = 13 },
        new() { Key = "A (High)", StartFretNumber = 14 },
        new() { Key = "A# (High", StartFretNumber = 15 },
    ];

    private ScalesDiagramViewModel CreateDiagram(string positionName)
    {
        return positionName switch
        {
            "1" => new ScalesDiagramViewModel
            {
                PositionName = $"Position {positionName}",
                Fingers =
                [
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 3 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 3 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 3 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 4 },
                ],
            },
            "2" => new ScalesDiagramViewModel
            {
                PositionName = $"Position {positionName}",
                Fingers =
                [
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 3 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 4 },
                ],
            },
            "3" => new ScalesDiagramViewModel
            {
                PositionName = $"Position {positionName}",
                Fingers =
                [
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 5 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 4 },
                ],
            },
            "4" => new ScalesDiagramViewModel
            {
                PositionName = $"Position {positionName}",
                Fingers =
                [
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 3 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 3 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 4 },
                ],
            },
            "5" => new ScalesDiagramViewModel
            {
                PositionName = $"Position {positionName}",
                Fingers =
                [
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 1, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 2, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 3, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 1 },
                    new ScaleFingerPosition { StringNumber = 4, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 5, FretNumber = 4 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 2 },
                    new ScaleFingerPosition { StringNumber = 6, FretNumber = 4 },
                ],
            },
        };
    }

    private void UpdateMajPentatonic()
    {
        if (MajPentatonic == null || SelectedKey == null)
            return;

        var start = SelectedKey.StartFretNumber;

        for (var i = 0; i < MajPentatonic.Count; i++)
        {
            var diagram = MajPentatonic[i];
            var diagramBaseFret = start + PositionOffsets[i];
            diagram.BaseFret = diagramBaseFret;

            foreach (var finger in diagram.Fingers)
            {
                var absoluteFret = diagramBaseFret + finger.FretNumber - 1;
                var openStringIdx = OpenStringPitchClasses[finger.StringNumber];
                var noteIndex = (openStringIdx + absoluteFret) % 12;
                finger.Note = ChromaticNotes[noteIndex];
            }
        }
    }
}

public class ScaleKey
{
    public string Key { get; set; }
    public int StartFretNumber { get; set; }
}
