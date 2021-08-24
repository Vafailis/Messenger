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
    /// Логика взаимодействия для MessageAnother.xaml
    /// </summary>
    public partial class MessageAnother : UserControl
    {
        public MessageAnother()
        {
            InitializeComponent();
        }
        public MessageAnother(string Another_Message_Name, string Another_Message_Time, string Another_Message_Text)
        {
            InitializeComponent();
            this.Another_Message_Name.Text = Another_Message_Name;
            this.Another_Message_Time.Text = Another_Message_Time;
            this.Another_Message_Text.Text = Another_Message_Text;
        }
    }
}
