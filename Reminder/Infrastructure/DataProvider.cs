using Microsoft.EntityFrameworkCore;
using Reminder.Contracts;
using Reminder.Models;
using Reminder.Resources;
using System.Windows;

namespace Reminder.Infrastructure
{
    public sealed class DataProvider : DbContext, IDataProvider
    {
        private string? DB;

        public DbSet<Person>? Persons { get; set; }
        public DbSet<Report>? Reports { get; set; }

        public DataProvider(DbContextOptions<DataProvider> options) : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (System.Exception)
            {
                DB = $"Data Source=DataBase.db";

                Database.SetConnectionString(DB);

                MessageBox.Show(Dict.Translate(Dict.Parameter.Error_load_DB), Dict.Translate(Dict.Parameter.App_name), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Dict.SetRefDBFile != null)
            {
                DB = $"Data Source={Dict.SetRefDBFile}";
            }
            else
            {
                DB = $"Data Source=DataBase.db";
            }
            optionsBuilder.UseSqlite(DB);
        }

      #endregion
    }
}
