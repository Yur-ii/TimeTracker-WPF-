using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace WpfApp1
{
    public class ViewData : INotifyPropertyChanged
    {
        public string data;
        public string project;
        public string comment;
        public string time;
        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged("Title");
            }
        }
        public string Project
        {
            get { return project; }
            set
            {
                project = value;
                OnPropertyChanged("Comment");
            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Timer");
            }
        }
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Timer");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
