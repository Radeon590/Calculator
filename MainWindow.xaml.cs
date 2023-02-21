using System;
using System.Windows;
using System.Windows.Controls;

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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
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
                            return;
                        }
                        break;
                }
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
