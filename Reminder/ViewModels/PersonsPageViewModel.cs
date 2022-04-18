using DevExpress.Mvvm;
using Reminder.Contracts;
using Reminder.Models;
using Reminder.Resources;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using IDialogService = Reminder.Contracts.IDialogService;
using System.Threading;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Drawing;

namespace Reminder.ViewModels
{
    public class PersonsPageViewModel : ObservableRecipient
    {
        private IRepository _repository;
        private IDialogService _dialogService;
        private IPageService _navigation;
        private Person? _selectedItem;
        private ObservableCollection<Person>? _persons;
        private FontWeight _fontWeigh = FontWeights.Bold;
        private Brush _brush = Brushes.Red;

        public FontWeight FontWeigh { get => _fontWeigh; set => SetProperty(ref _fontWeigh, value); }

        public Brush Brush { get => _brush; set => SetProperty(ref _brush, value); }
        /// <summary>
        /// Selected item for context menu
        /// </summary>
        public Person? SelectedItem { get => _selectedItem; set => SetProperty(ref _selectedItem, value); }

        /// <summary>
        /// Person collection
        /// </summary>
        public ObservableCollection<Person>? Persons { get => _persons; set => SetProperty(ref _persons, value); }
        public PersonsPageViewModel(IRepository repository, IPageService navigation, IDialogService dialogService)
        {
            _navigation = navigation;
            _repository = repository;
            _dialogService = dialogService;

            Persons = _repository.Persons;

            Timer timer = new Timer(new TimerCallback(_repository.CalculateTiming));
            timer.Change(0, 5000);
        }


        #region Header
        public string ToolTipText => Dict.Translate(Dict.Parameter.Tool_tip_text);
        public string MenuItemEditHeader => Dict.Translate(Dict.Parameter.Menu_item_edit);
        public string MenuItemDeleteHeader => Dict.Translate(Dict.Parameter.Menu_item_delete);
        public string NameHeader => Dict.Translate(Dict.Parameter.Name_person);
        public string LastNameHeader => Dict.Translate(Dict.Parameter.Last_name_person);
        public string MiddleNameHeader => Dict.Translate(Dict.Parameter.Middle_name_person);
        public string DateHeader => Dict.Translate(Dict.Parameter.Recording_date);
        public string PositionHeader => Dict.Translate(Dict.Parameter.Position_person);
        public string BirthdayHeader => Dict.Translate(Dict.Parameter.Birthday_person);
        public string DaysHeader => Dict.Translate(Dict.Parameter.Days_person);
        public string ArgHeader => Dict.Translate(Dict.Parameter.Arg_person);


        #endregion

        #region Commands

        /// <summary>
        /// Deleting from the database and from the collection
        /// </summary>
        public ICommand Remove => new DelegateCommand<Person>((SelectedItem) =>
        {
            if (SelectedItem == null)
            {
                MessageBox.Show(Dict.Translate(Dict.Parameter.Message_delete_person_error), Dict.Translate(Dict.Parameter.Error_header), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"{Dict.Translate(Dict.Parameter.Message_delete_person)} {SelectedItem.Name}?", Dict.Translate(Dict.Parameter.Warning), MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _repository.Persons?.Remove(SelectedItem);

                _repository.Remove(SelectedItem);

                if (_repository.Persons.Count == 0) _navigation.GoToEmptyPage();
            }
            else return;
        });

        /// <summary>
        /// Editing a person in the database
        /// </summary>
        public ICommand Edit => new DelegateCommand<Person>((SelectedItem) =>
        {
            if (SelectedItem == null)
            {
                MessageBox.Show(Dict.Translate(Dict.Parameter.Message_edit_person), Dict.Translate(Dict.Parameter.Error_header), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _dialogService.ShowDialogEditWindow(SelectedItem);
        });
        #endregion
    }
}
