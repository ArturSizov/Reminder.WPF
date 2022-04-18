using System.ComponentModel;
using System.Windows.Input;

namespace Reminder.Contracts
{
    public interface IAddPerson : INotifyPropertyChanged
    {
        public ICommand Save { get; }

        public ICommand Cancel { get; }

    }
}
