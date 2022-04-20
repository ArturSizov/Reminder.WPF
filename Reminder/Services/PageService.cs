using Microsoft.Toolkit.Mvvm.ComponentModel;
using Reminder.Contracts;
using Reminder.Views.Pages;
using System;
using System.Windows.Controls;

namespace Reminder.Services
{
    public class PageService : ObservableRecipient, IPageService
    {
        private bool _isActive;

        public event Action<Page>? OnPageChanget;

        /// <summary>
        /// Activate it
        /// </summary>
        public new bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }

        /// <summary>
        /// Go to Empty page
        /// </summary>
        public void GoToEmptyPage()
        {
            OnPageChanget?.Invoke(new EmptyPage());
            IsActive = false;
        }

        /// <summary>
        /// Go to person page
        /// </summary>
        public void GoToPersonPage()
        {
            OnPageChanget?.Invoke(new PersonsPage());
            IsActive = true;
        }

        /// <summary>
        /// Go to report page
        /// </summary>
        public void GoToReportPage()
        {
            OnPageChanget?.Invoke(new ReportPage());
            IsActive = false;
        }
        /// <summary>
        /// Go ro blank report page
        /// </summary>
        public void GoToBlankReportPage()
        {
            OnPageChanget?.Invoke(new BlankReportPage());
            IsActive = false;
        }   
    }
}
