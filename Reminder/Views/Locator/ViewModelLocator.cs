using Reminder.Infrastructure;
using Reminder.ViewModels;
using Unity;

namespace Reminder.Views.Locator
{
    public class ViewModelLocator 
    {
        public MainWindowViewModel MainWindowViewModel => RootContainer.Container.Resolve<MainWindowViewModel>();

        public EmptyPageViewModel EmptyPageViewModel => RootContainer.Container.Resolve<EmptyPageViewModel>();

        public ReportPageViewModel ReportPageViewModel => RootContainer.Container.Resolve<ReportPageViewModel>();

        public PersonsPageViewModel PersonsPageViewModel => RootContainer.Container.Resolve<PersonsPageViewModel>();

        public BlankReportPageViewModel BlankReportPageViewModel => RootContainer.Container.Resolve<BlankReportPageViewModel>();

        public СongratulationsWindowViewModel СongratulationsWindowViewModel => RootContainer.Container.Resolve<СongratulationsWindowViewModel>();

        public AddPersonWindowViewModel AddPersonWindowViewModel => RootContainer.Container.Resolve<AddPersonWindowViewModel>();

        public SettingsWindowViewModel SettingsWindowViewModel => RootContainer.Container.Resolve<SettingsWindowViewModel>();
    }
}
