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
using System.Windows.Threading;
using TelegramBot.BLL;


namespace TelegramBot.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TBot _tbot;
        private const string _token = "5149025176:AAF9ywvM1nXIkvpfKK4wV7Fsy8nTapirCDE";
        private List<string> _labels;
        private DispatcherTimer _timer;
        GroupBox _formVariant;
        GroupBox _formAnswer;


        private List<ListBox> _listOfListBox_Users = new List<ListBox>();

        private ListBox _userListBox;
        private string _tmp;


        public MainWindow()
        {
            _tbot = new TBot(_token, AddUsers);
            _labels = new List<string>();
            _labels.Add("sd");
            _labels.Add("ssf");
            InitializeComponent();

            ListBox_Users.ItemsSource = _labels;


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
            _timer.Start();
            _tbot.Start();
        }

        public void AddUsers(string s)
        {
            _labels.Add(s);
        }
        private void OnTick(object sender, EventArgs e)
        {
            ListBox_Users.Items.Refresh();
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            _tbot.Send(TextBox_Question.Text);
        }
        private void Button_AddGroup_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_UserGroups.Items.Add(TextBox_NameOfGroup.Text);

            _userListBox = new ListBox { Name = TextBox_NameOfGroup.Text };
            TabItem tmp = new TabItem { Header = new TextBlock { Text = TextBox_NameOfGroup.Text }, Content = _userListBox };

            _listOfListBox_Users.Add(_userListBox);

            ControlTab_UserGroup.Items.Add(tmp);
            TextBox_NameOfGroup.Text = "";

            tmp.Visibility = Visibility.Collapsed;
        }




        private void MenuItem_ClickDelete(object sender, RoutedEventArgs e)
        {

            if (ComboBox_UserGroups.SelectedIndex < 1 )
            {
                return;
            }

            int index = ComboBox_UserGroups.SelectedIndex;
            ComboBox_UserGroups.Items.RemoveAt(index);
            ControlTab_UserGroup.Items.RemoveAt(index);
        }

        private void MenuItem_ClickCut(object sender, RoutedEventArgs e)
        {

            if (ComboBox_UserGroups.SelectedIndex < 0 || _tmp != null)
            {
                return;
            }

            int index = ComboBox_UserGroups.SelectedIndex;
            int count = 0;
            
            if(index == 0)
            {

                for (int i = 0; i < ComboBox_UserGroups.Items.Count; i++)
                {
                    if(i == index)
                    {
                        _tmp = _labels[i];
                        _labels.RemoveAt(index);
                    }
                }
            }
            else
            {
                foreach (var userListBox in _listOfListBox_Users)
                {
                    count++;
                    if(index == count)
                    {
                        for(int i = 0; i < userListBox.Items.Count; i++)
                        {
                            _tmp = Convert.ToString(userListBox.Items[i]);
                            userListBox.Items.RemoveAt(userListBox.SelectedIndex);
 
                        }
                    }
                }
            }
        }


        private void MenuItem_ClickInsert(object sender, RoutedEventArgs e)
        {
            int index = ComboBox_UserGroups.SelectedIndex;
            int count = 0;

            if (ComboBox_UserGroups.SelectedIndex < 0 )
            {
                return;
            }
            
            if( index !=0)
            {

                foreach(var userListBox in _listOfListBox_Users)
                {
                    count++;
                    if(index == count)
                    {
                        if(_tmp != null)
                        {

                            userListBox.Items.Add(_tmp);
                            _tmp = null;
                        }
                    }
                }      
            }
            else
            {
                if(_tmp != null)
                {

                    _labels.Add(_tmp);
                    _tmp = null;
                }
            }
           
        }


        private void ComboBox_UserGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ComboBox_UserGroups.SelectedIndex;

            if (ControlTab_UserGroup != null)
            {

                ControlTab_UserGroup.SelectedIndex = index;
            }
        }

        private void RadioButton_Test_Click(object sender, RoutedEventArgs e)
        {
            GroupBox_Test.Visibility = Visibility.Visible;

            GroupBox_Poll.Visibility = Visibility.Hidden;

            ComboBox_ChooseToll.Text = "";

            HideExtraBoxes();
        }

        private void RadioButton_Poll_Click(object sender, RoutedEventArgs e)
        {
            GroupBox_Test.Visibility = Visibility.Hidden;

            GroupBox_Poll.Visibility = Visibility.Visible;

            ComboBox_ChooseTest.Text = "";

            HideExtraBoxes();
        }

        private void ComboBox_ChooseTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBox_ChoseTypeQuestion.Visibility = Visibility.Visible;
        }

        private void ComboBox_ChooseToll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBox_ChoseTypeQuestion.Visibility = Visibility.Visible;
        }

        private void ComboBox_ChooseQuestionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBox_Question.Visibility = Visibility.Visible;

            if (_formVariant is not null)
            {
                _formVariant.Visibility = Visibility.Hidden;
            }

            if (_formAnswer is not null)
            {
                _formAnswer.Visibility = Visibility.Hidden;
            }

            int tmp = ComboBox_ChooseQuestionType.SelectedIndex;

            if (tmp == 1 || tmp == 2 || tmp == 4)
            {
                GroupBox_AddVariants.Visibility = Visibility.Visible;
                _formVariant = GroupBox_AddVariants;
            }

            if (RadioButton_Test.IsChecked == true)
            {
                Label_TrueAnswer.Visibility = Visibility.Visible;

                if (tmp == 3)
                {
                    GroupBox_AnswerYesOrNo.Visibility = Visibility.Visible;
                    _formAnswer = GroupBox_AnswerYesOrNo;
                }
                else if (tmp == 0 || tmp == 1)
                {
                    GroupBox_TrueAnswer.Visibility = Visibility.Visible;
                    _formAnswer = GroupBox_TrueAnswer;
                }
                else
                {
                    GroupBox_AddTrueVarintsOrRigthOrder.Visibility = Visibility.Visible;
                    _formAnswer = GroupBox_AddTrueVarintsOrRigthOrder;
                }
            }
        }

        private void HideExtraBoxes()
        {
            GroupBox_ChoseTypeQuestion.Visibility = Visibility.Hidden;

            ComboBox_ChooseQuestionType.Text = "";

            GroupBox_Question.Visibility = Visibility.Hidden;

            TextBox_Question.Text = "";

            GroupBox_AddVariants.Visibility = Visibility.Hidden;

            TextBox_OneOrFewVariants.Text = "";

            GroupBox_AnswerYesOrNo.Visibility = Visibility.Hidden;

            RadioButton_Yes.IsChecked = false;

            RadioButton_No.IsChecked = false;

            GroupBox_AddTrueVarintsOrRigthOrder.Visibility = Visibility.Hidden;

            TextBox_OneOrFewTrueAnswers.Text = "";

            GroupBox_TrueAnswer.Visibility = Visibility.Hidden;

            TextBox_TrueAnswer.Text = "";

            Label_TrueAnswer.Visibility = Visibility.Hidden;
        }

    }
}
