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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        public bool StatusValue { get; }

        public User()
        {
            InitializeComponent();
        }

        public User(string User_Name, string Avatar_Path, bool Status)
        {
            InitializeComponent();

            this.User_Name.Text = User_Name;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(Avatar_Path);
            bitmap.EndInit();
            this.Avatar.ImageSource = bitmap;
            StatusValue = Status;

            if (StatusValue == true)
            {
                this.Status.Text = "online";
                this.Status.Foreground = Brushes.GreenYellow;
            }
            else
            {
                this.Status.Text = "offline";
                this.Status.Foreground = Brushes.LightBlue;
            }
        }
    }
}
