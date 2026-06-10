using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Meida.Desktop.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private bool _isSettingsDockVisible;

    private bool _option1Enabled;

    private double _option2Value = 50;

    public MainWindowViewModel()
    {
        // Toggle the dock visibility when the command is executed
        ToggleSettingsCommand = new RelayCommand(_ =>
            IsSettingsDockVisible = !IsSettingsDockVisible
        );
    }

    public bool IsSettingsDockVisible
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

    public bool Option1Enabled
    {
        get => _option1Enabled;
        set
        {
            if (_option1Enabled != value)
            {
                _option1Enabled = value;
                OnPropertyChanged();
            }
        }
    }

    public double Option2Value
    {
        get => _option2Value;
        set
        {
            if (_option2Value != value)
            {
                _option2Value = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand ToggleSettingsCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

// Simple RelayCommand implementation for MVVM
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
