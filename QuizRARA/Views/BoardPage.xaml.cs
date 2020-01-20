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
using QuizRARA.Models;

namespace TriviaGame
{
    public sealed partial class BoardPage : Page
    {
        ResultObject resultObject;
        GameViewModel gameViewModel;
        BoardSquare[] boardSquares; 

        public BoardPage()
        {
            this.InitializeComponent();
            // Hårdkodat De olika spelarna. Blajblaj

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitializeBoardSquares();
            var param = (string)e.Parameter;
            string[] name = param.Split('|');
            gameViewModel = new GameViewModel(name);

            ItemPlayer1.Visibility = Visibility.Visible;
            ItemPlayer2.Visibility = Visibility.Visible;
            TextBlockPlayer1.Text = name[0];
            TextBlockPlayer2.Text = name[1];
           
            Game game = gameViewModel.game;
            Player[] players = game.Players;
            SetPosition(players[0]);
            SetPosition(players[1]);
            if (game.NumberOfPlayers >= 3)
            {
                SetPosition(players[2]);
                ItemPlayer3.Visibility = Visibility.Visible;
                TextBlockPlayer3.Text = name[2];
            }
            if (game.NumberOfPlayers == 4)
            {
                SetPosition(players[3]);
                ItemPlayer4.Visibility = Visibility.Visible;
                TextBlockPlayer4.Text = name[3];
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
            int[] pos = gameViewModel.game.WhosTurnIsIt.Position;
            BoardSquare bS = boardSquares.Where(x => x.GridRow == pos[0] && x.GridColumn == pos[1]).First();
            string difficulty = "easy";
            resultObject = await gameViewModel.CreateQuestion(bS.Category, difficulty);
            Dictionary<string, string> answers = new Dictionary<string, string> { };
            answers.Add(resultObject.Results[0].CorrectAnswer, "Correct");
            answers.Add(resultObject.Results[0].IncorrectAnswers[0], "Incorrect");
            answers.Add(resultObject.Results[0].IncorrectAnswers[1], "Incorrect");
            answers.Add(resultObject.Results[0].IncorrectAnswers[2], "Incorrect");
            
            //categoryblock.Text = categoryblock.Text+category;

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

        private void InitializeBoardSquares()
        {
            boardSquares = new BoardSquare[24];
            int[] position;
            BoardSquare boardSquare;

            position = new int[] { 1, 1 };
            boardSquare = new BoardSquare(position, "green");
            boardSquares[0] = boardSquare;
            Grid.SetRow(r11, position[0]);
            Grid.SetColumn(r11, position[1]);
            r11.Stroke = boardSquare.Stroke;

            position = new int[] { 1, 7 };
            boardSquare = new BoardSquare(position, "green");
            boardSquares[1] = boardSquare;
            Grid.SetRow(r17, position[0]);
            Grid.SetColumn(r17, position[1]);
            r17.Stroke = boardSquare.Stroke;

            position = new int[] { 7, 1 };
            boardSquare = new BoardSquare(position, "green");
            boardSquares[2] = boardSquare;
            Grid.SetRow(r71, position[0]);
            Grid.SetColumn(r71, position[1]);
            r71.Stroke = boardSquare.Stroke;

            position = new int[] { 7, 7 };
            boardSquare = new BoardSquare(position, "green");
            boardSquares[3] = boardSquare;
            Grid.SetRow(r77, position[0]);
            Grid.SetColumn(r77, position[1]);
            r77.Stroke = boardSquare.Stroke;

            position = new int[] { 1, 2 };
            boardSquare = new BoardSquare(position, "blue");
            boardSquares[4] = boardSquare;
            Grid.SetRow(r12, position[0]);
            Grid.SetColumn(r12, position[1]);
            r12.Stroke = boardSquare.Stroke;

            position = new int[] { 2, 7 };
            boardSquare = new BoardSquare(position, "blue");
            boardSquares[5] = boardSquare;
            Grid.SetRow(r27, position[0]);
            Grid.SetColumn(r27, position[1]);
            r27.Stroke = boardSquare.Stroke;

            position = new int[] { 6, 1 };
            boardSquare = new BoardSquare(position, "blue");
            boardSquares[6] = boardSquare;
            Grid.SetRow(r61, position[0]);
            Grid.SetColumn(r61, position[1]);
            r61.Stroke = boardSquare.Stroke;

            position = new int[] { 7, 6 };
            boardSquare = new BoardSquare(position, "blue");
            boardSquares[7] = boardSquare;
            Grid.SetRow(r76, position[0]);
            Grid.SetColumn(r76, position[1]);
            r76.Stroke = boardSquare.Stroke;

            position = new int[] { 1, 3 };
            boardSquare = new BoardSquare(position, "pink");
            boardSquares[8] = boardSquare;
            Grid.SetRow(r13, position[0]);
            Grid.SetColumn(r13, position[1]);
            r13.Stroke = boardSquare.Stroke;

            position = new int[] { 3, 7 };
            boardSquare = new BoardSquare(position, "pink");
            boardSquares[9] = boardSquare;
            Grid.SetRow(r37, position[0]);
            Grid.SetColumn(r37, position[1]);
            r37.Stroke = boardSquare.Stroke;

            position = new int[] { 5, 1 };
            boardSquare = new BoardSquare(position, "pink");
            boardSquares[10] = boardSquare;
            Grid.SetRow(r51, position[0]);
            Grid.SetColumn(r51, position[1]);
            r51.Stroke = boardSquare.Stroke;

            position = new int[] { 7, 5 };
            boardSquare = new BoardSquare(position, "pink");
            boardSquares[11] = boardSquare;
            Grid.SetRow(r75, position[0]);
            Grid.SetColumn(r75, position[1]);
            r75.Stroke = boardSquare.Stroke;

            position = new int[] { 1, 4 };
            boardSquare = new BoardSquare(position, "yellow");
            boardSquares[12] = boardSquare;
            Grid.SetRow(r14, position[0]);
            Grid.SetColumn(r14, position[1]);
            r14.Stroke = boardSquare.Stroke;

            position = new int[] { 4, 1 };
            boardSquare = new BoardSquare(position, "yellow");
            boardSquares[13] = boardSquare;
            Grid.SetRow(r41, position[0]);
            Grid.SetColumn(r41, position[1]);
            r41.Stroke = boardSquare.Stroke;

            position = new int[] { 4, 7 };
            boardSquare = new BoardSquare(position, "yellow");
            boardSquares[14] = boardSquare;
            Grid.SetRow(r47, position[0]);
            Grid.SetColumn(r47, position[1]);
            r47.Stroke = boardSquare.Stroke;

            position = new int[] { 7, 4 };
            boardSquare = new BoardSquare(position, "yellow");
            boardSquares[15] = boardSquare;
            Grid.SetRow(r74, position[0]);
            Grid.SetColumn(r74, position[1]);
            r74.Stroke = boardSquare.Stroke;

            position = new int[] { 1, 5 };
            boardSquare = new BoardSquare(position, "red");
            boardSquares[16] = boardSquare;
            Grid.SetRow(r15, position[0]);
            Grid.SetColumn(r15, position[1]);
            r15.Stroke = boardSquare.Stroke;

            position = new int[] { 3, 1 };
            boardSquare = new BoardSquare(position, "red");
            boardSquares[17] = boardSquare;
            Grid.SetRow(r31, position[0]);
            Grid.SetColumn(r31, position[1]);
            r31.Stroke = boardSquare.Stroke;

            position = new int[] { 5, 7 };
            boardSquare = new BoardSquare(position, "red");
            boardSquares[18] = boardSquare;
            Grid.SetRow(r57, position[0]);
            Grid.SetColumn(r57, position[1]);
            r57.Stroke = boardSquare.Stroke;

            position = new int[] { 7, 3 };
            boardSquare = new BoardSquare(position, "red");
            boardSquares[19] = boardSquare;
            Grid.SetRow(r73, position[0]);
            Grid.SetColumn(r73, position[1]);
            r73.Stroke = boardSquare.Stroke;

            position = new int[] { 1, 6 };
            boardSquare = new BoardSquare(position, "purple");
            boardSquares[20] = boardSquare;
            Grid.SetRow(r16, position[0]);
            Grid.SetColumn(r16, position[1]);
            r16.Stroke = boardSquare.Stroke;

            position = new int[] { 2, 1 };
            boardSquare = new BoardSquare(position, "purple");
            boardSquares[21] = boardSquare;
            Grid.SetRow(r21, position[0]);
            Grid.SetColumn(r21, position[1]);
            r21.Stroke = boardSquare.Stroke;

            position = new int[] { 6, 7 };
            boardSquare = new BoardSquare(position, "purple");
            boardSquares[22] = boardSquare;
            Grid.SetRow(r67, position[0]);
            Grid.SetColumn(r67, position[1]);
            r67.Stroke = boardSquare.Stroke;

            position = new int[] { 7, 2 };
            boardSquare = new BoardSquare(position, "purple");
            boardSquares[23] = boardSquare;
            Grid.SetRow(r72, position[0]);
            Grid.SetColumn(r72, position[1]);
            r72.Stroke = boardSquare.Stroke;
        }
    }
}
