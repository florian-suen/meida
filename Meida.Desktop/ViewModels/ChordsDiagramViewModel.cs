using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Meida.Desktop.ViewModels;

public class FingerPosition : ObservableObject
{
    public int StringNumber { get; set; } // 1 to 6 (1 = high E, 6 = low E)
    public int FretNumber { get; set; } // 0 = open, 1+ = fret number
    public int FingerNumber { get; set; } // 1=Index, 2=Middle, 3=Ring, 4=Pinky
}

public class ChordsDiagramViewModel : ObservableObject
{
    public string ChordName { get; set; } = "C Major";
    public ObservableCollection<FingerPosition> Fingers { get; set; } = new();
}
