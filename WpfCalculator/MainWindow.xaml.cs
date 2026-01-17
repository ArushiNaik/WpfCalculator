using System;
using System.Windows;
using System.Windows.Controls;
public enum SelectedOperator
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}
public static class MathService
{
    public static double Add(double a, double b) => a + b;
    public static double Subtract(double a, double b) => a - b;
    public static double Multiply(double a, double b) => a * b;
    public static double Divide(double a, double b) => a / b;
}
namespace WpfCalculator
{
    
    public partial class MainWindow : Window
    {
       
        double lastNumber;
        double result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            ACBtn.Click += AC_Click;
            PlusMinusBtn.Click += PlusMinus_Click;
            PercentBtn.Click += Percent_Click;
            DotBtn.Click += Dot_Click;
            EqualBtn.Click += Equal_Click;
        }

        
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "";
            var button = (Button)sender;
            string num = button.Content.ToString();

            string current = ResultLabel.Content.ToString();

            if (current == "0")
                ResultLabel.Content = num;
            else
                ResultLabel.Content = current + num;
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string op = button.Content.ToString();

            
            if (!double.TryParse(ResultLabel.Content.ToString(), out lastNumber))
            {
                MessageBox.Show("Invalid number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ResultLabel.Content = lastNumber;

            switch (op)
            {
                case "+": selectedOperator = SelectedOperator.Addition; break;
                case "-": selectedOperator = SelectedOperator.Subtraction; break;
                case "*": selectedOperator = SelectedOperator.Multiplication; break;
                case "/": selectedOperator = SelectedOperator.Division; break;
            }
        }
        private void AC_Click(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "";

            lastNumber = 0;
            result = 0;
            selectedOperator = SelectedOperator.Addition;
        }

        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out double number))
            {
                number = -number;
                ResultLabel.Content = number.ToString();
            }
        }
        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(ResultLabel.Content.ToString(), out double current))
                return;

            double percentValue;

            if (lastNumber != 0)
            {
                percentValue = (lastNumber * current) / 100.0;
            }
            else
            {
                percentValue = current / 100.0;
            }

            ResultLabel.Content = percentValue.ToString();
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            string text = ResultLabel.Content.ToString();
            if (!text.Contains("."))
            {
                ResultLabel.Content = text + ".";
            }
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(ResultLabel.Content.ToString(), out double newNumber))
            {
                MessageBox.Show("Invalid number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (selectedOperator)
            {
                case SelectedOperator.Addition:
                    result = MathService.Add(lastNumber, newNumber);
                    break;

                case SelectedOperator.Subtraction:
                    result = MathService.Subtract(lastNumber, newNumber);
                    break;

                case SelectedOperator.Multiplication:
                    result = MathService.Multiply(lastNumber, newNumber);
                    break;

                case SelectedOperator.Division:
                    if (newNumber == 0)
                    {
                        MessageBox.Show("You cannot divide by zero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        ResultLabel.Content = "";
                        return;
                    }
                    result = MathService.Divide(lastNumber, newNumber);
                    break;

                default:
                    result = newNumber;
                    break;
            }

            ResultLabel.Content = result.ToString();
        }
    }
}
