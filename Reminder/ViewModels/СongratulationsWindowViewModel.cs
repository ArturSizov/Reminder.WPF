using DevExpress.Mvvm;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Reminder.Contracts;
using Reminder.Models;
using Reminder.Resources;
using Reminder.Views.Windows;
using System.Windows.Input;

namespace Reminder.ViewModels
{
    public class СongratulationsWindowViewModel : ObservableRecipient
    {
        private IRepository _repository;

        public string? Name;
        public string? LastName;
        public string? MiddleName;

        public string? Text { get; set; }

        public string? QestionText => Dict.Translate(Dict.Parameter.Сongratulations_window_text_question);

        public СongratulationsWindowViewModel(IRepository repository)
        {
            _repository = repository;
        }
        #region Header
        public string YesButtonHeader => Dict.Translate(Dict.Parameter.Button_yes);
        public string NoButtonHeader => Dict.Translate(Dict.Parameter.Button_no);

        #endregion

        #region Methods
        /// <summary>
        /// Set text congratulations window
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public string SetText(Person person)
        {
            int[] age = new[] { 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 95, 100 };

            string text = $"{Dict.Translate(Dict.Parameter.Happy_birthday_text)}\n {person.Name} {person.MiddleName}!".Replace(" !", "!");

            foreach (var a in age)
            {
                if (person.Age == a)
                {
                    text = $"{Dict.Translate(Dict.Parameter.Happy_anniversary_text)}\n {person.Name} {person.MiddleName}!".Replace(" !", "!");
                    break;
                }
            }
            return text;
        }

        #endregion

        #region Commands
        public ICommand YesButtonCommand => new DelegateCommand<СongratulationsWindow>((win) =>
        {
            var r = new Report { Name = Name, LastName = LastName, MiddleName = MiddleName, Status = "Yes" };
            _repository.Reports.Add(r);
            _repository.Save(r);
            if (win != null) win.Close();

        });

        public ICommand NoButtonCommand => new DelegateCommand<СongratulationsWindow>((win) =>
        {
            var r = new Report { Name = Name, LastName = LastName, MiddleName = MiddleName, Status = "No" };
            _repository.Reports.Add(r);
            _repository.Save(r);
            if (win != null) win.Close();
        });
        #endregion
    }
}