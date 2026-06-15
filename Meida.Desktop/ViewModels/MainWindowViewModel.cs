using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Material.Icons;
using Material.Icons.Avalonia;
using Meida.Desktop.Views;
using SukiUI.Controls;

namespace Meida.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private bool _option1Enabled;
    private double _option2Value = 50;

    public MainWindowViewModel()
    {
        NavigateCommand = new RelayCommand(parameter =>
        {
            if (parameter is string targetHeader)
            {
                var currentItem = MenuItems.FirstOrDefault(m => m.IsSelected);
                if (currentItem != null)
                    currentItem.IsSelected = false;

                var targetItem = MenuItems.FirstOrDefault(m => m.Header == targetHeader);
                if (targetItem != null)
                    targetItem.IsSelected = true;
            }
        });
    }

    public ObservableCollection<SukiSideMenuItem> MenuItems { get; set; } =
    [
        new()
        {
            Header = "Home",
            Icon = new MaterialIcon
            {
                Kind = MaterialIconKind.Home,
                Width = 24,
                Height = 24,
            },
            PageContent = new HomeView(),
        },
        new()
        {
            Header = "Chords",
            Icon = new MaterialIcon
            {
                Kind = MaterialIconKind.MusicNote,
                Width = 24,
                Height = 24,
            },
            PageContent = new ChordsView(),
        },
        new()
        {
            Header = "Scales",
            Icon = new MaterialIcon
            {
                Kind = MaterialIconKind.MusicAccidentalFlat,
                Width = 24,
                Height = 24,
            },
            PageContent = new ScalesView(),
        },
        new()
        {
            Header = "Videos",
            Icon = new MaterialIcon
            {
                Kind = MaterialIconKind.VideoBox,
                Width = 24,
                Height = 24,
            },
            PageContent = new VideosView(),
        },
        new()
        {
            Header = "Songs",
            Icon = new MaterialIcon
            {
                Kind = MaterialIconKind.BoxMusic,
                Width = 24,
                Height = 24,
            },
            PageContent = new SongsView(),
        },
        new()
        {
            Header = "Settings",
            Icon = new MaterialIcon
            {
                Kind = MaterialIconKind.Settings,
                Width = 24,
                Height = 24,
            },
            PageContent = new SettingsView(),
        },
    ];

    public ICommand NavigateCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private new void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool RaiseAndSetIfChanged<T>(
        ref T field,
        T value,
        [CallerMemberName] string? propertyName = null
    )
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false; // No change needed

        field = value;

        OnPropertyChanged(propertyName);

        return true;
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
