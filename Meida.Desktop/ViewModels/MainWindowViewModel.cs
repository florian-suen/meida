using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using Material.Icons.Avalonia;
using Meida.Desktop.Services;
using Meida.Desktop.Views;
using Meida.Desktop.Views.Chords;
using SukiUI.Controls;
using SukiUI.Dialogs;

namespace Meida.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MetronomeViewModel Metronome { get; }

    public MainWindowViewModel()
    {
        Metronome = new MetronomeViewModel(new MetronomeService()); 
        OpenMetronomeCommand = new RelayCommand(ExecuteOpenMetronome);
        NavigateCommand = new RelayCommand(parameter =>
        {
            if (parameter is not string targetHeader) return;
            var currentItem = MenuItems.FirstOrDefault(m => m.IsSelected);
            currentItem?.IsSelected = false;

            var targetItem = MenuItems.FirstOrDefault(m => (string)m.Header! == targetHeader);
            targetItem?.IsSelected = true;
        });
    }

    public ISukiDialogManager DialogManager { get; } = new SukiDialogManager();
    public ICommand OpenMetronomeCommand { get; }

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
            PageContent = new HomeView()
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
            PageContent = new ChordsView { DataContext = new ChordsViewModel() },
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
            PageContent = new ScalesView { DataContext = new ScalesViewModel() },
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
    [ObservableProperty] private bool _showHeaderBeats = true;

    public MetronomeService MetronomeService { get; } = new();

    private async void ExecuteOpenMetronome(object obj)
    {
        ShowHeaderBeats = false;

        await DialogManager
            .CreateDialog()
            .WithTitle("Metronome")
            .WithContent(new MetronomeView())
            .WithViewModel(dialog => Metronome)
            .Dismiss()
            .ByClickingBackground()
            .TryShowAsync();


        ShowHeaderBeats = true; 
        
    }

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
