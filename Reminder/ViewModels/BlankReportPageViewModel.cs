using DevExpress.Mvvm;
using Reminder.Contracts;
using Reminder.Resources;
using System.Windows.Input;

namespace Reminder.ViewModels
{
    public class BlankReportPageViewModel
    {
        private readonly IPageService _navigatot;

        private readonly IRepository _repository;

        public string Text => Dict.Translate(Dict.Parameter.Blank_report_text);

        public string ButtonHeader => Dict.Translate(Dict.Parameter.Blank_button);

        public BlankReportPageViewModel(IPageService navigatot, IRepository repository)
        {
            _navigatot = navigatot;
            _repository = repository;
        }

        #region Command
        /// <summary>
        /// Go to person page command
        /// </summary>
        public ICommand GoToPersonPageCommand => new DelegateCommand(() =>
        {
            if (_repository.Persons.Count == 0) _navigatot.GoToEmptyPage();

            else _navigatot.GoToPersonPage();
        });
       #endregion

    }
}
