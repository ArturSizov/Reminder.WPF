using DevExpress.Mvvm;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Reminder.Contracts;
using Reminder.Resources;
using Reminder.Services.Toolkit;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IDialogService = Reminder.Contracts.IDialogService;

namespace Reminder.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private readonly IPageService _navigation;
        private readonly IRepository _repository;
        private readonly IDialogService _dialogService;

        private string _message => Dict.Translate(Dict.Parameter.About_the_program);

        public string Title { get; set; } = Dict.Translate(Dict.Parameter.App_name);

        private Page? _currentPage;

        public Page? CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public MainWindowViewModel(IPageService navigation, IRepository repository, IDialogService dialogService)
        {
            navigation.OnPageChanget += page => CurrentPage = page;

            _navigation = navigation;

            _repository = repository;

            _dialogService = dialogService;

            if (_repository.Persons.Count == 0) _navigation.GoToEmptyPage();
            else _navigation.GoToPersonPage();

            Timer timer = new Timer(new TimerCallback(dialogService.ShowСongratulationsWindow));
            timer.Change(5000, 43200000);
        }

        #region Header
        public string FileHeader => Dict.Translate(Dict.Parameter.File);
        public string AddHeader => Dict.Translate(Dict.Parameter.Add);
        public string SettingsHeader => Dict.Translate(Dict.Parameter.Settings);
        public string ReportHeader => Dict.Translate(Dict.Parameter.Report);
        public string ExitHeader => Dict.Translate(Dict.Parameter.Exit);
        public string HelpHeader => Dict.Translate(Dict.Parameter.Help);
        public string AboutTheProgramHeader => Dict.Translate(Dict.Parameter.About_the_program_title);

        #endregion

        #region Commands
        /// <summary>
        /// Add person command
        /// </summary>
        public ICommand AddPersonCommand => new DelegateCommand(() =>
        {
            _dialogService.ShowDialogAddPersonWindow();

        });

        /// <summary>
        /// Show report command
        /// </summary>
        public ICommand ShowReportPageCommand => new DelegateCommand(() =>
        {
            if (_repository.Reports?.Count != 0)
            {
                _navigation.GoToReportPage();
            }
            else
            {
                _navigation.GoToBlankReportPage();
            }
        });

        /// <summary>
        /// Show settings window command
        /// </summary>
        public ICommand ShowSettingsWindowCommand => new DelegateCommand(() =>
        {
            _dialogService.ShowSettingsWindow();
        });
        /// <summary>
        /// Displaying information about the program
        /// </summary>
        public ICommand GetInformationCommand => new DelegateCommand(() =>
        {
            MessageBox.Show(_message, Dict.Translate(Dict.Parameter.About_the_program_title), MessageBoxButton.OK, MessageBoxImage.Information);

        });
        /// <summary>
        /// Close window command
        /// </summary>
        public ICommand CloseWindowCommand => new DelegateCommand<MainWindow>((win) =>
        {
            MessageBoxResult dialogResult = MessageBox.Show(Dict.Translate(Dict.Parameter.Close_App), Dict.Translate(Dict.Parameter.App_name), MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (dialogResult == MessageBoxResult.Yes)
            {
                _dialogService.IsActiveNotify = false;
                Application.Current.Shutdown();
            }
            else if (dialogResult == MessageBoxResult.No) return;
        });

        #region Toolkit
        private NotifyIconWrapper.NotifyRequestRecord? _notifyRequest;
        private bool _showInTaskbar;
        private WindowState _windowState;

        #region Toolkit commands
        public ICommand LoadedCommand => new DelegateCommand(() =>
        {
            WindowState = WindowState.Minimized;
            Notify("", Dict.Translate(Dict.Parameter.Window_state_started_work));
        });
        public ICommand ClosingCommand => new DelegateCommand<CancelEventArgs>((e) =>
        {
            if (_navigation.IsActive == false)
            {
                if (_repository.Persons.Count == 0 && _repository.Reports.Count == 0)
                {
                    _navigation.GoToEmptyPage();
                }
                else _navigation.GoToPersonPage();
            }

            if (e == null)
                return;
            e.Cancel = true;
            WindowState = WindowState.Minimized;
            Notify("", Dict.Translate(Dict.Parameter.Window_state_i_am_working));
        });
        public ICommand NotifyCommand => new DelegateCommand(() =>
        {
            Notify("", "");
        });
        public ICommand NotifyIconOpenCommand => new DelegateCommand(() =>
        {
            WindowState = WindowState.Normal;
        });
        public ICommand NotifyIconExitCommand => new DelegateCommand(() =>
        {
            MessageBoxResult dialogResult = MessageBox.Show(Dict.Translate(Dict.Parameter.Close_App), Dict.Translate(Dict.Parameter.App_name), MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (dialogResult == MessageBoxResult.Yes)
            {
                _dialogService.IsActiveNotify = false;
                Application.Current.Shutdown();
            }
            else if (dialogResult == MessageBoxResult.No) return;
        });
        #endregion
        #endregion

        #endregion

        #region Methods

        #region Toolkit methods
        /// <summary>
        /// Set windows set
        /// </summary>
        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                ShowInTaskbar = true;
                SetProperty(ref _windowState, value);
                ShowInTaskbar = value != WindowState.Minimized;
            }
        }

        /// <summary>
        /// Whether to display the taskbar
        /// </summary>
        public bool ShowInTaskbar
        {
            get => _showInTaskbar;
            set => SetProperty(ref _showInTaskbar, value);
        }

        /// <summary>
        /// Notify request
        /// </summary>
        public NotifyIconWrapper.NotifyRequestRecord? NotifyRequest
        {
            get => _notifyRequest;
            set => SetProperty(ref _notifyRequest, value);
        }

        /// <summary>
        /// Toolkit notify
        /// </summary>
        /// <param name="message"></param>
        private void Notify(string title, string message)
        {
            if (_dialogService.IsActiveNotify)
            {
                NotifyRequest = new NotifyIconWrapper.NotifyRequestRecord
                {
                    Title = title,
                    Text = message,
                };
            }
        }
        #endregion

       #endregion
    }
}
