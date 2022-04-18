using DevExpress.Mvvm;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Reminder.Contracts;
using Reminder.Models;
using Reminder.Resources;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Reminder.ViewModels
{
    public class ReportPageViewModel : ObservableRecipient
    {
        private IRepository _repository;
        private IPageService _navigation;
        private ObservableCollection<Report>? _reports = new();

        public ObservableCollection<Report>? Reports { get => _reports; set => SetProperty(ref _reports, value); }

        public ReportPageViewModel(IRepository repository, IPageService navigation)
        {
            _repository = repository;
            _navigation = navigation;

            foreach (var item in _repository.Reports)
            {
                string status;

                if (item.Status == "Yes") status = Dict.Translate(Dict.Parameter.Status_yes);
                else status = Dict.Translate(Dict.Parameter.Status_no);

                var r = new Report
                {
                    Id = item.Id,
                    Name = item.Name,
                    LastName = item.LastName,
                    Date = item.Date,
                    MiddleName = item.MiddleName,
                    Status = status
                };
                Reports.Add(r);
            }
        }

        #region Header
        public string NameHeader => Dict.Translate(Dict.Parameter.Name_person);
        public string LastNameHeader => Dict.Translate(Dict.Parameter.Last_name_person);
        public string MiddleNameHeader => Dict.Translate(Dict.Parameter.Middle_name_person);
        public string DateHeader => Dict.Translate(Dict.Parameter.Recording_date);
        public string StatusHeader => Dict.Translate(Dict.Parameter.Congratulations_status);
        public string ClearButtonHeader => Dict.Translate(Dict.Parameter.Clear_report_button);
        public string CancelButtonHeader => Dict.Translate(Dict.Parameter.Content_dutton_cancel);

        #endregion

        #region Commands

        /// <summary>
        /// Clear collection and remove database reports
        /// </summary>
        public ICommand ClearReportsCommnad => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show(Dict.Translate(Dict.Parameter.Question_clea_reports_text), Dict.Translate(Dict.Parameter.Warning), MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Reports.Clear();
                _repository.RemoveAll();
                _navigation.GoToBlankReportPage();
            }
            else return;
        });

        /// <summary>
        /// Go to home page command
        /// </summary>
        public ICommand HommeCommand => new DelegateCommand(() =>
        {
            if (_repository.Persons.Count != 0)
            {
                _navigation.GoToPersonPage();
            }
            else
            {
                _navigation.GoToEmptyPage();
            }
        });
        #endregion
    }
}
