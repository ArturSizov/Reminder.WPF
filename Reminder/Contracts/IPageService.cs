using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace Reminder.Contracts
{
    public interface IPageService : INotifyPropertyChanged
    {
        public event Action<Page>? OnPageChanget;
        public bool IsActive { get; set; }
        public void GoToEmptyPage();

        public void GoToPersonPage();

        public void GoToReportPage();

        public void GoToBlankReportPage();
    }
}
