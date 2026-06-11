using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Meida.Desktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isChordsActive;

    [ObservableProperty]
    private bool _isHomeActive = true;

    [ObservableProperty]
    private bool _isScalesActive;

    [ObservableProperty]
    private bool _isSettingsActive;

    private bool _isSettingsDockVisible;

    [ObservableProperty]
    private bool _isSongsActive;

    [ObservableProperty]
    private bool _isVideosActive;

    private bool _option1Enabled;

    private double _option2Value = 50;

    public MainWindowViewModel()
    {
        HomeViewModel = new HomeViewModel(this);

        ToggleSettingsCommand = new RelayCommand(_ =>
            IsSettingsDockVisible = !IsSettingsDockVisible
        );
    }

    public HomeViewModel HomeViewModel { get; }

    public ChordsViewModel ChordsViewModel { get; } = new();
    public ScalesViewModel ScalesViewModel { get; } = new();
    public VideosViewModel VideosViewModel { get; } = new();

    public SongsViewModel SongsViewModel { get; } = new();
    public SettingsViewModel SettingsViewModel { get; } = new();

    private bool IsSettingsDockVisible
    {
        get => _isSettingsDockVisible;
        set
        {
            if (_isSettingsDockVisible != value)
            {
                _isSettingsDockVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand ToggleSettingsCommand { get; }

    [RelayCommand]
    private void SetActivePage(string pageName)
    {
        IsHomeActive = pageName == "Home";
        IsChordsActive = pageName == "Chords";
        IsScalesActive = pageName == "Scales";
        IsVideosActive = pageName == "Videos";
        IsSongsActive = pageName == "Songs";
        IsSettingsActive = pageName == "Settings";
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
