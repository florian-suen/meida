using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Meida.Desktop.ViewModels;

public partial class FingerPosition : ObservableObject
{
    [ObservableProperty] private int _fingerNumber;
    [ObservableProperty] private int _stringNumber;
    [ObservableProperty] private int _fretNumber;


    [ObservableProperty] private double _timelineX;
    [ObservableProperty] private double _tabY;    


   
 


   
}

public partial class ChordsDiagramViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BaseFretLabel))]
    private int _baseFret = 1;

    [ObservableProperty]
    private string _chordName = "C Major";

    [ObservableProperty] private bool _isSelected;
    public string BaseFretLabel => BaseFret > 1 ? $"{BaseFret}fr" : string.Empty;

    public ObservableCollection<FingerPosition> Fingers { get; set; } = new();

    public IRelayCommand<ChordsDiagramViewModel>? SelectChordCommand { get; set; }


    [ObservableProperty] private bool _isCurrent;



}


 