using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Reminder.Resources
{
    public static class Dict
    {
        private static string _setRefDBFile;
        public static string SetRefDBFile
        {
            get => _setRefDBFile;
            set
            {
                if (value == "")
                    value = null;
                _setRefDBFile = value;
            }
        }

        public static string SetLanguage { get; private set; }

        public static ObservableCollection<string> Languages { get; set; }

        public enum Parameter
        {
            App_name,
            About_the_program_title,
            Close_App,
            About_the_program,
            File,
            Add,
            Settings,
            Report,
            Exit,
            Warning,
            Help,
            Enter_name,
            Title_setting_window,
            Blank_report_text,
            Setting_language_heder,
            Content_dutton_save,
            Content_dutton_cancel,
            Message_setting_window,
            DB_heder,
            Сongratulations_window_text_question,
            Button_yes,
            Button_no,
            Happy_birthday_text,
            Happy_anniversary_text,
            Question_clea_reports_text,
            Name_person,
            Last_name_person,
            Middle_name_person,
            Position_person,
            Birthday_person,
            Arg_person,
            Days_person,
            Recording_date,
            Congratulations_status,
            Clear_report_button,
            Status_yes,
            Status_no,
            Blank_button,
            Tool_tip_text,
            Menu_item_edit,
            Menu_item_delete,
            Error_header,
            Message_delete_person_error,
            Message_delete_person,
            Message_edit_person,
            Taskbar_open,
            Taskbar_close,
            Window_state_started_work,
            Window_state_i_am_working,
            Enter_last_name_header,
            Enter_middle_name_header,
            Enter_position_header,
            Entering_a_date_of_birth_header,
            ToolTip_add_person,
            Title_add,
            Title_edit,
            Error_Birth_day,
            Error_load_app_message,
            Epty_welcome_text,
            SelectDB,
            DB,
            Default_DB,
            Error_load_DB,
            CheckBox_content
        };
        static Dict()
        {
            Languages = new ObservableCollection<string>
            {
              "Russian",
              "English"
            };

            SetRefDBFile = ReadSetting(2);

            ReadLanguageSetting();
        }

        /// <summary>
        /// Setup languages
        /// </summary>

        /// <summary>
        /// Index languages
        /// </summary>
        public static int SelectedIndex { get; set; }

        #region Methods
        /// <summary>
        /// Translate language
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string Translate(Parameter parameter)
        {
            if (SetLanguage == Languages[0])
                return russian[parameter];

            else if (SetLanguage == Languages[1])
                return english[parameter];

            else return russian[parameter];
        }

        /// <summary>
        /// Write to language settings file
        /// </summary>
        /// <param name="language"></param>
        public static void WriteSettings(string language, string refDBFile, bool isCheked)
        {
            var path = "Settings.txt";
            using (StreamWriter writer = new StreamWriter(Path.GetFullPath(path)))
            {
                writer.WriteLine(language);
                writer.WriteLine(refDBFile);
                writer.WriteLine(isCheked);
            }
        }

        /// <summary>
        /// Read to language settings file
        /// </summary>
        private static void ReadLanguageSetting()
        {
            var language = ReadSetting(1);
            if (language != null)
                SetLanguage = language;
            else SetLanguage = Languages[0];

            SelectedIndex = Languages.IndexOf(SetLanguage);
        }

        /// <summary>
        /// Reading data. Parameter accepting line number with saved settings
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public static string ReadSetting(int lineNumber)
        {
            var path = "Settings.txt";

            string line = null;

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    for (int i = 0; i < lineNumber; ++i)
                    {
                        line = reader.ReadLine();
                    }
                }
                return line;
            };
        }

        #endregion

        #region Languages
        private static Dictionary<Parameter, string> russian = new Dictionary<Parameter, string>
        {
            [Parameter.App_name] = "Напоминалка",
            [Parameter.File] = "Файл",
            [Parameter.Add] = "Добавить",
            [Parameter.Report] = "Отчет",
            [Parameter.Settings] = "Настройки",
            [Parameter.Help] = "Помощь",
            [Parameter.Exit] = "Выход",
            [Parameter.Warning] = "Внимание",
            [Parameter.About_the_program_title] = "О программе",
            [Parameter.Close_App] = "Закрыть приложение?",
            [Parameter.About_the_program] = "Программа создана для\nнапоминания о днях рождений.\n2022г.",
            [Parameter.Enter_name] = "Введите имя:",
            [Parameter.Title_setting_window] = "Настройки",
            [Parameter.Blank_report_text] = "На данной странице\n фиксируется поздравлен ли именинник/юбиляр.\nНа данный момент нет записей в отчёте.",
            [Parameter.Setting_language_heder] = "Язык:",
            [Parameter.Content_dutton_save] = "Сохранить",
            [Parameter.Content_dutton_cancel] = "Отмена",
            [Parameter.Message_setting_window] = "Для вступления изменений, необходимо перезапустить приложение.\nПерезапустить?",
            [Parameter.DB_heder] = "БД:",
            [Parameter.Сongratulations_window_text_question] = "Поздравили?",
            [Parameter.Button_yes] = "Да",
            [Parameter.Button_no] = "Нет",
            [Parameter.Happy_birthday_text] = "С днем рождения",
            [Parameter.Happy_anniversary_text] = "С Юбилеем",
            [Parameter.Question_clea_reports_text] = "Вы уверены, что хотите очистить отчет?",
            [Parameter.Name_person] = "Имя",
            [Parameter.Last_name_person] = "Фамилия",
            [Parameter.Middle_name_person] = "Отчество",
            [Parameter.Position_person] = "Должность",
            [Parameter.Birthday_person] = "Дата рождения",
            [Parameter.Arg_person] = "Возраст",
            [Parameter.Days_person] = "Осталось дней",
            [Parameter.Recording_date] = "Дата записи",
            [Parameter.Congratulations_status] = "Статус",
            [Parameter.Clear_report_button] = "Очистить отчёт",
            [Parameter.Status_yes] = "Да",
            [Parameter.Status_no] = "Нет",
            [Parameter.Blank_button] = "Вернуться на главную страницу",
            [Parameter.Tool_tip_text] = "Для редактирования/удаления данных, выберите элемент, нажмите правую кнопку мыши для вызова контекстного меню",
            [Parameter.Menu_item_delete] = "Удалить",
            [Parameter.Menu_item_edit] = "Редактировать",
            [Parameter.Error_header] = "Ошибка",
            [Parameter.Message_delete_person_error] = "Для удаления выберите один из элементов",
            [Parameter.Message_delete_person] = "Вы деиствительно хотите удалить мониторинг для:",
            [Parameter.Message_edit_person] = "Для редактирования выберите один из элементов",
            [Parameter.Taskbar_open] = "Открыть",
            [Parameter.Taskbar_close] = "Вызод",
            [Parameter.Window_state_started_work] = "Я начал свою работу :)",
            [Parameter.Window_state_i_am_working] = "Я всё еще работаю :)",
            [Parameter.ToolTip_add_person] = "В данном поле вводится только текс, не менее двух букв.",
            [Parameter.Enter_last_name_header] = "Введите фамилию:",
            [Parameter.Enter_middle_name_header] = "Введите отчество:",
            [Parameter.Enter_position_header] = "Введите должность:",
            [Parameter.Entering_a_date_of_birth_header] = "Введите дату рождения:",
            [Parameter.Title_add] = "Добавить",
            [Parameter.Title_edit] = "Редактировать",
            [Parameter.Error_Birth_day] = "Введена не верная дата рождения",
            [Parameter.Error_load_app_message] = "Напоминалка уже запущена",
            [Parameter.Epty_welcome_text] = "Добро пожаловать!\nПрограмма создана для\nнапоминания о днях рождений.\nНачните работу с кнопки \"Добавть\"",
            [Parameter.SelectDB] = "Выбрать",
            [Parameter.DB] = "База данных",
            [Parameter.Default_DB] = "База данных по умолчанию",
            [Parameter.Error_load_DB] = "База данных повреждена, для работы приложения создана по умолчанию.\nДля указания корректной базы данных, переидите в настройки.",
            [Parameter.CheckBox_content] = "Автозагрузка программы"
        };

        private static Dictionary<Parameter, string> english = new Dictionary<Parameter, string>
        {
            [Parameter.App_name] = "Reminder",
            [Parameter.File] = "File",
            [Parameter.Add] = "Add",
            [Parameter.Report] = "Report",
            [Parameter.Settings] = "Settings",
            [Parameter.Help] = "Help",
            [Parameter.Exit] = "Exit",
            [Parameter.Warning] = "Warning",
            [Parameter.About_the_program_title] = "About the program",
            [Parameter.Close_App] = "Close application?",
            [Parameter.Enter_name] = "Enter your name:",
            [Parameter.About_the_program] = "The program was created\nto remind you of birthdays.\n2022 yer.",
            [Parameter.Title_setting_window] = "Settings",
            [Parameter.Blank_report_text] = "On this page it is fixed whether\nthe birthday person/hero of the day is congratulated.\nThere are currently no entries in the report.",
            [Parameter.Setting_language_heder] = "Language:",
            [Parameter.Content_dutton_save] = "Save",
            [Parameter.Content_dutton_cancel] = "Cancel",
            [Parameter.Message_setting_window] = "For the changes to take effect, you must restart the application.\nRestart?",
            [Parameter.DB_heder] = "DB:",
            [Parameter.Сongratulations_window_text_question] = "Сongratulated?",
            [Parameter.Button_yes] = "Yes",
            [Parameter.Button_no] = "No",
            [Parameter.Happy_birthday_text] = "Happy birthday",
            [Parameter.Happy_anniversary_text] = "Happy Anniversary",
            [Parameter.Question_clea_reports_text] = "Are you sure you want to clear the report?",
            [Parameter.Name_person] = "Name",
            [Parameter.Last_name_person] = "Last name",
            [Parameter.Middle_name_person] = "Middle name",
            [Parameter.Position_person] = "Position",
            [Parameter.Birthday_person] = "Date of Birth",
            [Parameter.Arg_person] = "Arg",
            [Parameter.Days_person] = "Days left",
            [Parameter.Recording_date] = "Recording date",
            [Parameter.Congratulations_status] = "Status",
            [Parameter.Clear_report_button] = "Clear report",
            [Parameter.Status_yes] = "Yes",
            [Parameter.Status_no] = "No",
            [Parameter.Blank_button] = "Return to main page",
            [Parameter.Tool_tip_text] = "To edit/delete data, select an element, right-click to open the context menu",
            [Parameter.Menu_item_delete] = "Delete",
            [Parameter.Menu_item_edit] = "Edit",
            [Parameter.Error_header] = "Error",
            [Parameter.Message_delete_person_error] = "Select one of the items to delete",
            [Parameter.Message_delete_person] = "Are you sure you want to remove monitoring for:",
            [Parameter.Message_edit_person] = "Select one of the elements to edit",
            [Parameter.Taskbar_open] = "Open",
            [Parameter.Taskbar_close] = "Exit",
            [Parameter.Window_state_started_work] = "I started my work :)",
            [Parameter.Window_state_i_am_working] = "I'm still working :)",
            [Parameter.ToolTip_add_person] = "In this field, only text is entered, at least two letters.",
            [Parameter.Enter_last_name_header] = "Enter last name:",
            [Parameter.Enter_middle_name_header] = "Enter middle name:",
            [Parameter.Enter_position_header] = "Enter position:",
            [Parameter.Entering_a_date_of_birth_header] = "Enter date of birth:",
            [Parameter.Title_add] = "Add",
            [Parameter.Title_edit] = "Edit",
            [Parameter.Error_Birth_day] = "Invalid date of birth entered",
            [Parameter.Error_load_app_message] = "Reminder already started",
            [Parameter.Epty_welcome_text] = "Welcome!\nThe program was created to\nremind you of birthdays.\nGet started with a button \"Add\"",
            [Parameter.SelectDB] = "Select",
            [Parameter.DB] = "Data base",
            [Parameter.Default_DB] = "Default database",
            [Parameter.Error_load_DB] = "The database is corrupted, it was created by default for the application to work.\nTo specify the correct database, go to the settings.",
            [Parameter.CheckBox_content] = "Autorun application"
        };
        #endregion
    }
}
