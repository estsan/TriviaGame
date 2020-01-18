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
            // Hårdkodat De olika spelarna. Blajblaj

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var param = (string)e.Parameter;
            string[] name = param.Split('|');
            gameViewModel = new GameViewModel(name);


            ItemPlayer1.Visibility = Visibility.Visible;
            ItemPlayer2.Visibility = Visibility.Visible;

            Game game = gameViewModel.game;
            Player[] players = game.Players;
            SetPosition(players[0]);
            SetPosition(players[1]);
            if (game.NumberOfPlayers >= 3)
            {
                SetPosition(players[2]);
                ItemPlayer3.Visibility = Visibility.Visible;
            }
            if (game.NumberOfPlayers == 4)
            {
                SetPosition(players[3]);
                ItemPlayer4.Visibility = Visibility.Visible;
            }

            RollDicePlayer.Text = game.WhosTurnIsIt.Name.ToString() + ", It's your turn!";
        
        }
        private void RollDice(object sender, RoutedEventArgs e)
        {
            gameViewModel.RollDice();
            Dice.Text = gameViewModel.game.Dice.ToString();
        }

        private async void GetQuestion()
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

        public void SetPosition(Player p)
        {
            if (p.Color.ToString() == "Red")
            {
                Grid.SetRow(PiecePlayerRedB, p.Position[0]);
                Grid.SetColumn(PiecePlayerRedB, p.Position[1]);
                Grid.SetRow(PiecePlayerRedF, p.Position[0]);
                Grid.SetColumn(PiecePlayerRedF, p.Position[1]);
                PiecePlayerRedB.Visibility = Visibility.Visible;
                PiecePlayerRedF.Visibility = Visibility.Visible;
            }

            else if (p.Color.ToString() == "Green")
            {
                Grid.SetRow(PiecePlayerGreenB, p.Position[0]);
                Grid.SetColumn(PiecePlayerGreenB, p.Position[1]);
                Grid.SetRow(PiecePlayerGreenF, p.Position[0]);
                Grid.SetColumn(PiecePlayerGreenF, p.Position[1]);
                PiecePlayerGreenB.Visibility = Visibility.Visible;
                PiecePlayerGreenF.Visibility = Visibility.Visible;
            }

            else if (p.Color.ToString() == "Blue")
            {
                Grid.SetRow(PiecePlayerBlueB, p.Position[0]);
                Grid.SetColumn(PiecePlayerBlueB, p.Position[1]);
                Grid.SetRow(PiecePlayerBlueF, p.Position[0]);
                Grid.SetColumn(PiecePlayerBlueF, p.Position[1]);
                PiecePlayerBlueB.Visibility = Visibility.Visible;
                PiecePlayerBlueF.Visibility = Visibility.Visible;
            }

            else if (p.Color.ToString() == "Yellow")
            {
                Grid.SetRow(PiecePlayerYellowB, p.Position[0]);
                Grid.SetColumn(PiecePlayerYellowB, p.Position[1]);
                Grid.SetRow(PiecePlayerYellowF, p.Position[0]);
                Grid.SetColumn(PiecePlayerYellowF, p.Position[1]);
                PiecePlayerYellowB.Visibility = Visibility.Visible;
                PiecePlayerYellowF.Visibility = Visibility.Visible;
            }
        }

        private void Step(object sender, RoutedEventArgs e)
        {
            Player player = gameViewModel.game.WhosTurnIsIt;
            // Flytta pjäsen, ta reda på färgen
            // skicka efter fråga
            int dice = gameViewModel.game.Dice;
            int row = player.Position[0];
            int col = player.Position[1];
            if (row == 1)
            {
                if (col + dice <= 7)
                {
                    player.Position[1] = col + dice;
                }
                else
                {
                    player.Position[0] = col + dice - 6;
                    player.Position[1] = 7;
                }
            }
            else if (row == 7)
            {
                if (col - dice >= 1)
                {
                    player.Position[1] = col - dice;
                }
                else
                {
                    player.Position[0] = col - dice + 6;
                    player.Position[1] = 1;
                }
            }
            else if (col == 1)
            {
                if (row - dice >= 1)
                {
                    player.Position[0] = row - dice;
                }
                else
                {
                    player.Position[0] = 1;
                    player.Position[1] = row - dice + 6;
                }
            }
            else if (col == 7)
            {
                if (row + dice <= 7)
                {
                    player.Position[0] = row + dice;
                }
                else
                {
                    player.Position[0] = 7;
                    player.Position[1] = row + dice - 6;
                }
            }
            SetPosition(player);
            GetQuestion();
        }
    }
}
