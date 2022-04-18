using DevExpress.Mvvm;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Reminder.Contracts;
using Reminder.Models;
using Reminder.Resources;
using Reminder.Views.Windows;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Reminder.ViewModels
{
    public class AddPersonWindowViewModel : ObservableRecipient, IAddPerson
    {
        private IRepository _repository;
        private IPageService _navigation;
        private Person _person = new();

        public string Title { get; set; } = Dict.Translate(Dict.Parameter.Title_add);
        public Person Person { get => _person; set => SetProperty(ref _person, value); }

        public bool buttonStatus;

        public AddPersonWindowViewModel(IRepository repository, IPageService navigation)
        {
            _repository = repository;
            _navigation = navigation;
        }

        #region Header
        public string EnterNameHeader => Dict.Translate(Dict.Parameter.Enter_name);
        public string EnterLastNameHeader => Dict.Translate(Dict.Parameter.Enter_last_name_header);
        public string EnterMiddleNameHeader => Dict.Translate(Dict.Parameter.Enter_middle_name_header);
        public string EnterPositionHeader => Dict.Translate(Dict.Parameter.Enter_position_header);
        public string EnteringADateOfBirthHeader => Dict.Translate(Dict.Parameter.Entering_a_date_of_birth_header);
        public string ToolTipText => Dict.Translate(Dict.Parameter.ToolTip_add_person);
        public string SaveButtonHeader => Dict.Translate(Dict.Parameter.Content_dutton_save);
        public string CancelButtonHeader => Dict.Translate(Dict.Parameter.Content_dutton_cancel);

        #endregion

        /// <summary>
        /// Check for button save
        /// </summary>
        /// <returns></returns>
        private bool Setter()
        {
            if (!string.IsNullOrEmpty(Person.LastName) && !string.IsNullOrEmpty(Person.Name)
                && Person.Name?.Length >= 2 && Person.LastName?.Length >= 2
                && Person.Name.All(Char.IsLetter) && Person.LastName.All(Char.IsLetter)) return true;

            return false;
        }

        #region Commands
        /// <summary>
        /// Save command
        /// </summary>
        public ICommand Save => new DelegateCommand<AddPersonWindow>((win) =>
        {
            if (Person.Birthday <= DateTime.Today && Person.Birthday >= new DateTime(1935, 1, 10))
            {
                if (_repository.Persons.Any((x) => x.Id == Person.Id))
                {
                    return;
                }
                else
                {
                    _repository.Persons.Add(Person);
                    _repository.CalculateTiming();
                    _repository.Save(Person);
                    if (!_navigation.IsActive) _navigation.GoToPersonPage();
                    if (win != null) win.Close();
                }
                buttonStatus = true;
            }
            else
            {
                MessageBox.Show(Dict.Translate(Dict.Parameter.Error_Birth_day), Dict.Translate(Dict.Parameter.Error_header), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }, (DelegateCommand) => Setter());

        /// <summary>
        /// Cancel command
        /// </summary>
        public ICommand Cancel => new DelegateCommand<AddPersonWindow>((win) =>
        {

            if (_repository.Persons.Count == 0) _navigation.GoToEmptyPage();

            if (win != null) win.Close();

            buttonStatus = false;
        });

        /// <summary>
        /// Closing Wwindow command
        /// </summary>
        public ICommand ClosingCommand => new DelegateCommand<CancelEventArgs>((e) =>
        {
            if (e == null) return;
            else buttonStatus = e.Cancel;
        });
        #endregion
    }
}
