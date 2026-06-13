using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using Meida.Desktop.Views;
using SukiUI.Controls;

namespace Meida.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isChordsActive;

    [ObservableProperty]
    private bool _isScalesActive;

    [ObservableProperty]
    private bool _isSongsActive;

    [ObservableProperty]
    private bool _isVideosActive;

    private bool _option1Enabled;

    private double _option2Value = 50;

    public MainWindowViewModel()
    {
        _menuItems.Add(
            new MenuItemViewModel
            {
                Header = "Home",
                IconKind = MaterialIconKind.Home,
                PageContent = new HomeView(),
            }
        );
        _menuItems.Add(
            new MenuItemViewModel
            {
                Header = "Chords",
                IconKind = MaterialIconKind.MusicNote,
                PageContent = new ChordsView(),
            }
        );
        _menuItems.Add(
            new MenuItemViewModel
            {
                Header = "Scales",
                IconKind = MaterialIconKind.MusicAccidentalFlat,
                PageContent = new ScalesView(),
            }
        );
        _menuItems.Add(
            new MenuItemViewModel
            {
                Header = "Videos",
                IconKind = MaterialIconKind.VideoBox,
                PageContent = new VideosView(),
            }
        );
        _menuItems.Add(
            new MenuItemViewModel
            {
                Header = "Songs",
                IconKind = MaterialIconKind.BoxMusic,
                PageContent = new SongsView(),
            }
        );
        _menuItems.Add(
            new MenuItemViewModel
            {
                Header = "Settings",
                IconKind = MaterialIconKind.Settings,
                PageContent = new SettingsView(),
            }
        );

        SelectedMenuItemView = _menuItems[0];
    }

    private ObservableCollection<MenuItemViewModel> _menuItems { get; } = [];

    public MenuItemViewModel SelectedMenuItemView
    {
        get;
        set
        {
            if (field == value)
                return;
            field = value;
            OnPropertyChanged();
        }
    }

    [RelayCommand]
    private void SetActivePage(string pageName)
    {
        IsChordsActive = pageName == "Chords";
        IsScalesActive = pageName == "Scales";
        IsVideosActive = pageName == "Videos";
        IsSongsActive = pageName == "Songs";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class RelayCommand : ICommand
{
    private readonly Func<object, bool> _canExecute;
    private readonly Action<object> _execute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        _execute(parameter);
    }

    public event EventHandler CanExecuteChanged;
}

public class MenuItemViewModel : SukiSideMenuItem
{
    public string header = string.Empty;
    public MaterialIconKind IconKind;
    public ContentControl PageContent = new();

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
