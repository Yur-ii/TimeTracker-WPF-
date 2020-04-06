using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;


namespace WpfApp1
{
    public partial class ViewHistory : Window
    {
        public static List<string> dates = new List<string>();
        public static ObservableCollection<ViewData> viewInfoData = new ObservableCollection<ViewData>();
        public bool check = true;
        public double tim = 0;
        public string selectedItem;
        public int a = 0;
        public ViewHistory()
        {
            viewInfoData.Clear();
            InitializeComponent();
            MainWindow mn = this.Owner as MainWindow;
            foreach(KeyValuePair<string, ObservableCollection<Element>> data in MainWindow.jsonInfo) {
                foreach (string str in dates) {
                    if (str == data.Key.Substring(3, 7)) check = false;
                }
                if (check) dates.Add(data.Key.Substring(3,7));
                check = true;
            }
            dates.Reverse();
            dataList.ItemsSource = dates;
        }
        public void Select_Item_Combobox(object sender, RoutedEventArgs e) {
            tim = 0;
            viewInfoData.Clear();
            ComboBox comboBox = (ComboBox)sender;
            selectedItem = comboBox.SelectedItem.ToString();
            foreach (KeyValuePair<string, ObservableCollection<Element>> data in MainWindow.jsonInfo)
            {
                if (selectedItem == data.Key.Substring(3, 7)) {
                    foreach (Element el in data.Value)
                    {
                        viewInfoData.Add(new ViewData { Data = data.Key, Project = el.Title, Comment = el.Comment, Time = el.Timer });
                    }
                }
            }
            for (int i = 0; i < viewInfoData.Count; i++)
            {
                if (viewInfoData[i].Time != null)
                {
                    tim += Double.Parse(viewInfoData[i].Time.Substring(0, 4));
                }
            }
            TotalTime.Text = "Total time: " + tim + " h"; 
        }
    } 
}
