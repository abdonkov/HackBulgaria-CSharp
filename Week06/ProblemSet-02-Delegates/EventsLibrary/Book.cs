using System.ComponentModel;

namespace EventsLibrary
{
    public class Book : INotifyPropertyChanged
    {
        private string author;
        private string name;
        private int year;
        public string Author
        {
            get { return author; }
            set
            {
                if (object.Equals(author, value))
                {
                    author = value;
                    NotifyPropertyChanged(this, "Author");
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (object.Equals(name, value))
                {
                    name = value;
                    NotifyPropertyChanged(this, "Name");
                }
            }
        }
        public int Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    NotifyPropertyChanged(this, "Year");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Book(string author, string name, int year)
        {
            this.author = author;
            this.name = name;
            this.year = year;
        }

        public Book(string author, string name, int year, PropertyChangedEventHandler propertyChanged)
        {
            this.author = author;
            this.name = name;
            this.year = year;
            PropertyChanged = propertyChanged;
        }

        private void NotifyPropertyChanged(object sender, string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return string.Concat("Book[Author: ", author, ", Name: ", name, ", Year: ", year, "]");
        }
    }
}
