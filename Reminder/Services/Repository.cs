using Microsoft.Toolkit.Mvvm.ComponentModel;
using Reminder.Contracts;
using Reminder.Infrastructure;
using Reminder.Models;
using System;
using System.Collections.ObjectModel;

namespace Reminder.Services
{
    public class Repository : ObservableRecipient, IRepository
    {
        private DataProvider _provider;
        private ObservableCollection<Person>? _persons;
        private ObservableCollection<Report>? _reports;

        public ObservableCollection<Person>? Persons { get => _persons; set => SetProperty(ref _persons, value); }
        public ObservableCollection<Report>? Reports { get => _reports; set => SetProperty(ref _reports, value); }

        public Repository(DataProvider provider)
        {
            _provider = provider;

            if (_provider.Persons != null) Persons = new ObservableCollection<Person>(_provider.Persons);

            if (_provider.Reports != null) Reports = new ObservableCollection<Report>(_provider.Reports);
        }

        /// <summary>
        /// Saving to database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Save<T>(T item)
        {
            _provider.Add(item);
            _provider.SaveChanges();
        }

        /// <summary>
        /// Delete from database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Remove<T>(T item)
        {
            _provider.Remove(item);
            _provider.SaveChanges();
        }

        /// <summary>
        /// Delete from all databas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void RemoveAll()
        {
            Reports.Clear();

            foreach (var p in _provider.Reports)
            {
                _provider.Remove(p);
            }
            _provider.SaveChanges();
        }
    }
}
