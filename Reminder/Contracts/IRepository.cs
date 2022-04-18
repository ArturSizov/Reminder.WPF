using Reminder.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Reminder.Contracts
{
    public interface IRepository : INotifyPropertyChanged
    {
        public ObservableCollection<Person>? Persons { get; set; }
        public ObservableCollection<Report>? Reports { get; set; }

        public void Save<T>(T item);

        public void Remove<T>(T item);

        public void RemoveAll();

        public void CalculateTiming(object obj = null);
    }
}
