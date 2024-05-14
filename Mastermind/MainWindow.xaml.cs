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

namespace Mastermind
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string secretCode;
        private int attemptsLeft = 12;
        public MainWindow()
        {
            InitializeComponent();
            Restart();
        }
        private void Restart()
        {
            secretCode = SecretCode();
            GuessHistory.Items.Clear();
            ResultText.Text = string.Empty;
            attemptsLeft = 12;
        }
        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            if(attemptsLeft > 0)
            {
                string Guess = GuessInput.Text.Trim();
                string result = CheckGuess(Guess);
                if (Guess.Length == 4 && Guess.All(char.IsDigit))
                {
                    result = CheckGuess(Guess);
                    GuessHistory.Items.Insert(0, $"Guess: {Guess} Info: {result}");
                    GuessInput.Clear();
                    attemptsLeft--;
                }
                if(result == "1111")
                {
                    ResultText.Text = "Correct code!";
                    GuessInput.IsEnabled = false;
                }
                else if(attemptsLeft == 0)
                {
                    ResultText.Text = $"Du hast verloren, der code wäre {secretCode} gewessen";
                }
                else if(attemptsLeft>0)
                {
                    ResultText.Text = $"Attempts Left: {attemptsLeft}";
                }
                else
                {
                    ResultText.Text = "Not possible, the code must be of 4 digits.";
                }
            }
        }
        private string CheckGuess(string Guess)
        {
            int correctPosition = secretCode.Where((c, i) => c == Guess[i]).Count();
            int inWrongPosition = secretCode.Count(c => Guess.Contains(c)) - correctPosition;
            return new string('1', correctPosition) + new string('2', inWrongPosition);
        }
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Restart();
            GuessInput.IsEnabled = true;
        }
        private string SecretCode()
        {
            Random random = new Random();
            return string.Join("", Enumerable.Range(0, 4).Select(_ => random.Next(1, 7)));
        }
    }
}
