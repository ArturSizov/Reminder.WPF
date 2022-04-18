using DevExpress.Mvvm;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using Reminder.Resources;
using Reminder.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using IDialogService = Reminder.Contracts.IDialogService;
namespace Reminder.ViewModels
{
    public class SettingsWindowViewModel : ObservableRecipient
    {
        private string _refDBfile = Dict.SetRefDBFile;
        private int _selectedIndex;
        private string _language;
        private string _refDBFile;

        public string Title => Dict.Translate(Dict.Parameter.Title_setting_window);

        /// <summary>
        /// Index languages
        /// </summary>
        public int SelectedIndex { get => _selectedIndex; set => SetProperty(ref _selectedIndex, value); }

        private readonly IDialogService _dialogService;

        /// <summary>
        /// Collection languages combo box
        /// </summary>
        public ObservableCollection<string> Languages { get; set; }

        /// <summary>
        /// Selected value combo box
        /// </summary>
        public string Language { get => _language; set => SetProperty(ref _language, value); }

        public string RefDBFile { get => _refDBFile; set => SetProperty(ref _refDBFile, value); }

        #region Header
        public string? LanguageHeader => Dict.Translate(Dict.Parameter.Setting_language_heder);
        public string? SaveHeader => Dict.Translate(Dict.Parameter.Content_dutton_save);
        public string? CancelHeader => Dict.Translate(Dict.Parameter.Content_dutton_cancel);
        public string DBHeader => Dict.Translate(Dict.Parameter.DB_heder);

        public string SelectHeader => Dict.Translate(Dict.Parameter.SelectDB);

        #endregion
        public SettingsWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Languages = Dict.Languages;

            SelectedIndex = Dict.SelectedIndex;

            if (_refDBfile == null || _refDBfile == "Data Source=DataBase.db")
            {
                RefDBFile = Dict.Translate(Dict.Parameter.Default_DB);
            }
            else RefDBFile = _refDBfile;
        }

        #region Commands

        /// <summary>
        /// Open a dialog box for selecting a database
        /// </summary>
        public ICommand ShowDialog => new DelegateCommand(() =>
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = $"{Dict.Translate(Dict.Parameter.DB)}(*.db)|*.db";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = true;

            if (myDialog.ShowDialog() == true)
            {
                _refDBfile = RefDBFile = myDialog.FileName;
            }
        });

        /// <summary>
        /// Save command
        /// </summary>
        public ICommand Save => new DelegateCommand(() =>
        {
            MessageBoxResult dialogResul = MessageBox.Show(Dict.Translate(Dict.Parameter.Message_setting_window), Dict.Translate(Dict.Parameter.Warning), MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (dialogResul == MessageBoxResult.Yes)
            {
                _dialogService.IsActiveNotify = false;

                Dict.WriteSettings(Language, _refDBfile);

                Application.Current.Shutdown();
            }
            else return;
        });
        /// <summary>
        /// Cancel command
        /// </summary>
        public ICommand Cancel => new DelegateCommand<SettingsWindow>((win) =>
        {
            if (win != null) win.Close();
        });
        #endregion
    }
}
