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
        ResultObject resultObject;
        GameViewModel gameViewModel;

        public BoardPage()
        {
            this.InitializeComponent();
            string[] name = new string[] { "E", "L", "J" };
            gameViewModel = new GameViewModel(name);
            ItemPlayer1.Visibility = Visibility.Visible;
            TextBlockPlayer1.Text = gameViewModel.game.Players[0].Name;
            ItemPlayer2.Visibility = Visibility.Visible;
            TextBlockPlayer2.Text = gameViewModel.game.Players[1].Name;
            ItemPlayer3.Visibility = Visibility.Visible;
            TextBlockPlayer3.Text = gameViewModel.game.Players[2].Name;
            // Hårdkodat De olika spelarna. Blajblaj
        }

        private void RollDice(object sender, RoutedEventArgs e)
        {
            gameViewModel.RollDice();
            Dice.Text = gameViewModel.game.Dice.ToString();
        }

        private async void GetQuestion(object sender, RoutedEventArgs e)
        {
            string category = "19";
            string difficulty = "easy";
            resultObject = await gameViewModel.CreateQuestion(category, difficulty);
            Dictionary<string, string> answers = new Dictionary<string, string> { };
            answers.Add(resultObject.Results[0].CorrectAnswer, "Correct");
            answers.Add(resultObject.Results[0].IncorrectAnswers[0], "Incorrect");
            answers.Add(resultObject.Results[0].IncorrectAnswers[1], "Incorrect");
            answers.Add(resultObject.Results[0].IncorrectAnswers[2], "Incorrect");

            asdf.Text = resultObject.Results[0].Question;
            asdf.Visibility = Visibility;
            string[] value;

            value = RandomValue(answers);
            a.Content = value[0];
            a.Visibility = Visibility;
            answers.Remove(value[0]);
            if (value[1] == "Correct")
                a.Tag = "Correct";
            else
                a.Tag = "";

            value = RandomValue(answers);
            s.Content = value[0];
            s.Visibility = Visibility;
            answers.Remove(value[0]);
            if (value[1] == "Correct")
                s.Tag = "Correct";
            else
                s.Tag = "";

            value = RandomValue(answers);
            d.Content = value[0];
            d.Visibility = Visibility;
            answers.Remove(value[0]);
            if (value[1] == "Correct")
                d.Tag = "Correct";
            else
                d.Tag = "";

            value = RandomValue(answers);
            f.Content = value[0];
            f.Visibility = Visibility;
            answers.Remove(value[0]);
            if (value[1] == "Correct")
                f.Tag = "Correct";
            else
                f.Tag = "";
        }

        public string[] RandomValue(Dictionary<string,string> dict)
        {
            Random rand = new Random();
            List<string> list = dict.Keys.ToList();
            string key = list[rand.Next(0, list.Count)];
            string[] KVP = { key, dict[key] };
            return KVP;
        }

        private void Answer(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            
            // Is it correct? Display if it is correct or not, also
            if (button.Tag.ToString() == "Correct")
            {
                button.Content = "Rätt";
            }
            else
            {
                button.Content = "Fel";
            }

            gameViewModel.Answer(9);
            // Spara resultatet, yes eller nej
            // Vems tur är det?
            // Har vi en vinnare?
            // Make ViewModel do this
        }
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StartPage));
        }
    }
}
