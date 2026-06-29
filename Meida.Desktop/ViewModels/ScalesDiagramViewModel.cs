using System.Collections.ObjectModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Meida.Desktop.ViewModels;

public class ScaleFingerPosition : ObservableObject
{
    private const double MarkerSize = 18;
    private string _note;
    public bool IsRoot = false;
    public int StringNumber { get; set; }
    public int FretNumber { get; set; }

    public string Note
    {
        get => _note;
        set
        {
            if (_note == value)
                return;
            _note = value;
            OnPropertyChanged();
        }
    }

    public double XPosition => 15.7 + (FretNumber - 1) * 30;
    public double YPosition => 0.5 + (StringNumber - 1) * 26;

    public Thickness PositionMargin =>
        new(XPosition - MarkerSize / 2, YPosition - MarkerSize / 2, 0, 0);
}

public partial class ScalesDiagramViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BaseFretLabel))]
    private int _baseFret = 1;

    [ObservableProperty]
    private string _positionName = "Position 1";

    public string BaseFretLabel => BaseFret > 1 ? $"{BaseFret}fr" : string.Empty;

    public ObservableCollection<ScaleFingerPosition> Fingers { get; set; } = new();
}
