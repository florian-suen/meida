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
 

    
    private SukiSideMenuItem _selectedPage;
    private bool _option1Enabled;

    private double _option2Value = 50;

    public MainWindowViewModel()
    {
        MenuItems.Add(
            new MenuItemViewModel
            {
                Header = "Home",
                Icon = MaterialIconKind.Home,
                PageContent = new HomeView(),
            }
        );
        MenuItems.Add(
            new MenuItemViewModel
            {
                Header = "Chords",
                Icon = MaterialIconKind.MusicNote,
                PageContent = new ChordsView(),
            }
        );
        MenuItems.Add(
            new MenuItemViewModel
            {
                Header = "Scales",
                Icon = MaterialIconKind.MusicAccidentalFlat,
                PageContent = new ScalesView(),
            }
        );
        MenuItems.Add(
            new MenuItemViewModel
            {
                Header = "Videos",
                Icon = MaterialIconKind.VideoBox,
                PageContent = new VideosView(),
            }
        );
        MenuItems.Add(
            new MenuItemViewModel
            {
                Header = "Songs",
                Icon = MaterialIconKind.BoxMusic,
                PageContent = new SongsView(),
            }
        );
        MenuItems.Add(
            new MenuItemViewModel
            {
                Header = "Settings",
                Icon = MaterialIconKind.Settings,
                PageContent = new SettingsView(),
            }
        );
        _selectedPage = MenuItems[0];
        
     
    }

    public ObservableCollection<MenuItemViewModel> MenuItems { get; set; } = [];

    
    public SukiSideMenuItem SelectedMenuItem
    {
        get => _selectedPage;
        set
        {
            if (field == value)
                return;
            field = value;
            OnPropertyChanged();
        }
    }

 

    public event PropertyChangedEventHandler? PropertyChanged;

    private new void OnPropertyChanged([CallerMemberName] string propertyName = null)
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