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
using TelegramBot.BL;
using TelegramBot.BL.DataBase;
using TelegramBot.BL.Questions;

namespace TelegramBot.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        private TBot _tbot;
        private const string _token = "5149025176:AAF9ywvM1nXIkvpfKK4wV7Fsy8nTapirCDE";
        private List<string> _labels;// test
        private DispatcherTimer _timer;
        GroupBox _formVariant;
        GroupBox _formAnswer;
        List<TypeOneVariant> _tryAnswers;
        List<TypeRightOrder> _typeRightOrder;
        List<TypeSeveralVariants> _typeSeveralVariants;
        List<TypeUserAnswer> _typeUserAnswer;
        List<TypeYesOrNo> _typeYesOrNo;

        TestsDatabase _dataBase;


        private List<ListBox> _listOfListBox_Users;
        private List<ListView> _listOfListView_ClasterQuestions;
        private ListView _listView_ClasterQuestions;
       
       
        private User _tmpUser;
        private string _tmpListView;

        
        public MainWindow()
        {
             _tbot = new TBot(_token, AddUsers);
            _labels = new List<string>();//test
            

            _listOfListBox_Users = new List <ListBox>();//test
            _listOfListView_ClasterQuestions = new List<ListView>();//test
            _tryAnswers = new List<TypeOneVariant>();
            _typeRightOrder = new List<TypeRightOrder>();
            _typeSeveralVariants = new List<TypeSeveralVariants>();
            _typeUserAnswer = new List<TypeUserAnswer>();
            _typeYesOrNo = new List<TypeYesOrNo>();

            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
            _timer.Start();
            _tbot.Start();
        }

        private void Window_MainWindow_Initialized_1(object sender, EventArgs e)
        {
            
            _dataBase = new TestsDatabase();

            ComboBox_UserGroups.SelectedIndex = 0;

            ListBox _userListBox = new ListBox();

            TabItem tmp = new TabItem { Header = new TextBlock { Text = TextBox_NameOfGroup.Text }, Content = _userListBox };

            _listOfListBox_Users.Add(_userListBox);

            ControlTab_UserGroup.Items.Add(tmp);
            TextBox_NameOfGroup.Clear();

            tmp.Visibility = Visibility.Hidden;

            ComboBox_UserGroups.Items.Add(_dataBase.UserGroups[0].NameGroup);

        }

        public void AddUsers(User newUser)
        {

             _dataBase.UserGroups[0].AddUser(newUser);
            
            
        }

       

        private void OnTick(object sender, EventArgs e)
        {
            int indexGroup = ComboBox_UserGroups.SelectedIndex;

            List<string> forUsers = new List<string>();

            foreach (User user in _dataBase.UserGroups[indexGroup].UserGroups)
            {
                forUsers.Add($"{user.Name} {user.Id}");
            }
            _listOfListBox_Users[indexGroup].ItemsSource = forUsers;

            
       

        }
       
        private void Button_AddGroup_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_NameOfGroup.Text =="" || TextBox_NameOfGroup.Text is null)
            {
                return;
            }
            _labels.Add("sd");//test
            DataGrid_SingleQuestions.ItemsSource = _labels;//test
            foreach (var item in ComboBox_UserGroups.Items)
            {
                string group = Convert.ToString(item);

                if (group == TextBox_NameOfGroup.Text)
                {
                    TextBox_NameOfGroup.Text = "Такая группа уже есть";

                    return;
                }
            }
            ComboBox_UserGroups.Items.Add(TextBox_NameOfGroup.Text);
            
            ListBox _userListBox = new ListBox();
            Group newGroup = new Group(TextBox_NameOfGroup.Text);
            _userListBox.ItemsSource = newGroup.UserGroups;
            TabItem tmp = new TabItem { Header = new TextBlock { Text = TextBox_NameOfGroup.Text }, Content = _userListBox };

            _listOfListBox_Users.Add(_userListBox);
            _dataBase.UserGroups.Add(newGroup);

            ControlTab_UserGroup.Items.Add(tmp);

            ListBox_UserGroups.Items.Add(TextBox_NameOfGroup.Text);

            TextBox_NameOfGroup.Clear();

            tmp.Visibility = Visibility.Collapsed;
        }
       

        private void MenuItem_ClickDelete(object sender, RoutedEventArgs e)
        {
            int index = ComboBox_UserGroups.SelectedIndex;
            int count = 0;

            if (ComboBox_UserGroups.SelectedIndex < 1)
            {
                return;
            }

            foreach (var userListBox in _listOfListBox_Users)
            {
                count++;
                if (index == count)
                {
                    foreach (var user in userListBox.Items)
                    {

                        _labels.Add(Convert.ToString(user));
                    }
                }
            }

            ComboBox_UserGroups.Items.RemoveAt(index);
            ControlTab_UserGroup.Items.RemoveAt(index);
            _listOfListBox_Users.RemoveAt(index - 1);
        }
       
        private void MenuItem_ClickCut(object sender, RoutedEventArgs e)
        {
            int indexGroup = ComboBox_UserGroups.SelectedIndex;
            int indexUser = _listOfListBox_Users[indexGroup].SelectedIndex;



            if (ComboBox_UserGroups.SelectedIndex < 0 || indexUser < 0 || _tmpUser is not null)
            {
                return;
            }

            
           _tmpUser = _dataBase.UserGroups[indexGroup].UserGroups[indexUser];
           _dataBase.UserGroups[indexGroup].DeleteUserById(_tmpUser.Id);


        }


        private void MenuItem_ClickInsert(object sender, RoutedEventArgs e)
        {
            int indexGroup = ComboBox_UserGroups.SelectedIndex;
            int indexUser = _listOfListBox_Users[indexGroup].SelectedIndex;

            if (ComboBox_UserGroups.SelectedIndex < 0 || _tmpUser is null)
            {
                return;
            }

            _dataBase.UserGroups[indexGroup].AddUser(_tmpUser);
            _tmpUser = null;

        }

        private void ComboBox_UserGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ComboBox_UserGroups.SelectedIndex;


            if (ControlTab_UserGroup != null)
            {

                ControlTab_UserGroup.SelectedIndex = index;
            }

        }

        private void ControlTab_UserGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ControlTab_UserGroup.SelectedIndex;

            if (ComboBox_UserGroups.Items == null)
            {

                ComboBox_UserGroups.SelectedIndex = index;
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
        private void RadioButtonEdit_Test_Click(object sender, RoutedEventArgs e)
        {
            GroupBox_TestEdit.Visibility = Visibility.Visible;

            GroupBox_PollEdit.Visibility = Visibility.Hidden;

            ComboBox_ChooseTollEdit.Text = "";

            HideExtraBoxes();
        }
        private void RadioButtonEdit_Poll_Click(object sender, RoutedEventArgs e) // моё
        {
            GroupBox_TestEdit.Visibility = Visibility.Hidden;

            GroupBox_PollEdit.Visibility = Visibility.Visible;

            ComboBox_ChooseTestEdit.Text = "";

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
        private void ComboBox_ChooseQuestionTypeEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBox_ChoseTypeQuestionEdit.Visibility = Visibility.Visible;
            GroupBox_QuestionEdit.Visibility = Visibility.Visible;
            Button_SaveChanges.Visibility = Visibility.Visible;
            Button_CancelChanges.Visibility = Visibility.Visible;

            if (_formVariant is not null)
            {
                _formVariant.Visibility = Visibility.Hidden;
            }

            if (_formAnswer is not null)
            {
                _formAnswer.Visibility = Visibility.Hidden;
            }

            int tmpToll = ComboBox_ChooseTollEdit.SelectedIndex;
            int tmpTest = ComboBox_ChooseTestEdit.SelectedIndex;

            if (tmpToll == 1 || tmpToll == 2 || tmpToll == 4 || tmpTest == 1 || tmpTest == 2 || tmpTest == 4)
            {
                GroupBox_AddVariantsEdit.Visibility = Visibility.Visible;
                _formVariant = GroupBox_AddVariantsEdit;
            }

            if (RadioButtonEdit_Test.IsChecked == true)
            {
                Label_TrueAnswerEdit.Visibility = Visibility.Visible;

                if (tmpTest == 3)
                {
                    GroupBox_AnswerYesOrNoEdit.Visibility = Visibility.Visible;
                    _formAnswer = GroupBox_AnswerYesOrNoEdit;
                }
                else if (tmpTest == 0 || tmpTest == 1)
                {
                    DataGrid_ChangeAnswers.Visibility = Visibility.Visible;

                }
                else
                {
                    DataGrid_ChangeAnswers.Visibility = Visibility.Visible;
                    //_formAnswer = GroupBox_AddTrueVarintsOrRigthOrderEdit;
                }
            }
            else
            {
                Label_TrueAnswerEdit.Visibility = Visibility.Hidden;
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

        private void DataGrid_ChangeAnswers_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItemEditСhange_ClickCut(object sender, RoutedEventArgs e)
        {
            int index = DataGrid_ChangeAnswers.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            DataGrid_ChangeAnswers.Items.RemoveAt(index);

        }

        private void MenuItemEditChange_ClickInsert(object sender, RoutedEventArgs e)
        {
            List <string> list = new List<string>();
            _tryAnswers.Add(new TypeOneVariant("", "", list));
            DataGrid_ChangeAnswers.Items.Add(_tryAnswers);
        }



        private void MenuItem_ClickCutListView_ClasterQuestions(object sender, RoutedEventArgs e)
        {
            int index = ListView_ClasterQuestions.SelectedIndex;

            if (index < 0)
            {
                return;
            }
            _tmpListView = (string)ListView_ClasterQuestions.SelectedItem;
            

            ListView_ClasterQuestions.Items.RemoveAt(index);
        }

        private void MenuItem_ClickInsertListView_ClasterQuestions(object sender, RoutedEventArgs e)
        {
            if (_tmpListView == null)
            {
                return;
            }

            ListView_ClasterQuestions.Items.Add(_tmpListView);
            _tmpListView = null;
        }

        private void MenuItem_ClickDeleteListView_ClasterQuestions(object sender, RoutedEventArgs e)
        {
            int index = ListView_ClasterQuestions.SelectedIndex;

            if (index < 0)
            {
                return;
            }
            ListView_ClasterQuestions.Items.RemoveAt(index);
        }

        private void MenuItem_ClickCutListView_SingleQuestions(object sender, RoutedEventArgs e)
        {
            int index = ListView_SingleQuestions.SelectedIndex;

            if (index < 0)
            {
                return;
            }

            _tmpListView = (string)ListView_SingleQuestions.SelectedItem;


            ListView_SingleQuestions.Items.RemoveAt(index);

        }

        private void MenuItem_ClickInsertListView_SingleQuestions(object sender, RoutedEventArgs e)
        {

            if (_tmpListView == null)
            {
                return;
            }

            ListView_SingleQuestions.Items.Add(_tmpListView);
            _tmpListView = null;
        }

        private void MenuItem_ClickDeleteListView_SingleQuestions(object sender, RoutedEventArgs e)
        {
            int index = ListView_SingleQuestions.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            ListView_SingleQuestions.Items.RemoveAt(index);
        }

        private void MenuItem_ClickDeleteComboBox_Claster (object sender, RoutedEventArgs e)
        {
            int index = ComboBox_Claster.SelectedIndex;

            if (index <= 0)
            {
                return;
            }

            ComboBox_Claster.Items.RemoveAt(index);
        }

        private void ComboBox_Claster_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ComboBox_Claster.SelectedIndex;


            if (TabControll_ClasterQuestions != null)
            {

                TabControll_ClasterQuestions.SelectedIndex = index;
            }
        }

       

        private void TextBox_ClasterName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _listView_ClasterQuestions= new ListView();
                _listView_ClasterQuestions = ListView_ClasterQuestions;
                ComboBox_Claster.Items.Add(TextBox_ClasterName.Text);
                TextBox_ClasterName.Clear();

                 TabItem tmp = new TabItem { Header = new TextBlock { Text = TextBox_ClasterName.Text }, Content = _listView_ClasterQuestions };
                _listOfListView_ClasterQuestions.Add(_listView_ClasterQuestions);

                TabControll_ClasterQuestions.Items.Add(tmp);
                
                tmp.Visibility = Visibility.Collapsed;
            }

        }


        private void Button_SendToBot_Click(object sender, RoutedEventArgs e)
        {
            var question = DataGrid_SingleQuestions.SelectedItem;
            int indexGroup = ListBox_UserGroups.SelectedIndex;

            if(question is null || indexGroup == -1)
            {
                return;
            }

            foreach(User user in _dataBase.UserGroups[indexGroup].UserGroups)
            {
                _tbot.Send((string)question, user.Id);
                    
            }
            DataGrid_SingleQuestions.SelectedItem = null;
            ListBox_UserGroups.SelectedIndex = -1;
        }
    }
}