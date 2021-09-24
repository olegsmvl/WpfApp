using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using WpfApp.Model;
using WpfApp.Controller;

namespace WpfApp
{
    class AppViewModel
    {
        public ObservableCollection<Record> Records { get; set; }

        private DbManager DbManager;
        private const int TEXT_LENGTH_ITEM = 6;

        public AppViewModel()
        {
            DbManager = new DbManager();
            UpdateComboBoxAsync();
        }

        private async void UpdateComboBoxAsync()
        {
            var records = await DbManager.LoadData();
            //comboBox.ItemsSource = records.Select(x => BuildItem(x));
        }

        private string BuildItem(Record record)
        {
            if (record.Text.Length < TEXT_LENGTH_ITEM)
                return $"{record.Id} \"{record.Text.Substring(0, record.Text.Length)}\" {record.Log.DateTime}";

            return $"{record.Id} \"{record.Text.Substring(0, TEXT_LENGTH_ITEM)}\" {record.Log.DateTime}";
        }
    }
}
