using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace Reminder.Models
{
    public class Report : ObservableRecipient
    {
        private Guid _id = new Guid();
        private string? _name;
        private string? _middleName;
        private string? _lastName;
        private DateTime _date = DateTime.Now;
        private string? _status;

        public Guid Id { get => _id; set => SetProperty(ref _id, value); }
        public string? Name { get => _name; set => SetProperty(ref _name, value); }
        public string? MiddleName { get => _middleName; set => SetProperty(ref _middleName, value); }
        public string? LastName { get => _lastName; set => SetProperty(ref _lastName, value); }
        public DateTime Date { get => _date; set => SetProperty(ref _date, value); }
        public string? Status { get => _status; set => SetProperty(ref _status, value); }
    }
}
