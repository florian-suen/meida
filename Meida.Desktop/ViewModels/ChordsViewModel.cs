using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Meida.Desktop.ViewModels;

public partial class ChordsViewModel : ViewModelBase
{
    [ObservableProperty]
    private double _topPanelProportion = 0.15;

    public PlayerViewModel PlayerVM { get; } = new();

    [RelayCommand]
    private void ToggleTopPanel()
    {
        TopPanelProportion = TopPanelProportion >= 0.4 ? 0.15 : 0.5;
    }
}
