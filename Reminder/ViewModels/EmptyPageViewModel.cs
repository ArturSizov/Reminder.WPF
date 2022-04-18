using DevExpress.Mvvm;
using Reminder.Resources;
using System.Windows.Input;
using IDialogService = Reminder.Contracts.IDialogService;

namespace Reminder.ViewModels
{
    public class EmptyPageViewModel
    {
        private IDialogService _dialogService;

        public string Text => Dict.Translate(Dict.Parameter.Epty_welcome_text);

        public EmptyPageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        #region Header
        public string AddButtonHeader => Dict.Translate(Dict.Parameter.Title_add);
        #endregion

        /// <summary>
        /// Go to add person Window
        /// </summary>
        public ICommand GoToAddPersonPage => new DelegateCommand(() =>
        {
            _dialogService.ShowDialogAddPersonWindow();
        });
    }
}
