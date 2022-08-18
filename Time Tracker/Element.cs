using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class Element : INotifyPropertyChanged
    {
        //testcommecnt
        //third comment
        //fourth
        public string title;
        public string comment;
        public string timer;
        public double time;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }
        public string Timer
        {
            get { return timer; }
            set
            {
                timer = value;
                OnPropertyChanged("Timer");
            }
        }
        public double Time
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
