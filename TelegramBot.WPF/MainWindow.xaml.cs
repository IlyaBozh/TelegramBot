using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telegram.Bot.Types.ReplyMarkups;
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

        TestsDataBase _testsDataBase;
        UsersDataBase _usersDataBase;


        private List<ListBox> _listOfListBox_Users;
        private List<ListView> _listOfListView_ClasterQuestions;
        private ListView _listView_ClasterQuestions;


        private User _tmpUser;
        private string _tmpListView;

        ContextMenu testMenu = new ContextMenu();


        public MainWindow()
        {
             _tbot = new TBot(_token, AddUsers);
            _labels = new List<string>();//test 
            _listOfListBox_Users = new List<ListBox>();//test
            _listView_ClasterQuestions = new ListView(); //test
            _listOfListView_ClasterQuestions = new List<ListView>();//test

            InitializeComponent();

            _tryAnswers = new List<TypeOneVariant>();
            _typeRightOrder = new List<TypeRightOrder>();
            _typeSeveralVariants = new List<TypeSeveralVariants>();
            _typeUserAnswer = new List<TypeUserAnswer>();
            _typeYesOrNo = new List<TypeYesOrNo>();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
            _timer.Start();
            _tbot.Start();
        }

        private void Window_MainWindow_Initialized_1(object sender, EventArgs e)
        {
            _testsDataBase = TestsDataBase.GetInstance();
            _usersDataBase = UsersDataBase.GetInstance();
            _usersDataBase.UserGroups = _usersDataBase.Load();

            List<List<AbstractQuestion>> loadData = _testsDataBase.LoadSingel();
            List<List<Claster>> loadClasters = _testsDataBase.LoadClaster();

            _testsDataBase.TestSingelQuestions = loadData[0];
            _testsDataBase.PollSingelQuestions = loadData[1];
            _testsDataBase.Tests = loadClasters[0];
            _testsDataBase.Polls = loadClasters[1];


            ComboBox_UserGroups.SelectedIndex = 0;

            ListBox _userListBox = new ListBox();

            for(int i = 0; i < _usersDataBase.UserGroups.Count; i++)
            {
                 TabItem tmp = new TabItem { Header = new TextBlock { Text = TextBox_NameOfGroup.Text }, Content = _userListBox };

                 _listOfListBox_Users.Add(_userListBox);

                 ControlTab_UserGroup.Items.Add(tmp);
                 TextBox_NameOfGroup.Clear();

                 tmp.Visibility = Visibility.Hidden;
            }

            ComboBox_UserGroups.Items.Add(_usersDataBase.UserGroups[0].NameGroup);
        }

        public void AddUsers(User newUser)
        {

            _usersDataBase.UserGroups[0].AddUser(newUser);


        }



        private void OnTick(object sender, EventArgs e)
        {
            int indexGroup = ComboBox_UserGroups.SelectedIndex;

            List<string> forUsers = new List<string>();

            foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
            {
                forUsers.Add($"{user.Name} {user.Id}");
            }
            _listOfListBox_Users[indexGroup].ItemsSource = forUsers;

            ControlTab_UserGroup.Items.Refresh();
           


        }

        private void Button_AddGroup_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_NameOfGroup.Text == "" || TextBox_NameOfGroup.Text is null)
            {
                return;
            }
            
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
            _usersDataBase.UserGroups.Add(newGroup);

            ControlTab_UserGroup.Items.Add(tmp);

            ListBox_UserGroups.Items.Add(TextBox_NameOfGroup.Text);

            TextBox_NameOfGroup.Clear();

            tmp.Visibility = Visibility.Collapsed;
        }


        private void MenuItem_ClickDelete(object sender, RoutedEventArgs e)
        {
            int index = ComboBox_UserGroups.SelectedIndex;


            if (ComboBox_UserGroups.SelectedIndex < 1)
            {
                return;
            }

            ComboBox_UserGroups.Items.RemoveAt(index);
            ControlTab_UserGroup.Items.RemoveAt(index);
            _listOfListBox_Users.RemoveAt(index - 1);
            _usersDataBase.UserGroups.RemoveAt(index);
        }

        private void MenuItem_ClickCut(object sender, RoutedEventArgs e)
        {
            int indexGroup = ComboBox_UserGroups.SelectedIndex;
            int indexUser = _listOfListBox_Users[indexGroup].SelectedIndex;



            if (ComboBox_UserGroups.SelectedIndex < 0 || indexUser < 0 || _tmpUser is not null)
            {
                return;
            }


            _tmpUser = _usersDataBase.UserGroups[indexGroup].UserGroups[indexUser];
            _usersDataBase.UserGroups[indexGroup].DeleteUserById(_tmpUser.Id);


        }


        private void MenuItem_ClickInsert(object sender, RoutedEventArgs e)
        {
            int indexGroup = ComboBox_UserGroups.SelectedIndex;
            int indexUser = _listOfListBox_Users[indexGroup].SelectedIndex;

            if (ComboBox_UserGroups.SelectedIndex < 0 || _tmpUser is null)
            {
                return;
            }

            _usersDataBase.UserGroups[indexGroup].AddUser(_tmpUser);
            _tmpUser = null;

        }

        private void ComboBox_UserGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ComboBox_UserGroups.SelectedIndex;

            if (index == -1)
            {
                ComboBox_UserGroups.SelectedIndex = 0;

            }

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


        private void ComboBox_ChooseTestOrPoll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBox_ChoseTypeQuestion.Visibility = Visibility.Visible;
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

            ///ТУТ ЕБАТЬ ТЕСТЫ ТЕСТОВ
        }



        private void HideExtraBoxes()
        {
            GroupBox_ChoseTypeQuestion.Visibility = Visibility.Hidden;

            ComboBox_ChooseQuestionType.Text = "";

            Button_AddQuestion.Visibility = Visibility.Hidden;

            GroupBox_Question.Visibility = Visibility.Hidden;

            TextBox_Question.Text = "";

            GroupBox_AddVariants.Visibility = Visibility.Hidden;

            TextBox_OneOrFewVariants.Text = "";

            GroupBox_AnswerYesOrNo.Visibility = Visibility.Hidden;

            RadioButton_Yes.IsChecked = false;

            RadioButton_No.IsChecked = false;

            GroupBox_AddTrueVarintsOrRigthOrder.Visibility = Visibility.Hidden;

            ListBox_Variants.Items.Clear();

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
            List<string> list = new List<string>();
            _tryAnswers.Add(new TypeOneVariant("", "", list));
            DataGrid_ChangeAnswers.Items.Add(_tryAnswers);
        }


        private void Button_SendToBot_Click(object sender, RoutedEventArgs e)
        {
            AbstractQuestion abstractQuestion = (AbstractQuestion) DataGrid_SingleQuestions.SelectedItem;
            string question;
            List<string> answers= new List<string>();
            int indexGroup = ListBox_UserGroups.SelectedIndex;

            if( indexGroup == -1)
            {
                return;
            }


            if (1 == ComboBox_QuestionContainer.SelectedIndex)
            {
                question = abstractQuestion.Description;

                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {
                    
                   
                    _tbot.Send((string)question, user.Id);
                }
            }



            if (2 == ComboBox_QuestionContainer.SelectedIndex)
            {
                question = abstractQuestion.Description;
                answers = abstractQuestion.Variants;


                var buttons = answers.Select(answers => new[] { new KeyboardButton(answers) })
                    .ToArray();
                var replyMarkup = new ReplyKeyboardMarkup(buttons);

                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {

                    replyMarkup.OneTimeKeyboard = true;
                    _tbot.Send((string)question, user.Id, replyMarkup);
                }

            }

            if (3 == ComboBox_QuestionContainer.SelectedIndex)
            {
                question = abstractQuestion.Description;
                answers = abstractQuestion.Variants;

                var buttons = answers.Select(answers => new[] { new KeyboardButton(answers) })
                    .ToArray();
                var replyMarkup = new ReplyKeyboardMarkup(buttons);

                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {

                    replyMarkup.OneTimeKeyboard = true;
                    _tbot.Send((string)question, user.Id, replyMarkup);
                }
            }

            if (4 == ComboBox_QuestionContainer.SelectedIndex)
            {
                question = abstractQuestion.Description;

                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {
                    
                    ReplyKeyboardMarkup replyMarkup = new ReplyKeyboardMarkup(
                    new[]
                    {
                     new[]
                     {
                          new KeyboardButton("DA")
                     },

                     new[]
                     {
                        new KeyboardButton("NET")
                     },

                    }
                    );

                    replyMarkup.OneTimeKeyboard = true;
                    _tbot.Send((string)question, user.Id, replyMarkup);
                }


            }

            if (5 == ComboBox_QuestionContainer.SelectedIndex)
            {
                question = abstractQuestion.Description;
                answers = abstractQuestion.Variants;

                var buttons = answers.Select(answers => new[] { new KeyboardButton(answers) })
                    .ToArray();
                var replyMarkup = new ReplyKeyboardMarkup(buttons);

                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {
                    
                    replyMarkup.OneTimeKeyboard = true;
                    _tbot.Send((string)question, user.Id, replyMarkup);
                }
            }

            DataGrid_SingleQuestions.SelectedItem = null;
            ListBox_UserGroups.SelectedIndex = -1;

        }





        private void RadioButton_PollContainer_Checked(object sender, RoutedEventArgs e)
        {
           
            DataGrid_SingleQuestions.ItemsSource = _testsDataBase.TestSingelPolls;

            for(int i = 1; i < DataGrid_SingleQuestions.Columns.Count; i++)
            {
                DataGrid_SingleQuestions.Columns[i].MaxWidth = 0;
            }
                
            ComboBox_QuestionContainer.SelectedIndex = 0;
        }

        private void RadioButton_TestContainer_Checked(object sender, RoutedEventArgs e)
        {

            DataGrid_SingleQuestions.ItemsSource = _testsDataBase.TestSingelQuestions;

            for (int i = 1; i < DataGrid_SingleQuestions.Columns.Count; i++)
            {
                DataGrid_SingleQuestions.Columns[i].MaxWidth = 0;
            }
            ComboBox_QuestionContainer.SelectedIndex = 0;
        }

        private void RadioButton_Test_Checked(object sender, RoutedEventArgs e)
        {
            Label_TestOrPoll.Content = "Тест:";
            

            foreach (Claster test in _testsDataBase.Tests)
            {
                ComboBox_ChooseTestOrPoll.Items.Add(test.NameClaster);
            }

            foreach (Claster poll in _testsDataBase.Polls)
            {
                ComboBox_ChooseTestOrPoll.Items.Remove(poll.NameClaster);
            }
        }

        private void RadioButton_Poll_Checked(object sender, RoutedEventArgs e)
        {
            Label_TestOrPoll.Content = "Опрос:";
           

            foreach (Claster poll in _testsDataBase.Polls)
            {
                ComboBox_ChooseTestOrPoll.Items.Add(poll.NameClaster);
            }

            foreach (Claster test in _testsDataBase.Tests)
            {
                ComboBox_ChooseTestOrPoll.Items.Remove(test.NameClaster);
            }
        }


        private void RadioButton_Test_Click(object sender, RoutedEventArgs e)
        {
            GroupBox_Test.Visibility = Visibility.Visible;

            ComboBox_ChooseTestOrPoll.Text = "";

            HideExtraBoxes();

          
        }
        private void RadioButton_Poll_Click(object sender, RoutedEventArgs e)
        {
            GroupBox_Test.Visibility = Visibility.Visible;

            ComboBox_ChooseTestOrPoll.Text = "";

            HideExtraBoxes();
        }


        #region Add Question in TAB CreateQuestion
        private void Button_AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckBoxes())
            {
                return;
            }

            if (ComboBox_ChooseTestOrPoll.SelectedIndex == 0)
            {
                if(RadioButton_Test.IsChecked == true)
                {
                    _testsDataBase.TestSingelQuestions.Add(GetQuestionWhithAnswer());
                }
                else
                {
                    _testsDataBase.PollSingelQuestions.Add(GetQuestionWhithoutAnswer());
                }
            }
            else
            {
                if (RadioButton_Test.IsChecked == true)
                {
                    _testsDataBase.Tests[ComboBox_ChooseTestOrPoll.SelectedIndex].Add(GetQuestionWhithAnswer());
                }
                else
                {
                    _testsDataBase.Polls[ComboBox_ChooseTestOrPoll.SelectedIndex].Add(GetQuestionWhithoutAnswer());
                }
            }
            
            ClearBoxes();
        }

        private void ComboBox_ChooseQuestionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBox_Question.Visibility = Visibility.Visible;
            Button_AddQuestion.Visibility = Visibility.Visible;

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
                GroupBox_AddTrueVarintsOrRigthOrder.Visibility = Visibility.Visible;
                _formAnswer = GroupBox_AddTrueVarintsOrRigthOrder;
                _formVariant = GroupBox_AddVariants;
                ListBox_RightOrder.Visibility = Visibility.Hidden;
            }

            if (RadioButton_Test.IsChecked == true)
            {
                Label_TrueAnswer.Visibility = Visibility.Visible;

                if (tmp == 3)
                {
                    GroupBox_AnswerYesOrNo.Visibility = Visibility.Visible;
                    _formAnswer = GroupBox_AnswerYesOrNo;
                }
                else if (tmp == 0)
                {
                    GroupBox_TrueAnswer.Visibility = Visibility.Visible;
                    _formAnswer = GroupBox_TrueAnswer;
                }

                if (tmp == 4)
                {
                    ListBox_RightOrder.Visibility = Visibility.Visible;
                }
                else
                {
                    ListBox_RightOrder.Visibility = Visibility.Hidden;
                }
            }
            ClearBoxes();
        }

        private void ClearBoxes()
        {
            TextBox_Question.Text = "";
            TextBox_OneOrFewVariants.Text = "";
            TextBox_TrueAnswer.Text = "";
            ListBox_Variants.Items.Clear();
            ListBox_RightOrder.Items.Clear();
            RadioButton_No.IsChecked = false;
            RadioButton_Yes.IsChecked = false;
        }

        private AbstractQuestion GetQuestionWhithoutAnswer()
        {
            List<string> tmp_1 = new List<string>();

            foreach (string variant in ListBox_Variants.Items)
            {
                tmp_1.Add(variant);
            }

            switch (ComboBox_ChooseQuestionType.SelectedIndex)
            {
                case 0:
                    return new TypeUserAnswer(TextBox_Question.Text);
                case 1:
                    return new TypeOneVariant(TextBox_Question.Text, tmp_1);
                case 2:
                    return new TypeSeveralVariants(TextBox_Question.Text, tmp_1);
                case 3:
                    return new TypeYesOrNo(TextBox_Question.Text);
                case 4:
                    return new TypeRightOrder(TextBox_Question.Text, tmp_1);
                default:
                    return new TypeRightOrder("adwad", tmp_1);
            }
        }

        private AbstractQuestion GetQuestionWhithAnswer()
        {
            List<string> tmp_1 = new List<string>();
            List<string> tmp_2 = new List<string>();
            string tmp_3 = RadioButton_Yes.IsChecked == true ? "Да" : "Нет";

            if(ComboBox_ChooseQuestionType.SelectedIndex == 1)
            {
                foreach (RadioButton variant in ListBox_Variants.Items)
                {
                    TextBox_TrueAnswer.Text = variant.IsChecked == true ? (string)variant.Content : "";
                    tmp_1.Add((string)variant.Content);
                }
            }
            else if (ComboBox_ChooseQuestionType.SelectedIndex == 2)
            {
                foreach (CheckBox variant in ListBox_Variants.Items)
                {
                    if (variant.IsChecked == true)
                    {
                        tmp_2.Add((string)variant.Content);
                    }

                    tmp_1.Add((string)variant.Content);
                }
            }
            else
            {
                foreach (string variant in ListBox_Variants.Items)
                {
                    tmp_1.Add(variant);
                }
                foreach(string variant in ListBox_RightOrder.Items)
                {
                    tmp_2.Add(variant);
                }
            }

            switch (ComboBox_ChooseQuestionType.SelectedIndex)
            { 
                case 0:
                    return new TypeUserAnswer(TextBox_Question.Text, TextBox_TrueAnswer.Text);
                case 1:
                    return new TypeOneVariant(TextBox_Question.Text, TextBox_TrueAnswer.Text, tmp_1);
                case 2:
                    return new TypeSeveralVariants(TextBox_Question.Text, tmp_2, tmp_1);
                case 3:
                    return new TypeYesOrNo(TextBox_Question.Text, tmp_3);
                case 4:
                    return new TypeRightOrder(TextBox_Question.Text, tmp_2, tmp_1);
                default:
                    return new TypeRightOrder("adwad", tmp_2, tmp_1);
            }    
        }


        private bool CheckBoxes()
        {
            if (RadioButton_Test.IsChecked == true)
            {
                if (!IsNotEmptyBoxQuestion() || !IsNotEmptyBoxAnswer())
                {
                    MessageBox.Show("Fill in the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                if (!IsNotEmptyBoxQuestion())
                {
                    MessageBox.Show("Fill in the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }

        private bool IsNotEmptyBoxQuestion()
        {
            bool result = true;
            int index = ComboBox_ChooseQuestionType.SelectedIndex;

            if (TextBox_Question.Text == "")
            {
                result = false;
            }

            if (index == 2 || index == 4 || index == 1)
            {
                if (ListBox_Variants.Items.Count < 2)
                {
                    result = false;
                }
            }
            return result;
        }

        private bool IsNotEmptyBoxAnswer()
        {
            bool result = true;
            int index = ComboBox_ChooseQuestionType.SelectedIndex;

            if (index == 0)
            {
                if (TextBox_TrueAnswer.Text == "")
                {
                    result = false;
                }
            }
            else if (index == 1)
            {
                foreach (RadioButton answer in ListBox_Variants.Items)
                {
                    if (answer.IsChecked == true)
                    {
                        result = true;
                        break;
                    }

                    result = false;
                }
            }
            else if (index == 2)
            {
                foreach (CheckBox answer in ListBox_Variants.Items)
                {
                    if (answer.IsChecked == true)
                    {
                        result = true;
                        break;
                    }

                    result = false;
                }
            }
            else if (index == 3)
            {
                if (RadioButton_No.IsChecked == false && RadioButton_Yes.IsChecked == false)
                {
                    result = false;
                }
            }
            else
            {
                if (ListBox_Variants.Items.Count != ListBox_RightOrder.Items.Count)
                {
                    result = false;
                }
            }

            return result;
            /* if (index == 4 && ListBox_Variants.Items.Count > 0)
             {
                 result = false;
             }

             if (index == 2)
             { 
                 if (countAnswer < 2)
                 {
                     result = false;
                 }
             }
             else if (index == 0)
             {
                 if (TextBox_TrueAnswer.Text == "")
                 {
                     result = false;
                 }
             }
             else
             {
                 if (RadioButton_No.IsChecked == false && RadioButton_Yes.IsChecked == false)
                 {
                     result = false;
                 }
             }*/
        }

        private void Button_AddOneOrFewVariants_Click(object sender, RoutedEventArgs e)
        {
            if(TextBox_OneOrFewVariants.Text == "")
            {
                return;
            }

            if(ComboBox_ChooseQuestionType.SelectedIndex == 1 && RadioButton_Test.IsChecked == true)
            {
                RadioButton variant = new RadioButton() { Content = TextBox_OneOrFewVariants.Text };
                ListBox_Variants.Items.Add(variant);
            }
            else if(ComboBox_ChooseQuestionType.SelectedIndex == 2 && RadioButton_Test.IsChecked == true)
            {
                CheckBox variant = new CheckBox() { Content = TextBox_OneOrFewVariants.Text };
                ListBox_Variants.Items.Add(variant);
            }
            else
            {
                string variant = TextBox_OneOrFewVariants.Text;
                ListBox_Variants.Items.Add(variant);
            }

            TextBox_OneOrFewVariants.Text = "";
        }

        private void Button_RemoveOneOrFewVariants_Click(object sender, RoutedEventArgs e)
        {
            if(ListBox_Variants.Items.Count != 0)
            {
                ListBox_RightOrder.Items.Remove(ListBox_Variants.Items[ListBox_Variants.Items.Count - 1]);
                ListBox_Variants.Items.RemoveAt(ListBox_Variants.Items.Count - 1);
            }
        }

        private void ListBox_Variants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ListBox_RightOrder.Items.Contains(ListBox_Variants.SelectedItem))
            {
                ListBox_RightOrder.Items.Add(ListBox_Variants.SelectedItem);
            }
        }

        private void ListBox_RightOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox_RightOrder.Items.Remove(ListBox_RightOrder.SelectedItem);
        }

        #endregion

        #region Кластер вопросов
        private void TextBox_ClasterName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ComboBox_Claster.Items.Add(TextBox_ClasterName.Text);
                TextBox_ClasterName.Clear();
            }
        }
        private void Button_AddClasterName_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_Claster.Items.Add(TextBox_ClasterName.Text);
            TextBox_ClasterName.Clear();
        }

        private void RadioButton_TestClaster_Click(object sender, RoutedEventArgs e)
        {
            ListView_SingleQuestions.Items.Clear();

            foreach (var question in _testsDataBase.TestSingelQuestions)
            {
                ListView_SingleQuestions.Items.Add(question.Description);
            }                     
        }

        private void RadioButton_PoolClaster_Click(object sender, RoutedEventArgs e)
        {
            ListView_SingleQuestions.Items.Clear();
            foreach (var question in _testsDataBase.TestSingelPolls)
            {
                ListView_SingleQuestions.Items.Add(question.Description);
            }
        }

        private void ComboBox_Claster_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #region context menu        
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

        private void MenuItem_ClickDeleteComboBox_Claster(object sender, RoutedEventArgs e)
        {
            int index = ComboBox_Claster.SelectedIndex;

            if (index <= 0)
            {
                return;
            }

            ComboBox_Claster.Items.RemoveAt(index);
        }
        #endregion

        //private void ComboBox_Claster_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    int index = ComboBox_Claster.SelectedIndex;

        //    if (TabControll_ClasterQuestions != null)
        //    {

        //        TabControll_ClasterQuestions.SelectedIndex = index;
        //    }
        //}

        //private void TextBox_ClasterName_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        _listView_ClasterQuestions = new ListView();
        //        _listView_ClasterQuestions = ListView_ClasterQuestions;
        //        ComboBox_Claster.Items.Add(TextBox_ClasterName.Text);
        //        TextBox_ClasterName.Clear();

        //        TabItem tmp = new TabItem { Header = new TextBlock { Text = TextBox_ClasterName.Text }, Content = _listView_ClasterQuestions };
        //        _listOfListView_ClasterQuestions.Add(_listView_ClasterQuestions);

        //        TabControll_ClasterQuestions.Items.Add(tmp);

        //        tmp.Visibility = Visibility.Collapsed;
        //    }

        //}

        #endregion

        private void Window_MainWindow_Closed(object sender, EventArgs e)
        {
            _usersDataBase.Save();
        }

        private void ComboBox_QuestionContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            if (DataGrid_SingleQuestions == null)
            {
                return;
            }

            _testsDataBase = TestsDataBase.GetInstance();

            List<AbstractQuestion> tmpTestOrPoll = RadioButton_TestContainer.IsChecked == true ? _testsDataBase.TestSingelQuestions : _testsDataBase.TestSingelPolls;

            List<AbstractQuestion> tmp = new List<AbstractQuestion>();

            
            foreach (var type in tmpTestOrPoll)
            {
                switch (ComboBox_QuestionContainer.SelectedIndex)
                {
                    case 0:
                        tmp.Add(type);
                        break;

                    case 1:
                        if (type is TypeUserAnswer)
                        {
                            tmp.Add(type);
                        }
                        break;

                    case 2:
                        if (type is TypeOneVariant)
                        {
                            tmp.Add(type);                         
                        }
                        break;

                    case 3:
                        if (type is TypeSeveralVariants)
                        {

                            tmp.Add(type);
                        }
                        break;

                    case 4:
                        if (type is TypeYesOrNo)
                        {
                            tmp.Add(type);
                        }
                        break;

                    default:
                        if (type is TypeRightOrder)
                        {  
                            tmp.Add(type);
                        }
                        break;
                }
            }

           
            DataGrid_SingleQuestions.ItemsSource = tmp;

            for(int i = 1; i < DataGrid_SingleQuestions.Columns.Count; i++)
            {
                 DataGrid_SingleQuestions.Columns[i].MaxWidth = 0;

            }

           testMenu.Items.Clear();
        }

        private void MenuItem_ClickAnswers(object sender, RoutedEventArgs e)
        {
            

        }
        private void MenuItem_ClickTryAnswers(object sender, RoutedEventArgs e)
        {
            

        }

        private void DataGrid_SingleQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ComboBox_QuestionContainer.SelectedIndex;

            AbstractQuestion tmp =(AbstractQuestion) DataGrid_SingleQuestions.SelectedItem;

            if(tmp == null|| tmp.Variants == null)
            {
                return;
            }

            if (testMenu.Items != null)
            {
                testMenu.Items.Clear();

            }


            testMenu.Items.Add("ВАРИАНТЫ ОТВЕТОВ:");
            foreach (var variants in tmp.Variants)
            {
                testMenu.Items.Add(variants);
            }

           if(RadioButton_TestContainer.IsChecked == true)
           {
                testMenu.Items.Add("ПРАВИЛЬНЫЕ ОТВЕТЫ:");
                if(tmp.TrueAnswers == null)
                {
                    testMenu.Items.Add(tmp.TrueAnswer);

                }
                else
                {
                    foreach (var trueAnswer in tmp.TrueAnswers)
                    {
                        testMenu.Items.Add(trueAnswer);
                    }

                }


           }
                
            DataGrid_SingleQuestions.ContextMenu = testMenu;
            this.ContextMenu = testMenu;

            tmp = null;
        }


        #endregion

        private void Window_MainWindow_Closed(object sender, EventArgs e)
        {
            _testsDataBase.SaveSingel(_testsDataBase.TestSingelQuestions, _testsDataBase.PollSingelQuestions);
            _testsDataBase.SaveClaster(_testsDataBase.Tests, _testsDataBase.Polls);
        }
    }
    #endregion
}