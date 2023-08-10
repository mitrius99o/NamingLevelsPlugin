using Co.NamingLevelsPlugin.Contracts.InterfacesUI;
using System;
using System.Windows;
using System.Windows.Interop;

namespace Co.NamingLevelsPlugin.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window, IView
    {
        public MainWindowView(IViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public IntPtr GetIntPtr() => new WindowInteropHelper(this).Handle;

        public void SetOwner(IntPtr intPtr)
            => this.Owner = HwndSource.FromHwnd(intPtr).RootVisual as Window;
    }
}
