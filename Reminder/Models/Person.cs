﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace Reminder.Models
{
    public class Person : ObservableRecipient
    {
        private Guid _id = new Guid();
        private string? _name;
        private string? _middleName;
        private string? _lastName;
        private string? _position;
        private DateTime _birthday = new DateTime(2000, 1, 10);
        private int _arg;
        private double _days;

        public Guid Id { get => _id; set => SetProperty(ref _id, value); }
        public string? Name { get => _name; set => SetProperty(ref _name, value); }
        public string? MiddleName { get => _middleName; set => SetProperty(ref _middleName, value); }
        public string? LastName { get => _lastName; set => SetProperty(ref _lastName, value); }
        public string? Position { get => _position; set => SetProperty(ref _position, value); }
        public DateTime Birthday { get => _birthday; set => SetProperty(ref _birthday, value); }
        public int Arg { get => _arg; set => SetProperty(ref _arg, value); }
        public double Days { get => _days; set => SetProperty(ref _days, value); }
    }
}
