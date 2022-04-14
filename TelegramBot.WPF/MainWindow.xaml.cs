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

namespace TelegramBot.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_SendQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_ChooseQuestionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBox_ChooseQuestionType.SelectedIndex)
            {
                case 0:
                    //TextBox_Question.Visibility = Visibility.Visible;
                    TextBox_Variants.Visibility = Visibility.Hidden;
                    Button_AddVariant.Visibility = Visibility.Hidden;
                    Button_RemoveVariant.Visibility = Visibility.Hidden;
                    RadioButton_Test.Visibility = Visibility.Visible;
                    RadioButton_Poll.Visibility = Visibility.Visible;
                    RadioButton_Yes.Visibility = Visibility.Hidden;
                    RadioButton_No.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    //TextBox_Question.Visibility = Visibility.Visible;
                    TextBox_Variants.Visibility = Visibility.Hidden;
                    Button_AddVariant.Visibility = Visibility.Hidden;
                    Button_RemoveVariant.Visibility = Visibility.Hidden;
                    RadioButton_Test.Visibility = Visibility.Visible;
                    RadioButton_Poll.Visibility = Visibility.Visible;
                    RadioButton_Yes.Visibility = Visibility.Hidden;
                    RadioButton_No.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    //TextBox_Question.Visibility = Visibility.Visible;
                    TextBox_Variants.Visibility = Visibility.Visible;
                    Button_AddVariant.Visibility = Visibility.Visible;
                    Button_RemoveVariant.Visibility = Visibility.Visible;
                    RadioButton_Test.Visibility = Visibility.Visible;
                    RadioButton_Poll.Visibility = Visibility.Visible;
                    RadioButton_Yes.Visibility = Visibility.Hidden;
                    RadioButton_No.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    //TextBox_Question.Visibility = Visibility.Visible;
                    RadioButton_Yes.Visibility = Visibility.Visible;
                    RadioButton_No.Visibility = Visibility.Visible;
                    TextBox_Variants.Visibility = Visibility.Hidden;
                    Button_AddVariant.Visibility = Visibility.Hidden;
                    Button_RemoveVariant.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    RadioButton_Test.Visibility = Visibility.Visible;
                    RadioButton_Poll.Visibility = Visibility.Visible;
                    TextBox_Variants.Visibility = Visibility.Visible;
                    Button_AddVariant.Visibility = Visibility.Visible;
                    Button_RemoveVariant.Visibility = Visibility.Visible;
                    RadioButton_Yes.Visibility = Visibility.Hidden;
                    RadioButton_No.Visibility = Visibility.Hidden;
                    break;
                //case 5:
                //    TextBox_Question.Visibility = Visibility.Hidden;
                //    TextBox_Variants.Visibility = Visibility.Hidden;
                //    Button_AddVariant.Visibility = Visibility.Hidden;
                //    Button_RemoveVariant.Visibility = Visibility.Hidden;
                //    RadioButton_Test.Visibility = Visibility.Visible;
                //    RadioButton_Poll.Visibility = Visibility.Visible;
                //    break;
                default:
                    RadioButton_Test.Visibility = Visibility.Hidden;
                    RadioButton_Poll.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void RadioButton_Test_Click(object sender, RoutedEventArgs e)
        {
            TextBox_TrueAnswer.Visibility = Visibility.Visible;
            Button_AddAnswer.Visibility = Visibility.Visible;
            Button_RemoveAnswer.Visibility = Visibility.Visible;
            Label_TrueAnswer.Visibility = Visibility.Visible;
        }

        private void RadioButton_Poll_Click(object sender, RoutedEventArgs e)
        {
            TextBox_TrueAnswer.Visibility = Visibility.Hidden;
            Button_AddAnswer.Visibility = Visibility.Hidden;
            Button_RemoveAnswer.Visibility = Visibility.Hidden;
            Label_TrueAnswer.Visibility = Visibility.Hidden;
        }

        private void Button_RemoveAnswer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
