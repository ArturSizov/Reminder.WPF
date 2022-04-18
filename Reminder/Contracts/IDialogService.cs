using Reminder.Models;
using System.ComponentModel;

namespace Reminder.Contracts
{
    public interface IDialogService : INotifyPropertyChanged
    {
        public bool IsActive { get; set; }
        public bool IsActiveNotify { get; set; }
        public void ShowDialogCongratulationsWindow(Person person);

        public void ShowDialogAddPersonWindow();

        public void ShowDialogEditWindow(Person person);

        public void ShowSettingsWindow();

        public void ShowСongratulationsWindow(object obj);
    }
}
