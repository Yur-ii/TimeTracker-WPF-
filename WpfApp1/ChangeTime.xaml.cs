using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ChangeTime.xaml
    /// </summary>
    public partial class ChangeTime : Window
    {
        double inputTime;
        public ChangeTime()
        {
            InitializeComponent();
        }
        public void InputNumb(object sender, KeyEventArgs e)
        {
            //if (e.Key < Key.D0 || e.Key > Key.D9)
            //    e.Handled = true;
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
            else inputTime = Double.Parse(InputTime.Text);
            MainWindow.List[window.indexTimeAdd].Time = inputTime * 3600;
            MainWindow.List[window.indexTimeAdd].Timer = (MainWindow.List[window.indexTimeAdd].Time / 3600).ToString("F2") + " h";
            MainWindow.WriteInJson(MainWindow.jsonInfo);
            window.AllTime.Text = "All time today: " + (MainWindow.List.Sum(x => x.Time) / 3600).ToString("F2") + " h";
            this.Close();

        }
        public void SubstractionTime(object sender, RoutedEventArgs e) {
        //    MainWindow window = this.Owner as MainWindow;
        //    if (InputTime.Text == null || InputTime.Text == "") inputTime = 0;
        //    else inputTime = Double.Parse(InputTime.Text);
        //    MainWindow.List[window.indexTimeAdd].Time -= inputTime * 3600;
        //    MainWindow.List[window.indexTimeAdd].Timer = (MainWindow.List[window.indexTimeAdd].Time / 3600).ToString("F2") + " h";
        //    MainWindow.WriteInJson(MainWindow.jsonInfo);
        //    window.AllTime.Text = "All time today: " + (MainWindow.List.Sum(x => x.Time) / 3600).ToString("F2") + " h";
        //    this.Close();
        }
    }
}
