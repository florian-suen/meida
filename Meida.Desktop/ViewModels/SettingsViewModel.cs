namespace Meida.Desktop.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private bool _option1Enabled;

    private double _option2Value = 50;

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
}
