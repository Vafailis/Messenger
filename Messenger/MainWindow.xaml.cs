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
using LiteDB;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private My_Account Me;
        private User interlocutor;

        public MainWindow()
        {
            InitializeComponent();
            interlocutor = new User();
            DrawSymbols();
            SetMyAccount("Zhan0_0", @"pack://application:,,/Images/вжух.jpg");
            SetUsers_InForm(SetUsers_From_DataBase());

        }

        private void DrawSymbols()
        {
            My_Profile_Settings_btn.Content = "\u22ee";
        }

        private void SetMyAccount(string Name, string Avatar_Path)
        {
            Me = new My_Account(Name, Avatar_Path);
            My_Name.Text = Me.Name;
        }

        private void SetUsers_InForm(List<DataUser> dataUsers)
        {
            UsersMenu.Items.Add(new Separator());
            int i = 0;
            foreach (DataUser dataUser in dataUsers)
            {
                User newUser = new User(dataUser.User_Name, dataUser.Avatar_Path, dataUser.Status);
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = newUser;
                listBoxItem.Selected += ListBoxItem_Selected;
                listBoxItem.Margin = new Thickness(1);
                if (i == 0) listBoxItem.IsSelected = true;
                UsersMenu.Items.Add(listBoxItem);
                UsersMenu.Items.Add(new Separator()); i++;
            }
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            if (sender is ListBoxItem obj)
            {
                Chat.Children.Clear();
                interlocutor = (User)obj.Content;
                Interlocutor_Name.Text = "@ "+ interlocutor.User_Name.Text;

                GetMesseges_From_DataBase();
            }
        }

        private void GetMesseges_From_DataBase()
        {
            using (var db = new LiteDatabase(@"DATABASE.db"))
            {
                var col = db.GetCollection<DataMessege>(interlocutor.User_Name.Text);
                var result = col.FindAll();
                foreach(DataMessege messegeDB in result)
                {
                    if (messegeDB.IsMine)
                    {
                        MessageMe myMsg = new MessageMe(Me.Name, messegeDB.Time, messegeDB.Messege_Text);
                        Chat.Children.Add(myMsg);
                        ChatScroll.ScrollToBottom();
                    }
                    else
                    {
                        MessageAnother anotherMsg = new MessageAnother(messegeDB.User_Name, messegeDB.Time, messegeDB.Messege_Text);
                        Chat.Children.Add(anotherMsg);
                        ChatScroll.ScrollToBottom();
                    }
                }
            }
        }


        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (My_Message.Text != string.Empty)
            {
                string Now = DateTime.Now.Hour.ToString();
                Now += ":" + DateTime.Now.Minute.ToString();
                MessageMe my_msg = new MessageMe(My_Name.Text, Now, My_Message.Text);
                Chat.Children.Add(my_msg);
                MessageAnother another_msg = new MessageAnother(interlocutor.User_Name.Text, Now, My_Message.Text);
                Chat.Children.Add(another_msg);
                ChatScroll.ScrollToBottom();
                My_Message.Text = string.Empty;
                Save_Messeges_In_DataBase(my_msg);
                Save_Messeges_In_DataBase(another_msg);

            }
        }

        private void Save_Messeges_In_DataBase(UserControl messege)
        {
            DataMessege element;
            if (messege is MessageMe myMessege)
            {
                element = new DataMessege(myMessege.My_Message_Text.Text, myMessege.My_Message_Time.Text,true);
                SetMesseges_In_DataBase(element);
            }
            else if(messege is MessageAnother messageAnother)
            {
                element = new DataMessege(messageAnother.Another_Message_Name.Text,messageAnother.Another_Message_Text.Text, messageAnother.Another_Message_Time.Text, false);
                SetMesseges_In_DataBase(element);
            }
        }

        private void SetMesseges_In_DataBase(DataMessege messege)
        {
            using (var db = new LiteDatabase("DATABASE.db"))
            {
                var col = db.GetCollection<DataMessege>(interlocutor.User_Name.Text);
                col.Insert(messege);
            }
        }

        private List<DataUser> SetUsers_From_DataBase()
        {
            using (var db = new LiteDatabase(@"DATABASE.db"))
            {
                var col = db.GetCollection<DataUser>("Users");
                //col.Insert(new DataUser("Human_1", "pack://application:,,/Images/abstraction.jpg", true));
                //col.Insert(new DataUser("Human_2", "pack://application:,,/Images/egle.jpg", false));
                //col.Insert(new DataUser("Bot", "pack://application:,,/Images/eye.jpg", true));
                //col.Insert(new DataUser("Human_3", "pack://application:,,/Images/grogu.jpg", false));
                //col.Insert(new DataUser("Human_22", "pack://application:,,/Images/упоротый.jpg", true));
                //col.Insert(new DataUser("Human_222", "pack://application:,,/Images/standart_avatar.png", true));
                var result = col.FindAll();
                List<DataUser> Users = new List<DataUser>();
                foreach (DataUser user in result)
                    Users.Add(user);
                return Users;
            }
        }

        private List<DataUser> GetOnlineUsers_From_DataBase()
        {
            List<DataUser> Users;
            using (var db = new LiteDatabase(@"DATABASE.db"))
            {
                var col = db.GetCollection<DataUser>("Users");
                var result = col.FindAll();
                Users = new List<DataUser>();
                foreach (DataUser user in result)
                {
                    if (user.Status == true)
                        Users.Add(user);
                }
            }
            return Users;
        }

        private void Sort_ByAll_Click(object sender, RoutedEventArgs e)
        {
            UsersMenu.Items.Clear();
            SetUsers_InForm(SetUsers_From_DataBase());
        }

        private void Sort_ByOnline_Click(object sender, RoutedEventArgs e)
        {
            UsersMenu.Items.Clear();
            SetUsers_InForm(GetOnlineUsers_From_DataBase());
        }
    }

    class DataUser
    {
        public string User_Name { set; get; }
        public string Avatar_Path { set; get; }
        public bool Status { set; get; }

        public DataUser(string User_Name, string Avatar_Path, bool Status)
        {
            this.User_Name = User_Name;
            this.Avatar_Path = Avatar_Path;
            this.Status = Status;
        }
    }

    class DataMessege
    {
        public string User_Name { set; get; }
        public string Messege_Text { set; get; }
        public string Time { set; get; }
        public bool IsMine { set; get; }

        public DataMessege(string Messege_Text, string Time, bool IsMine)
        {
            this.Messege_Text = Messege_Text;
            this.Time = Time;
            this.IsMine = IsMine;
        }

        public DataMessege(string User_Name, string Messege_Text, string Time, bool IsMine)
        {
            this.User_Name = User_Name;
            this.Messege_Text = Messege_Text;
            this.Time = Time;
            this.IsMine = IsMine;
        }
    }
}
