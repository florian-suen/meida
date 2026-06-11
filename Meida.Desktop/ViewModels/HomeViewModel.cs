using CommunityToolkit.Mvvm.Input;

namespace Meida.Desktop.ViewModels;

public partial class HomeViewModel(MainWindowViewModel mainVm) : ViewModelBase
{
    [RelayCommand]
    private void GoToChords()
    {
        mainVm.SetActivePageCommand.Execute("Chords");
    }

    [RelayCommand]
    private void GoToScales()
    {
        mainVm.SetActivePageCommand.Execute("Scales");
    }

    [RelayCommand]
    private void GoToVideos()
    {
        mainVm.SetActivePageCommand.Execute("Videos");
    }

    [RelayCommand]
    private void GoToSongs()
    {
        mainVm.SetActivePageCommand.Execute("Songs");
    }
}
