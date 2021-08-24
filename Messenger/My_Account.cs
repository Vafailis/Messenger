using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    class My_Account
    {
        public string Name { get; }
        public BitmapImage Avatar_path { get; }


        public My_Account(string Name, string Avatar_path)
        {
            this.Name = Name;
            this.Avatar_path = new BitmapImage();

            this.Avatar_path.BeginInit();
            this.Avatar_path.UriSource = new Uri(Avatar_path);
            this.Avatar_path.EndInit();
        }
    }
}
