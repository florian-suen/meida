using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Material.Icons;
using SukiUI.Controls;

namespace Meida.Desktop.Models;

public class MenuItemViewModel : SukiSideMenuItem
{
    public ContentControl PageContent = new UserControl();
    public string Header { get; set; }
    public MaterialIconKind IconKind { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
