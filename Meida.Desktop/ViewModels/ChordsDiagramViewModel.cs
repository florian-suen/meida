using System.Collections.ObjectModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Meida.Desktop.ViewModels;

public class FingerPosition : ObservableObject
{
    public int StringNumber { get; set; }
    public int FretNumber { get; set; }
    public int FingerNumber { get; set; }

    public double XPosition => 10 + (StringNumber - 1) * 20;
    public double YPosition => 20 + (FretNumber - 1) * 25;

    public Thickness PositionMargin => new(XPosition, YPosition, 0, 0);
}

public partial class ChordsDiagramViewModel : ObservableObject
{
    [ObservableProperty]
    private string _chordName = "C Major";

    public ObservableCollection<FingerPosition> Fingers { get; } = new();
}
