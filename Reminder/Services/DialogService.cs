using Microsoft.Toolkit.Mvvm.ComponentModel;
using Reminder.Contracts;
using Reminder.Models;
using Reminder.Resources;
using Reminder.ViewModels;
using Reminder.Views.Windows;
using System;
using System.Media;
using System.Windows;
using IDialogService = Reminder.Contracts.IDialogService;

namespace Reminder.Services
{
    public class DialogService : ObservableRecipient, IDialogService
    {
        private IRepository _repository;
        private IPageService _navidator;
        private SoundPlayer SoundPlayer = new();
        private bool _isActive = true;
        private bool _isActiveNotify = true;

        public new bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }
        public bool IsActiveNotify { get => _isActiveNotify; set => SetProperty(ref _isActiveNotify, value); }
        public DialogService(IRepository repository, IPageService navidator)
        {
            _repository = repository;

            _navidator = navidator;
        }

        #region Methods
        /// <summary>
        /// Show dialog
        /// </summary>
        public void ShowDialogCongratulationsWindow(Person person)
        {
            СongratulationsWindowViewModel vm = new(_repository);

            vm = new СongratulationsWindowViewModel(_repository)
            {
                Name = person.Name,
                LastName = person.LastName,
                MiddleName = person.MiddleName,
                Text = vm.SetText(person)
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                IsActive = false;

                var win = new СongratulationsWindow();
                win.DataContext = vm;
                win.ShowDialog();
                IsActive = true;
            });
        }

        /// <summary>
        /// Show congratulations Window
        /// </summary>
        /// <param name="obj"></param>
        public void ShowСongratulationsWindow(object obj)
        {
            foreach (var item in _repository.Persons)
            {
                if (IsActive && _navidator.IsActive)
                {
                    if (item.RemainingDays == 0)
                    {
                        SoundPlayer.Stream = Properties.Resources.Sound;
                        SoundPlayer.Play();
                        ShowDialogCongratulationsWindow(item);
                    }
                }
            }
        }

        /// <summary>
        /// Open add person window
        /// </summary>
        public void ShowDialogAddPersonWindow()
        {
            IsActive = false;

            var vm = new AddPersonWindowViewModel(_repository, _navidator);

            Application.Current.Dispatcher.Invoke(() =>
            {
                var win = new AddPersonWindow();
                win.DataContext = vm;
                win.ShowDialog();
                IsActive = true;
            });
        }

        /// <summary>
        /// Open edit person window
        /// </summary>
        public void ShowDialogEditWindow(Person person)
        {
            IsActive = false;

            var vm = new AddPersonWindowViewModel(_repository, _navidator)
            {
                Title = Dict.Translate(Dict.Parameter.Title_edit),
                Person = new Person
                {
                    Age = person.Age,
                    MiddleName = person.MiddleName,
                    Birthday = person.Birthday,
                    RemainingDays = person.RemainingDays,
                    LastName = person.LastName,
                    Name = person.Name,
                    Position = person.Position,
                    Id = new Guid()
                }
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                var win = new AddPersonWindow();
                win.DataContext = vm;

                win.ShowDialog();

                if (vm.buttonStatus)
                {
                    _repository.Persons.Remove(person);
                    _repository.Remove(person);
                }

                IsActive = true;
            });
        }

        /// <summary>
        /// Show settings window
        /// </summary>
        public void ShowSettingsWindow()
        {
            var win = new SettingsWindow();
            win.ShowDialog();

            IsActive = true;
        }

        #endregion
    }
}
