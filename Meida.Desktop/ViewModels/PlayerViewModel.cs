using System.Collections.ObjectModel;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sortable.Avalonia;

namespace Meida.Desktop.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{
    public ObservableCollection<ChordsDiagramViewModel> SelectedChordsOrder { get; set; }

    [ObservableProperty]
    private string _selectedItem;

    public AvaloniaList<string> BackgroundMusic { get; } =
    ["Custom", "C Major", "D Major", "E Major"];

    public IRelayCommand<ChordsDiagramViewModel>? SelectChordCommand { get; set; }


    [RelayCommand]
    private void UpdateChordOrder(SortableUpdateEventArgs e)
    {
        e.ApplyUpdateMutation();
    }
    
    
    
    
}
