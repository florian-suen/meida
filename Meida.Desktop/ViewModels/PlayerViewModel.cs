using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Meida.Desktop.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _selectedItem;

    public AvaloniaList<string> BackgroundMusic { get; } =
    ["Custom", "C Major", "D Major", "E Major"];
}
