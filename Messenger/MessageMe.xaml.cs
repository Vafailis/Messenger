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
    /// Логика взаимодействия для MessageMe.xaml
    /// </summary>
    public partial class MessageMe : UserControl
    {
        public MessageMe()
        {
            InitializeComponent();
        }
        public MessageMe(string My_Message_Name, string My_Message_Time, string My_Message_Text)
        {
            InitializeComponent();
            this.My_Message_Name.Text = My_Message_Name;
            this.My_Message_Time.Text = My_Message_Time;
            this.My_Message_Text.Text = My_Message_Text;
        }
    }
}
