using Meida.Desktop.ViewModels;
using SukiUI.Controls;

namespace Meida.Desktop.Views;

public partial class MainWindow : SukiWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
