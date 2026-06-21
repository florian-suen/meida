using System.Collections.ObjectModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Meida.Desktop.ViewModels;

public class FingerPosition : ObservableObject
{
    private const double MarkerSize = 18;

    public int StringNumber { get; set; }
    public int FretNumber { get; set; }
    public int FingerNumber { get; set; }

  
    public double XPosition => 111 - (StringNumber - 1) * 20;
    public double YPosition => 33 + (FretNumber - 1) * 25;

 
    public Thickness PositionMargin =>
        new(XPosition - MarkerSize / 2, YPosition - MarkerSize / 2, 0, 0);
}

public partial class ChordsDiagramViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BaseFretLabel))]
    private int _baseFret = 1;

    [ObservableProperty]
    private string _chordName = "C Major";

    public string BaseFretLabel => BaseFret > 1 ? $"{BaseFret}fr" : string.Empty;

    public ObservableCollection<FingerPosition> Fingers { get; set; } = new();
}


 