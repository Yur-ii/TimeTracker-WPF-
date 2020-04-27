using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.ComponentModel;
using System.Text.Json;
using System.IO;

namespace WpfApp1
{
    
    public partial class MainWindow : Window
    {
        //ChangeTime wind;
        public static ObservableCollection<Element> List { get; set; }
        public static ObservableCollection<string> Source { get; set; }
        public static Dictionary<string, ObservableCollection<Element>> jsonInfo = new Dictionary<string, ObservableCollection<Element>>();
        public static string date = DateTime.Today.ToString("d");
        public int indexButton = 0;
        public bool changeImg = true;
        public int indexTimeAdd = 0;
        public static bool checkDate = false;
        int checkButton = 0;
        public static string readJson;
        DispatcherTimer dt = new DispatcherTimer();
        static MainWindow() {
            List = new ObservableCollection<Element>() {
                new Element()
            };

            if (!File.Exists("test.json"))
            {
                checkDate = true;
                jsonInfo.Add(date, List);
                WriteInJson(jsonInfo);
            }
            else
            {
                ReadFromJson();
            }
            Source = new ObservableCollection<string>() { null };
            if (!File.Exists("projects.json"))
            {
                WriteInJsonProject(Source);
            }
            else
            {
                ReadFromJsonProject();
            }
        }
        
        public MainWindow()
        {
            InitializeComponent();
            Uri iconUri = new Uri(Directory.GetCurrentDirectory() + "/start.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
        void Closing_Window(object sender, CancelEventArgs e) {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            ViewProject.ItemsSource = Source;
            AllTime.Text = "All time today: " + (List.Sum(x => x.Time) / 3600).ToString("F2") + " h";
        }
        public void Button_Start(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = Int32.Parse(button.Tag.ToString());
            if (!checkDate)
            {
                jsonInfo.Add(date, List);
                File.WriteAllText("test.json", string.Empty);
                WriteInJson(jsonInfo);
                checkDate = true;
            }
            if (changeImg)
            {
                Uri iconUri = new Uri(Directory.GetCurrentDirectory() + "/stop.png", UriKind.RelativeOrAbsolute);
                this.Icon = BitmapFrame.Create(iconUri);
                indexButton = index;
                checkButton = Convert.ToInt32(button.Tag);
                button.Background = Brushes.SaddleBrown;
                button.Content = "Stop";
                dt.Start();
                changeImg = false;
            }
            else if (!changeImg && (checkButton == Convert.ToInt32(button.Tag)))
            {
                Uri iconUri = new Uri(Directory.GetCurrentDirectory() + "/start.png", UriKind.RelativeOrAbsolute);
                this.Icon = BitmapFrame.Create(iconUri);
                button.Background = Brushes.DarkGreen;
                button.Content = "Start";
                dt.Stop();
                changeImg = true;
                jsonInfo[date] = List;
                File.WriteAllText("test.json", string.Empty);
                WriteInJson(jsonInfo);
            }
        }
        private void dtTicker(object sender, EventArgs e) {
            ++List[indexButton].Time;
            List[indexButton].Timer = (List[indexButton].Time / 3600).ToString("F2") + " h";
            AllTime.Text = "All time today: " + (List.Sum(x => x.Time) / 3600).ToString("F2") + " h";
            //jsonInfo[date] = List;
            //File.WriteAllText("test.json", string.Empty);
            //WriteInJson(jsonInfo);
        }
        public string CurrentPath() {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            currentPath = currentPath.Remove(currentPath.Length - 10, 10);
            while (currentPath.IndexOf('\\') != -1) {
                currentPath = currentPath.Replace("\\", "/");
            }
            return currentPath;
        }
        public void CreateNewLine_Click(object sender, RoutedEventArgs e)
        {
            List.Add(new Element());
        }
        public void Create_Project(object sender, RoutedEventArgs e)
        {
            Source.Add($"{ProjectName.Text}");
            Source.Remove(null);
            Source.Remove("");
            File.WriteAllText("projects.json", string.Empty);
            WriteInJsonProject(Source);
            ProjectName.Text = "";
        }
        public void Delete_Proj(object sender, RoutedEventArgs e)
        {
            Source.Remove(ViewProject.SelectedItem.ToString());
            File.WriteAllText("projects.json", string.Empty);
            WriteInJsonProject(Source);
        }
        public void Delete_Row(object sender, RoutedEventArgs e)
        {
            List.RemoveAt(TimerGrid.SelectedIndex);
        }
        public static void WriteInJson(Dictionary<string, ObservableCollection<Element>> jsonInf)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            using (StreamWriter fs = new StreamWriter("test.json", false, System.Text.Encoding.Default))
            {
                readJson = JsonSerializer.Serialize<Dictionary<string, ObservableCollection<Element>>>(jsonInf, options);
                fs.Write(readJson);
                //await JsonSerializer.SerializeAsync<Dictionary<string, ObservableCollection<Element>>>(fs, jsonInf, options);
            }
        }
        public static void ReadFromJson()
        {
            using (StreamReader fs = new StreamReader("test.json"))
            {
                readJson = fs.ReadToEnd();
                jsonInfo = JsonSerializer.Deserialize<Dictionary<string, ObservableCollection<Element>>>(readJson);

            }
            foreach (KeyValuePair<string, ObservableCollection<Element>> keyVal in jsonInfo)
            {
                if (keyVal.Key == date) { List = keyVal.Value;
                    checkDate = true;
                }
            }
        }
        public static void WriteInJsonProject(ObservableCollection<string> jsonInf)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            using (StreamWriter fp = new StreamWriter("projects.json", false, System.Text.Encoding.Default))
            {
                readJson = JsonSerializer.Serialize<ObservableCollection<string>>(jsonInf, options);
                fp.Write(readJson);
            }
        }
        public static void ReadFromJsonProject()
        {
            using (StreamReader fp = new StreamReader("projects.json"))
            {
                
                readJson = fp.ReadToEnd();
                Source = JsonSerializer.Deserialize<ObservableCollection<string>>(readJson);
            }
        }
        public void Add_Time(object sender, RoutedEventArgs e) {
            Button butt = sender as Button;
            indexTimeAdd = Int32.Parse(butt.Tag.ToString());
            ChangeTime wind = new ChangeTime();
            wind.Owner = this;
            wind.ShowDialog();
        }
        public void View_History(object sender, RoutedEventArgs e) {
            ViewHistory view = new ViewHistory();
            view.Owner = this;
            view.ShowDialog();
        }
    }
}

