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
using TelegramBot.BL.Askers;
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
        GroupBox _editFormVariants;
        GroupBox _editFormAnswer;
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

            _testsDataBase.TestSingelQuestions = _testsDataBase.LoadSingelTest();
            _testsDataBase.PollSingelQuestions = _testsDataBase.LoadSingelPoll();
            _testsDataBase.Tests = _testsDataBase.LoadClasterTests();
            _testsDataBase.Polls = _testsDataBase.LoadClasterPolls();


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



            for (int i = 0; i < _usersDataBase.UserGroups.Count; i++)
            {
                ComboBox_UserGroups.Items.Add(_usersDataBase.UserGroups[i].NameGroup);
                
                ListBox_UserGroups.Items.Add(ComboBox_UserGroups.Items[i]);

            }
            if(_usersDataBase.UserGroups[0].NameGroup != "пользователи без группы")
            {

                ComboBox_UserGroups.Items.Add(_usersDataBase.UserGroups[0]);
            }



            
        }

        public void AddUsers(User newUser)
        {
            bool isSearch = false;
            foreach (var group in _usersDataBase.UserGroups)
            {

                foreach (var user in group.UserGroups)
                {
                    
                    if (newUser.Id == user.Id)
                    {
                        isSearch = true;
                        break;
                    }
                }
                if(isSearch)
                {
                    break;
                }
            }  
            if(!isSearch)
            {
                _usersDataBase.UserGroups[0].AddUser(newUser);
            }
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
            _listOfListBox_Users.RemoveAt(index);
            _usersDataBase.UserGroups.RemoveAt(index);
            ListBox_UserGroups.Items.RemoveAt(index);
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

        #region Edit

        private void RadioButtonEdit_Test_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_ChooseQuestionTypeEdit.Items.Clear();
            GroupBox_TestEdit.Visibility = Visibility.Visible;
            GroupBox_ChoseTypeQuestionEdit.Visibility = Visibility.Visible;

            ComboBox_ChooseTestEdit.Items.Clear();
            ComboBox_ChooseTestEdit.Items.Add("Все вопросы");

            foreach (Claster claster in _testsDataBase.Tests)
            {
                ComboBox_ChooseTestEdit.Items.Add(claster.NameClaster);
            }

            HideExtraBoxesEdit();
        } 

        private void RadioButtonEdit_Poll_Click(object sender, RoutedEventArgs e) 
        {
            ComboBox_ChooseQuestionTypeEdit.Items.Clear();
            GroupBox_TestEdit.Visibility = Visibility.Visible;
            GroupBox_ChoseTypeQuestionEdit.Visibility = Visibility.Visible;

            ComboBox_ChooseTestEdit.Items.Clear();
            ComboBox_ChooseTestEdit.Items.Add("Все вопросы");

            foreach (Claster claster in _testsDataBase.Polls)
            {
                ComboBox_ChooseTestEdit.Items.Add(claster.NameClaster);
            }

            HideExtraBoxesEdit();
        } 


        private void ComboBox_ChooseTestOrPoll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupBox_ChoseTypeQuestion.Visibility = Visibility.Visible;

        }


        private void ComboBox_ChooseTestEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_ChooseTestEdit.SelectedIndex == -1)
            {
                return;
            }

            List<AbstractQuestion> questions = GetQuestions();
            ComboBox_ChooseQuestionTypeEdit.Items.Clear();

            foreach(AbstractQuestion question in questions)
            {
                ComboBox_ChooseQuestionTypeEdit.Items.Add(question.Description);
            }

            HideExtraBoxesEdit();
        }

        private void ComboBox_ChooseQuestionTypeEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_ChooseQuestionTypeEdit.SelectedIndex == -1)
            {
                return;
            }

            HideExtraBoxesEdit();

            Button_SaveChanges.Visibility = Visibility.Visible;
            Button_CancelChanges.Visibility = Visibility.Visible;

            List<AbstractQuestion> questions = GetQuestions();

            AbstractQuestion question = questions[ComboBox_ChooseQuestionTypeEdit.SelectedIndex];

            GroupBox_QuestionEdit.Visibility = Visibility.Visible;

            TextBox_QuestionEdit.Text = question.Description;

            OpenBoxEdit(question);

            if (RadioButtonEdit_Test.IsChecked == true)
            {
                OpenBoxTrueAnswerEdit(question);
            }
        }

        private void OpenBoxTrueAnswerEdit(AbstractQuestion question)
        {
            if (question is TypeYesOrNo)
            {
                GroupBox_AnswerYesOrNoEdit.Visibility = Visibility.Visible;

                RadioButton_YesEdit.IsChecked = question.TrueAnswer == "Да" ? true : false;
                RadioButton_NoEdit.IsChecked = question.TrueAnswer == "Нет" ? true : false;
            }
            else if (question is TypeUserAnswer)
            {
                GroupBox_ChangeOneAnswerEdit.Visibility = Visibility.Visible;

                TextBox_AnswersEdit.Text = question.TrueAnswer;
            }
        }

        private void OpenBoxEdit(AbstractQuestion question)
        {
            if(question is TypeRightOrder || question is TypeSeveralVariants || question is TypeOneVariant)
            {
                GroupBox_AddVariantEdit.Visibility = Visibility.Visible;
                GroupBox_AddTrueVarintsOrRigthOrderEdit.Visibility = Visibility.Visible;

                FillListBox(question);
            }
        }

        private void FillListBox(AbstractQuestion question)
        {
            foreach (string variant in question.Variants)
            {
                if (RadioButtonEdit_Poll.IsChecked == true || question is TypeRightOrder)
                {
                    ListBox_VariantsEdit.Items.Add(variant);
                }
                else if (question is TypeSeveralVariants)
                {
                    CheckBox checkBoxVariant = new CheckBox();
                    checkBoxVariant.Content = variant;

                    if(question.TrueAnswers.Contains(variant))
                    {
                        checkBoxVariant.IsChecked = true;
                    }

                    ListBox_VariantsEdit.Items.Add(checkBoxVariant);
                }
                else
                {
                    RadioButton radioButtunVariant = new RadioButton();
                    radioButtunVariant.Content = variant;

                    if (variant == question.TrueAnswer)
                    {
                        radioButtunVariant.IsChecked = true;
                    }

                    ListBox_VariantsEdit.Items.Add(radioButtunVariant);
                }
            }

            if(RadioButtonEdit_Test.IsChecked == true && question is TypeRightOrder)
            {
                foreach(string variant in question.TrueAnswers)
                {
                    ListBox_RightOrderEdit.Items.Add(variant);
                }

                ListBox_RightOrderEdit.Visibility = Visibility.Visible;
            }

        }

        private List<AbstractQuestion> GetQuestions()
        {
            List<AbstractQuestion> questions;

            if (ComboBox_ChooseTestEdit.SelectedIndex == 0)
            {
                questions = RadioButtonEdit_Test.IsChecked == true ? _testsDataBase.TestSingelQuestions : _testsDataBase.PollSingelQuestions;
            }
            else
            {
                questions = RadioButtonEdit_Test.IsChecked == true ? _testsDataBase.Tests[ComboBox_ChooseTestEdit.SelectedIndex - 1].Questions
                    : _testsDataBase.Polls[ComboBox_ChooseTestEdit.SelectedIndex - 1].Questions;
            }

            return questions;
        }

        private void Button_AddOneOrFewVariantsEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_OneOrFewVariantsEdit.Text == "")
            {
                return;
            }

            List<AbstractQuestion> questions = GetQuestions();
            AbstractQuestion question = questions[ComboBox_ChooseQuestionTypeEdit.SelectedIndex];

            if (question is TypeOneVariant && RadioButtonEdit_Test.IsChecked == true)
            {
                RadioButton variant = new RadioButton() { Content = TextBox_OneOrFewVariantsEdit.Text };
                ListBox_VariantsEdit.Items.Add(variant);
            }
            else if (question is TypeSeveralVariants && RadioButtonEdit_Test.IsChecked == true)
            {
                CheckBox variant = new CheckBox() { Content = TextBox_OneOrFewVariantsEdit.Text };
                ListBox_VariantsEdit.Items.Add(variant);
            }
            else
            {
                string variant = TextBox_OneOrFewVariantsEdit.Text;
                ListBox_VariantsEdit.Items.Add(variant);
            }

            TextBox_OneOrFewVariantsEdit.Text = "";
        }

        private void Button_RemoveOneOrFewVariantsEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_VariantsEdit.Items.Count != 0)
            {
                ListBox_RightOrderEdit.Items.Remove(ListBox_VariantsEdit.Items[ListBox_VariantsEdit.Items.Count - 1]);
                ListBox_VariantsEdit.Items.RemoveAt(ListBox_VariantsEdit.Items.Count - 1);
            }
        }

        private void ListBox_VariantsEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<AbstractQuestion> questions = GetQuestions();
            AbstractQuestion question = questions[ComboBox_ChooseQuestionTypeEdit.SelectedIndex];

            if (!ListBox_RightOrderEdit.Items.Contains(ListBox_VariantsEdit.SelectedItem) && question is TypeRightOrder)
            {
                ListBox_RightOrderEdit.Items.Add(ListBox_VariantsEdit.SelectedItem);
            }
        }

        private void ListBox_RightOrderEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox_RightOrderEdit.Items.Remove(ListBox_RightOrderEdit.SelectedItem);
        }


        private void Button_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            List<AbstractQuestion> questions = GetQuestions();
            AbstractQuestion question = questions[ComboBox_ChooseQuestionTypeEdit.SelectedIndex];
            SetEditQuestionWithAnswer(question);
        }

        private void SetEditQuestionWithAnswer(AbstractQuestion question)
        {
            List<string> variants = new List<string>();
            List<string> trueAnswers = new List<string>();
            string trueAnswer = RadioButton_YesEdit.IsChecked == true ? "Да" : "Нет";

            if (question is TypeOneVariant && RadioButtonEdit_Test.IsChecked == true)
            {
                foreach (RadioButton variant in ListBox_VariantsEdit.Items)
                {
                    if (variant.IsChecked == true)
                    {
                        trueAnswer = (string)variant.Content;
                    }
                    variants.Add((string)variant.Content);
                }
            }
            else if (question is TypeSeveralVariants && RadioButtonEdit_Test.IsChecked == true)
            {
                foreach (CheckBox variant in ListBox_VariantsEdit.Items)
                {
                    if (variant.IsChecked == true)
                    {
                        trueAnswers.Add((string)variant.Content);
                    }

                    variants.Add((string)variant.Content);
                }
            }
            else
            {
                foreach (string variant in ListBox_VariantsEdit.Items)
                {
                    variants.Add(variant);
                }
                foreach (string variant in ListBox_RightOrderEdit.Items)
                {
                    trueAnswers.Add(variant);
                }
            }

            question.Description = TextBox_QuestionEdit.Text;

            if( question is TypeUserAnswer && RadioButtonEdit_Test.IsChecked == true)
            {
                question.TrueAnswer = TextBox_AnswersEdit.Text;
            }
            else if(question is TypeYesOrNo && RadioButtonEdit_Test.IsChecked == true)
            {
                question.TrueAnswer = trueAnswer;
            }
            else
            {
                question.Variants = variants;
                if (RadioButtonEdit_Test.IsChecked == true)
                {
                    if (question is TypeOneVariant)
                    {
                        question.TrueAnswer = trueAnswer;
                    }
                    else
                    {
                        question.TrueAnswers = trueAnswers;
                    }
                }
            }
        }

        private void Button_CancelChanges_Click(object sender, RoutedEventArgs e)
        {
            HideExtraBoxesEdit();
        }

        private void HideExtraBoxesEdit()
        {

            Button_SaveChanges.Visibility = Visibility.Hidden;

            Button_CancelChanges.Visibility = Visibility.Hidden;

            GroupBox_AddVariantEdit.Visibility = Visibility.Hidden;

            GroupBox_AnswerYesOrNoEdit.Visibility = Visibility.Hidden;

            RadioButton_YesEdit.IsChecked = false;

            RadioButton_NoEdit.IsChecked = false;

            GroupBox_AddTrueVarintsOrRigthOrderEdit.Visibility = Visibility.Hidden;

            ListBox_VariantsEdit.Items.Clear();

            ListBox_RightOrderEdit.Items.Clear();

            TextBox_QuestionEdit.Visibility = Visibility.Visible;

            TextBox_QuestionEdit.Text = "";

            GroupBox_ChangeOneAnswerEdit.Visibility = Visibility.Hidden;

            TextBox_AnswersEdit.Text = "";

            GroupBox_QuestionEdit.Visibility = Visibility.Hidden;

            ListBox_RightOrderEdit.Visibility = Visibility.Hidden;

            Button_SaveChanges.Visibility = Visibility.Hidden;
            Button_CancelChanges.Visibility = Visibility.Hidden;
        }

        #endregion

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

        //private void MenuItemEditСhange_ClickCut(object sender, RoutedEventArgs e)
        //{
        //    int index = DataGrid_ChangeAnswers.SelectedIndex;
        //    if (index == -1)
        //    {
        //        return;
        //    }
        //    DataGrid_ChangeAnswers.Items.RemoveAt(index);

        //}

      


        private void MenuItemEditChange_ClickInsert(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            _tryAnswers.Add(new TypeOneVariant("", "", list));
            DataGrid_ChangeAnswers.Items.Add(_tryAnswers);
        }*/


        private void Button_SendToBot_Click(object sender, RoutedEventArgs e)
        {
            AbstractQuestion abstractQuestion = (AbstractQuestion)DataGrid_SingleQuestions.SelectedItem;
            CreateButtons newButtons = new CreateButtons();  
            int indexGroup = ListBox_UserGroups.SelectedIndex;

            if (indexGroup == -1)
            {
                return;
            }


            if (1 == ComboBox_QuestionContainer.SelectedIndex && abstractQuestion is not null)
            {
               
                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {

                    _tbot.Send((string)abstractQuestion.Description, user.Id);
                }
            }


            if (2 == ComboBox_QuestionContainer.SelectedIndex && abstractQuestion is not null)
            {
                List<InlineKeyboardButton[]> newButtonsList = newButtons.AddButtons(abstractQuestion.Description, abstractQuestion.Variants);

                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {
                    _tbot.Send(abstractQuestion.Description, user.Id, newButtonsList.ToArray());
                }
            }

            if (3 == ComboBox_QuestionContainer.SelectedIndex && abstractQuestion is not null)
            {
                List<InlineKeyboardButton[]> newButtonsList = newButtons.AddButtons(abstractQuestion.Description, abstractQuestion.Variants);

                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {
                    _tbot.Send(abstractQuestion.Description, user.Id, newButtonsList.ToArray());
                }
            }
                
            if (4 == ComboBox_QuestionContainer.SelectedIndex && abstractQuestion is not null)
            {
                List<string> variants = new List<string> { "Да", "Нет" };
                List<InlineKeyboardButton[]> newButtonsList = newButtons.AddButtons(abstractQuestion.Description, variants);

                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {
                    _tbot.Send(abstractQuestion.Description, user.Id, newButtonsList.ToArray());
                }

            }

            if (5 == ComboBox_QuestionContainer.SelectedIndex && abstractQuestion is not null)
            {
                List<InlineKeyboardButton[]> newButtonsList = newButtons.AddButtons(abstractQuestion.Description, abstractQuestion.Variants);

                foreach (User user in _usersDataBase.UserGroups[indexGroup].UserGroups)
                {
                    _tbot.Send(abstractQuestion.Description, user.Id, newButtonsList.ToArray());
                }

            }
            DataGrid_SingleQuestions.SelectedItem = null;
            ListBox_UserGroups.SelectedIndex = -1;
            newButtons = null;
        }


        private void RadioButton_PollContainer_Checked(object sender, RoutedEventArgs e)
        {
           
            DataGrid_SingleQuestions.ItemsSource = _testsDataBase.PollSingelQuestions;

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
            if (!CheckBoxes())
            {
                return;
            }

            if (RadioButton_Test.IsChecked == true)
            {
                AbstractQuestion question = GetQuestionWhithAnswer();
                _testsDataBase.TestSingelQuestions.Add(question);
            }
            else
            {
                AbstractQuestion question = GetQuestionWhithoutAnswer();
                _testsDataBase.PollSingelQuestions.Add(question);
            }

            if (ComboBox_ChooseTestOrPoll.SelectedIndex != 0)
            {
                if (RadioButton_Test.IsChecked == true)
                {
                    AbstractQuestion question = GetQuestionWhithAnswer();
                    _testsDataBase.Tests[ComboBox_ChooseTestOrPoll.SelectedIndex - 1].Add(question);
                }
                else
                {
                    AbstractQuestion question = GetQuestionWhithoutAnswer();
                    _testsDataBase.Polls[ComboBox_ChooseTestOrPoll.SelectedIndex - 1].Add(question);
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
            List<string> variants = new List<string>();
            List<string> trueAnswers = new List<string>();
            string trueAnswer = RadioButton_Yes.IsChecked == true ? "Да" : "Нет";

            if(ComboBox_ChooseQuestionType.SelectedIndex == 1)
            {
                foreach (RadioButton variant in ListBox_Variants.Items)
                {
                    if (variant.IsChecked == true)
                    {
                        trueAnswer = (string)variant.Content;
                    }
                    variants.Add((string)variant.Content);
                }
            }
            else if (ComboBox_ChooseQuestionType.SelectedIndex == 2)
            {
                foreach (CheckBox variant in ListBox_Variants.Items)
                {
                    if (variant.IsChecked == true)
                    {
                        trueAnswers.Add((string)variant.Content);
                    }

                    variants.Add((string)variant.Content);
                }
            }
            else
            {
                foreach (string variant in ListBox_Variants.Items)
                {
                    variants.Add(variant);
                }
                foreach(string variant in ListBox_RightOrder.Items)
                {
                    trueAnswers.Add(variant);
                }
            }

            switch (ComboBox_ChooseQuestionType.SelectedIndex)
            { 
                case 0:
                    return new TypeUserAnswer(TextBox_Question.Text, TextBox_TrueAnswer.Text);
                case 1:
                    return new TypeOneVariant(TextBox_Question.Text, trueAnswer, variants);
                case 2:
                    return new TypeSeveralVariants(TextBox_Question.Text, trueAnswers, variants);
                case 3:
                    return new TypeYesOrNo(TextBox_Question.Text, trueAnswer);
                case 4:
                    return new TypeRightOrder(TextBox_Question.Text, trueAnswers, variants);
                default:
                    return new TypeRightOrder("adwad", trueAnswers, variants);
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
            if (!ListBox_RightOrder.Items.Contains(ListBox_Variants.SelectedItem) && ComboBox_ChooseQuestionType.SelectedIndex == 4)
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
        private void TextBox_ClasterName_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox_Claster.SelectedIndex = -1;
        }
        private void Button_AddClasterName_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBox_ClasterName.Text;

            if ((RadioButton_TestClaster.IsChecked == false && RadioButton_PoolClaster.IsChecked == false) || name == "" || name == " ")
            {
                return;
            }

            ComboBox_Claster.Items.Add(TextBox_ClasterName.Text);

            Claster clasterQuestion;

            if (ListView_ClasterQuestions.Items.Count == 0)
            {
                clasterQuestion = new Claster(TextBox_ClasterName.Text);
            }
            else
            {
                List<AbstractQuestion> questions = GetQuestionsFromList(RadioButton_TestClaster.IsChecked, RadioButton_PoolClaster.IsChecked);
                clasterQuestion = new Claster(TextBox_ClasterName.Text, questions);
            }

            if(RadioButton_TestClaster.IsChecked == true)
            {
                _testsDataBase.Tests.Add(clasterQuestion);
            }
            else
            {
                _testsDataBase.Polls.Add(clasterQuestion);
            }

            ListView_ClasterQuestions.Items.Clear();
            TextBox_ClasterName.Clear();
        }

        private List<AbstractQuestion> GetQuestionsFromList(bool? radioCheckTest, bool? radioCheckPoll)
        {
            List<AbstractQuestion> singelQuestions;

            if (radioCheckTest is not null && radioCheckTest == true)
            {
                singelQuestions = _testsDataBase.TestSingelQuestions;
            }
            else
            {
                singelQuestions = _testsDataBase.PollSingelQuestions;
            }

            List <AbstractQuestion> resultQuestions = new List<AbstractQuestion>();

            foreach(var question in ListView_ClasterQuestions.Items)
            {
                int index = ListView_SingleQuestions.Items.IndexOf(question);
                resultQuestions.Add(singelQuestions[index]);
            }

            return resultQuestions;
        }

        private void RadioButton_TestClaster_Click(object sender, RoutedEventArgs e)
        {
            ListView_SingleQuestions.Items.Clear();

            foreach (var question in _testsDataBase.TestSingelQuestions)
            {
                ListView_SingleQuestions.Items.Add(question.Description);
            }

            ComboBox_Claster.Items.Clear();

            foreach (var claster in _testsDataBase.Tests)
            {
                ComboBox_Claster.Items.Add(claster.NameClaster);
            }
        }

        private void RadioButton_PoolClaster_Click(object sender, RoutedEventArgs e)
        {
            ListView_SingleQuestions.Items.Clear();

            foreach (var question in _testsDataBase.PollSingelQuestions)
            {
                ListView_SingleQuestions.Items.Add(question.Description);
            }

            ComboBox_Claster.Items.Clear();

            foreach (var claster in _testsDataBase.Polls)
            {
                ComboBox_Claster.Items.Add(claster.NameClaster);
            }
        }

        private void ComboBox_Claster_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView_ClasterQuestions.Items.Clear();

            TextBox_ClasterName.Clear();

            if (ComboBox_Claster.SelectedIndex == -1)
            {
                return;
            }

            List<Claster> clasters = new List<Claster>();

            if (RadioButton_TestClaster.IsChecked == true)
            {
                clasters = _testsDataBase.Tests;
            }
            else
            {
                clasters = _testsDataBase.Polls;
            }

            int index = ComboBox_Claster.SelectedIndex;

            foreach(AbstractQuestion question in clasters[index].Questions)
            {
                ListView_ClasterQuestions.Items.Add(question.Description);
            }

        }

        #region context menu        

        private void MenuItem_ClickInsertListView_SingleQuestions(object sender, RoutedEventArgs e)
        {
            if(ListView_ClasterQuestions.Items.Contains(ListView_SingleQuestions.SelectedItem))
            {
                return;
            }

            ListView_ClasterQuestions.Items.Add(ListView_SingleQuestions.SelectedItem);

            if (ComboBox_Claster.SelectedIndex != -1)
            {
                List<Claster> clasters = RadioButton_TestClaster.IsChecked == true ? _testsDataBase.Tests : _testsDataBase.Polls;
                List<AbstractQuestion> questions = RadioButton_TestClaster.IsChecked == true ? _testsDataBase.TestSingelQuestions : _testsDataBase.PollSingelQuestions;

                clasters[ComboBox_Claster.SelectedIndex].Add(questions[ListView_SingleQuestions.SelectedIndex]);
            }
        }

        private void MenuItem_ClickDeleteListView_SingleQuestions(object sender, RoutedEventArgs e)
        {
            List<AbstractQuestion> questions = RadioButton_TestClaster.IsChecked == true ? _testsDataBase.TestSingelQuestions : _testsDataBase.PollSingelQuestions;

            questions.Remove(questions[ListView_SingleQuestions.SelectedIndex]);

            ListView_SingleQuestions.Items.RemoveAt(ListView_SingleQuestions.SelectedIndex);
        }


        private void MenuItem_ClickDeleteListView_ClasterQuestions(object sender, RoutedEventArgs e)
        { 

            if (ComboBox_Claster.SelectedIndex != -1)
            {
                List<Claster> clasters = RadioButton_TestClaster.IsChecked == true ? _testsDataBase.Tests : _testsDataBase.Polls;
                clasters[ComboBox_Claster.SelectedIndex].Remove(ListView_ClasterQuestions.SelectedIndex);
            }

            ListView_ClasterQuestions.Items.RemoveAt(ListView_ClasterQuestions.SelectedIndex);
        }



        private void ContextMenu_DeleteClaster_Click(object sender, RoutedEventArgs e)
        {
            List<Claster> clasters = RadioButton_TestClaster.IsChecked == true ? _testsDataBase.Tests : _testsDataBase.Polls;

            clasters.RemoveAt(ComboBox_Claster.SelectedIndex);

            ComboBox_Claster.Items.Clear();

            foreach (var claster in clasters)
            {
                ComboBox_Claster.Items.Add(claster.NameClaster);
            }
        }

        private int _indexOfClaster;


        private void ContextMenuRenameClaster_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_Claster.IsEditable = true;
            _indexOfClaster = ComboBox_Claster.SelectedIndex;
        }

        private void ComboBox_Claster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<Claster> clasters = RadioButton_TestClaster.IsChecked == true ? _testsDataBase.Tests : _testsDataBase.Polls;

                clasters[_indexOfClaster].NameClaster = ComboBox_Claster.Text;

                ComboBox_Claster.IsEditable = false;

                ComboBox_Claster.Items.Clear();

                foreach (var claster in clasters)
                {
                    ComboBox_Claster.Items.Add(claster.NameClaster);
                }
            }
        }
        #endregion



        #endregion

        private void Window_MainWindow_Closed(object sender, EventArgs e)
        {
            _usersDataBase.Save();
            _testsDataBase.SaveSingelTest(_testsDataBase.TestSingelQuestions);
            _testsDataBase.SaveSingelPoll(_testsDataBase.PollSingelQuestions);
            _testsDataBase.SaveClasterTest(_testsDataBase.Tests);
            _testsDataBase.SaveClasterPoll(_testsDataBase.Polls);
        }

        private void ComboBox_QuestionContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            if (DataGrid_SingleQuestions == null)
            {
                return;
            }

            _testsDataBase = TestsDataBase.GetInstance();

            List<AbstractQuestion> tmpTestOrPoll = RadioButton_TestContainer.IsChecked == true ? _testsDataBase.TestSingelQuestions : _testsDataBase.PollSingelQuestions;

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

    }
}