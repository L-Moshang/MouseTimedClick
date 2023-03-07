using Prism.DryIoc;
using System.Windows;
using Prism.Ioc;
using MouseClickSender.ViewModels;
using MouseClickSender.Views;

namespace MouseClickSender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow, SettingInfoViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

    }
}
