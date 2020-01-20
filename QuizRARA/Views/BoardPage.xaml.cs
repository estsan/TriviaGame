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
            var param = (string)e.Parameter;
            string[] name = param.Split('|');
            gameViewModel = new GameViewModel(name);
            InitializeBoardSquares();

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
            int[] position = gameViewModel.game.WhosTurnIsIt.Position;
            BoardSquare boardSquare = boardSquares.Where(x => x.GridRow == position[0] && x.GridColumn == position[1]).First();
            string difficulty = "easy";
            resultObject = await gameViewModel.CreateQuestion(boardSquare.Category, difficulty);
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
            boardSquares = gameViewModel.boardSquares;
            BoardSquare boardSquare;

            boardSquare = boardSquares[0];
            Grid.SetRow(r11, boardSquare.GridRow);
            Grid.SetColumn(r11, boardSquare.GridColumn);
            r11.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[1];
            Grid.SetRow(r17, boardSquare.GridRow);
            Grid.SetColumn(r17, boardSquare.GridColumn);
            r17.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[2];
            Grid.SetRow(r71, boardSquare.GridRow);
            Grid.SetColumn(r71, boardSquare.GridColumn);
            r71.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[3];
            Grid.SetRow(r77, boardSquare.GridRow);
            Grid.SetColumn(r77, boardSquare.GridColumn);
            r77.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[4];
            Grid.SetRow(r12, boardSquare.GridRow);
            Grid.SetColumn(r12, boardSquare.GridColumn);
            r12.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[5];
            Grid.SetRow(r27, boardSquare.GridRow);
            Grid.SetColumn(r27, boardSquare.GridColumn);
            r27.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[6];
            Grid.SetRow(r61, boardSquare.GridRow);
            Grid.SetColumn(r61, boardSquare.GridColumn);
            r61.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[7];
            Grid.SetRow(r76, boardSquare.GridRow);
            Grid.SetColumn(r76, boardSquare.GridColumn);
            r76.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[8];
            Grid.SetRow(r13, boardSquare.GridRow);
            Grid.SetColumn(r13, boardSquare.GridColumn);
            r13.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[9];
            Grid.SetRow(r37, boardSquare.GridRow);
            Grid.SetColumn(r37, boardSquare.GridColumn);
            r37.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[10];
            Grid.SetRow(r51, boardSquare.GridRow);
            Grid.SetColumn(r51, boardSquare.GridColumn);
            r51.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[11];
            Grid.SetRow(r75, boardSquare.GridRow);
            Grid.SetColumn(r75, boardSquare.GridColumn);
            r75.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[12];
            Grid.SetRow(r14, boardSquare.GridRow);
            Grid.SetColumn(r14, boardSquare.GridColumn);
            r14.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[13];
            Grid.SetRow(r41, boardSquare.GridRow);
            Grid.SetColumn(r41, boardSquare.GridColumn);
            r41.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[14];
            Grid.SetRow(r47, boardSquare.GridRow);
            Grid.SetColumn(r47, boardSquare.GridColumn);
            r47.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[15];
            Grid.SetRow(r74, boardSquare.GridRow);
            Grid.SetColumn(r74, boardSquare.GridColumn);
            r74.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[16];
            Grid.SetRow(r15, boardSquare.GridRow);
            Grid.SetColumn(r15, boardSquare.GridColumn);
            r15.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[17];
            Grid.SetRow(r31, boardSquare.GridRow);
            Grid.SetColumn(r31, boardSquare.GridColumn);
            r31.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[18];
            Grid.SetRow(r57, boardSquare.GridRow);
            Grid.SetColumn(r57, boardSquare.GridColumn);
            r57.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[19];
            Grid.SetRow(r73, boardSquare.GridRow);
            Grid.SetColumn(r73, boardSquare.GridColumn);
            r73.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[20];
            Grid.SetRow(r16, boardSquare.GridRow);
            Grid.SetColumn(r16, boardSquare.GridColumn);
            r16.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[21];
            Grid.SetRow(r21, boardSquare.GridRow);
            Grid.SetColumn(r21, boardSquare.GridColumn);
            r21.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[22];
            Grid.SetRow(r67, boardSquare.GridRow);
            Grid.SetColumn(r67, boardSquare.GridColumn);
            r67.Stroke = boardSquare.Stroke;

            boardSquare = boardSquares[23];
            Grid.SetRow(r72, boardSquare.GridRow);
            Grid.SetColumn(r72, boardSquare.GridColumn);
            r72.Stroke = boardSquare.Stroke;
        }
    }
}
