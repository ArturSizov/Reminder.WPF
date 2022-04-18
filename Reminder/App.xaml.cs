using Reminder.Contracts;
using Reminder.Infrastructure;
using Reminder.Resources;
using Reminder.Services;
using Reminder.ViewModels;
using System;
using System.Threading;
using System.Windows;
using Unity;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected Mutex? Mutex;

        /// <summary>
        /// OnStartup handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            Mutex = new Mutex(true, "reminder", out bool isnew);

             if (isnew == false)
            {
                MessageBox.Show(Dict.Translate(Dict.Parameter.Error_load_app_message), Dict.Translate(Dict.Parameter.Error_header), MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
                return;
            }
            else
            {
                GC.KeepAlive(this.Mutex);
                base.OnStartup(e);
                ConfigureIOC();
            }
           
        }

        private void ConfigureIOC()
        {
            RootContainer.Container.RegisterSingleton<MainWindowViewModel, MainWindowViewModel>();
            RootContainer.Container.RegisterSingleton<IPageService, PageService>();
            RootContainer.Container.RegisterSingleton<IDialogService, DialogService>();
            RootContainer.Container.RegisterSingleton<IDataProvider, DataProvider>();
            RootContainer.Container.RegisterSingleton<IRepository, Repository>();
        }
    }
}
