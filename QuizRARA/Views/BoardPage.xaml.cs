using TriviaGame.Models;
using TriviaGame.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TriviaGame
{
    public sealed partial class BoardPage : Page
    {
        ResultObject _resultObject;
        GameViewModel _gameViewModel;

        public BoardPage()
        {
            this.InitializeComponent();
        }

        private void RollDie(object sender, RoutedEventArgs e)
        {
            if (die.Text == "a")
            {
                die.Text = "b";
            }
            else
            {
                die.Text = "a";
            }
        }

        private async void GetQuestion(object sender, RoutedEventArgs e)
        {
            string category = "12";
            string difficulty = "easy";
            _gameViewModel = new GameViewModel();
            _resultObject = await _gameViewModel.CreateQuestion(category, difficulty);
            List<string> answers = new List<string> {
                _resultObject.Results[0].CorrectAnswer + " RÄTT",
                _resultObject.Results[0].IncorrectAnswers[0],
                _resultObject.Results[0].IncorrectAnswers[1],
                _resultObject.Results[0].IncorrectAnswers[2] };
            asdf.Text = _resultObject.Results[0].Question;
            Random random = new Random();

            int index = random.Next(0, answers.Count);
            a.Content = answers[index];
            answers.RemoveAt(index);

            index = random.Next(0, answers.Count);
            s.Content = answers[index];
            answers.RemoveAt(index);

            index = random.Next(0, answers.Count);
            d.Content = answers[index];
            answers.RemoveAt(index);

            f.Content = answers[0];
        }
    }
}
