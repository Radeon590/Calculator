using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _varOne = 0;
        private int _varTwo = 0;
        private string _operation = "*";
        private bool _isVarOneEntered = false;
        private bool _isGreyLast = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowHistory(object sender, RoutedEventArgs e)
        {
            storyBlockGrid.Visibility = Visibility.Visible;
        }

        private void DontShowHistory(object sender, RoutedEventArgs e)
        {
            storyBlockGrid.Visibility = Visibility.Collapsed;
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            storyList.Items.Clear();
            _isGreyLast = false;
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            storyList.Items.RemoveAt(storyList.Items.Count - 1);
            _isGreyLast = !_isGreyLast;
        }

        private void CalculatorButtonClick(object sender, RoutedEventArgs e)
        {
            string operationsTypes = "+-X/";

            Button button = (Button)e.Source;
            string content = (string)button.Content;
            textBox.Text = content;
            if (content == "C")
            {
                ClearTextBox();
            }
            else if (content == "=")
            {
                int result = 0;
                switch (_operation)
                {
                    case "+":
                        result = _varOne + _varTwo;
                        break;
                    case "-":
                        result = _varOne - _varTwo;
                        break;
                    case "X":
                        result = _varOne * _varTwo;
                        break;
                    case "/":
                        if (_varTwo != 0)
                            result = _varOne / _varTwo;
                        else
                        {
                            textBox.Text = "Error. Zero Deviding";
                            _varOne = 0;
                            _varTwo = 0;
                            _isVarOneEntered = false;
                            SaveOperation("Error. Zero Deviding");
                            return;
                        }
                        break;
                }
                SaveOperation($"{_varOne} {_operation} {_varTwo} = {result}");
                _varOne = result;
                _varTwo = 0;
                _isVarOneEntered = false;
                textBox.Text = Convert.ToString(result);
            }
            else if (operationsTypes.Contains(content))
            {
                if (textBox.Text == "Error. Zero Deviding")
                {
                    ClearTextBox();
                }
                else
                {
                    _operation = content;
                    textBox.Text = content;
                    _isVarOneEntered = true;
                }
            }
            else
            {
                if (textBox.Text == "Error. Zero Deviding")
                {
                    ClearTextBox();
                    _varOne = Convert.ToInt32(content);
                    textBox.Text = content;
                    return;
                }
                if (textBox.Text == "0")
                {
                    textBox.Text = content;
                    if (!_isVarOneEntered)
                        _varOne = Convert.ToInt32(content);
                    else
                        _varTwo = Convert.ToInt32(content);
                }
                else
                {
                    if (!_isVarOneEntered)
                    {
                        _varOne = _varOne * 10 + Convert.ToInt32(content);
                        textBox.Text = Convert.ToString(_varOne);
                    }
                    else
                    {
                        _varTwo = _varTwo * 10 + Convert.ToInt32(content);
                        textBox.Text = Convert.ToString(_varTwo);
                    }
                }
            }
        }

        private void SaveOperation(string operation)
        {
            if(save.IsChecked == true)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Background = _isGreyLast ? new SolidColorBrush(Color.FromRgb(255, 255, 255)) : new SolidColorBrush(Color.FromRgb(128, 128, 128));
                _isGreyLast = !_isGreyLast;
                newItem.Content = operation;
                storyList.Items.Add(newItem);
            }
        }

        private void ClearTextBox()
        {
            textBox.Text = "0";
            _varOne = 0;
            _varTwo = 0;
            _operation = "*";
            _isVarOneEntered = false;
        }
    }
}
