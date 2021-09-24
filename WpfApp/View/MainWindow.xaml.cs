using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using WpfApp.Controller;
using WpfApp.Model;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private DbManager DbManager;
        private const int TEXT_LENGTH_ITEM = 6;
        private int SelectedId = 0;

        public MainWindow()
        {
            InitializeComponent();
            DbManager = new DbManager();
            UpdateComboBoxAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb1.Text == string.Empty)
            {
                MessageBox.Show("Необходимо ввести текст");
                return;
            }

            DbManager.SaveRecordAsync(new Record { Id = SelectedId, Text = tb1.Text });
            UpdateComboBoxAsync();
        }

        private async void UpdateComboBoxAsync()
        {
            var records = await DbManager.LoadData();
            comboBox.ItemsSource = records.Select(x => BuildItem(x));
        }

        private string BuildItem(Record record)
        {
            if (record.Text.Length < TEXT_LENGTH_ITEM)
                return $"{record.Id} \"{record.Text.Substring(0, record.Text.Length)}\" {record.Log.DateTime}";

            return $"{record.Id} \"{record.Text.Substring(0, TEXT_LENGTH_ITEM)}\" {record.Log.DateTime}";
        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedIndex == -1) return;
            
            var item = (string)comboBox.SelectedItem;
            tb1.Text = item;
            var id = GetId(item);
            UpdateTextBox(id);
            SelectedId = id;
        }

        private async void UpdateTextBox(int id)
        {
            var selectedRecord = await DbManager.LoadData(id);

            if(selectedRecord != null)
                tb1.Text = selectedRecord.Text;
        }

        private int GetId(string text)
        {
            if (text == null) return 0;

            string id  = text.Substring(0, text.IndexOf(" "));
            int idInt;
            return Int32.TryParse(id, out idInt) ? idInt : 0;
        }

    }
}
