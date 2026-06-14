 
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using SukiUI.Controls;

namespace Meida.Desktop.ViewModels;

public  partial class MenuItemViewModel : SukiSideMenuItem
{    
      private string _header = string.Empty;
      private ContentControl _pageContent = new ContentControl();
      private string _icon = string.Empty;
      private bool _isVisible = true;
      private bool _isExpanded = true;

}
