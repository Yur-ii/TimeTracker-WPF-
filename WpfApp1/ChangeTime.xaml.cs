using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace WpfApp1
{
    public partial class ChangeTime : Window
    {
        double inputTime;
        public ChangeTime()
        {
            InitializeComponent();
        }
        public void InputNumb(object sender, KeyEventArgs e)
        {
            if (((TextBox)sender).Text.Contains(','))
            {
                if (e.Key >= Key.D0 && e.Key <= Key.D9)
                    e.Handled = false;
                else e.Handled = true;
            }
            else
            {
                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemComma)
                    e.Handled = false;
                else e.Handled = true;
            }
            if (((TextBox)sender).Text.Contains('б')) ((TextBox)sender).Text = ((TextBox)sender).Text.Replace('б', ',');
            InputTime.SelectionStart = InputTime.Text.Length;
        }
        private void AdditionTime(object sender, RoutedEventArgs e)
        {
            MainWindow window = this.Owner as MainWindow;
            if (InputTime.Text == null || InputTime.Text == "") inputTime = 0;
            else if (Double.TryParse(InputTime.Text, out inputTime)) ;
            else return;
            MainWindow.List[window.indexTimeAdd].Time = inputTime * 3600;
            MainWindow.List[window.indexTimeAdd].Timer = (MainWindow.List[window.indexTimeAdd].Time / 3600).ToString("F2") + " h";
            MainWindow.WriteInJson(MainWindow.jsonInfo);
            window.AllTime.Text = "All time today: " + (MainWindow.List.Sum(x => x.Time) / 3600).ToString("F2") + " h";
            this.Close();
        }
    }
}
