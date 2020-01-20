using TriviaGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizRARA.Models;
using Windows.UI.Popups;

namespace TriviaGame.ViewModels
{
    public class GameViewModel
    {
        public ResultObject resultObject;
        public Game game;
        public BoardSquare[] boardSquares;
        public bool IsCurrentAnswerCorrect;

        public GameViewModel(string[] name)
        {
            game = new Game(name);
            boardSquares = new BoardSquare[24];
            GenerateBoardSquares();
        }
        public void GenerateBoardSquares()
        {
            int[] position;
            BoardSquare boardSquare;

            position = new int[] { 1, 1 };
            boardSquare = new BoardSquare(position, "green");
            boardSquares[0] = boardSquare;

            position = new int[] { 1, 7 };
            boardSquare = new BoardSquare(position, "green");
            boardSquares[1] = boardSquare;

            position = new int[] { 7, 1 };
            boardSquare = new BoardSquare(position, "green");
            boardSquares[2] = boardSquare;

            position = new int[] { 7, 7 };
            boardSquare = new BoardSquare(position, "green");
            boardSquares[3] = boardSquare;

            position = new int[] { 1, 2 };
            boardSquare = new BoardSquare(position, "blue");
            boardSquares[4] = boardSquare;

            position = new int[] { 2, 7 };
            boardSquare = new BoardSquare(position, "blue");
            boardSquares[5] = boardSquare;

            position = new int[] { 6, 1 };
            boardSquare = new BoardSquare(position, "blue");
            boardSquares[6] = boardSquare;

            position = new int[] { 7, 6 };
            boardSquare = new BoardSquare(position, "blue");
            boardSquares[7] = boardSquare;

            position = new int[] { 1, 3 };
            boardSquare = new BoardSquare(position, "pink");
            boardSquares[8] = boardSquare;

            position = new int[] { 3, 7 };
            boardSquare = new BoardSquare(position, "pink");
            boardSquares[9] = boardSquare;

            position = new int[] { 5, 1 };
            boardSquare = new BoardSquare(position, "pink");
            boardSquares[10] = boardSquare;

            position = new int[] { 7, 5 };
            boardSquare = new BoardSquare(position, "pink");
            boardSquares[11] = boardSquare;

            position = new int[] { 1, 4 };
            boardSquare = new BoardSquare(position, "yellow");
            boardSquares[12] = boardSquare;

            position = new int[] { 4, 1 };
            boardSquare = new BoardSquare(position, "yellow");
            boardSquares[13] = boardSquare;

            position = new int[] { 4, 7 };
            boardSquare = new BoardSquare(position, "yellow");
            boardSquares[14] = boardSquare;

            position = new int[] { 7, 4 };
            boardSquare = new BoardSquare(position, "yellow");
            boardSquares[15] = boardSquare;

            position = new int[] { 1, 5 };
            boardSquare = new BoardSquare(position, "red");
            boardSquares[16] = boardSquare;

            position = new int[] { 3, 1 };
            boardSquare = new BoardSquare(position, "red");
            boardSquares[17] = boardSquare;

            position = new int[] { 5, 7 };
            boardSquare = new BoardSquare(position, "red");
            boardSquares[18] = boardSquare;

            position = new int[] { 7, 3 };
            boardSquare = new BoardSquare(position, "red");
            boardSquares[19] = boardSquare;

            position = new int[] { 1, 6 };
            boardSquare = new BoardSquare(position, "purple");
            boardSquares[20] = boardSquare;

            position = new int[] { 2, 1 };
            boardSquare = new BoardSquare(position, "purple");
            boardSquares[21] = boardSquare;

            position = new int[] { 6, 7 };
            boardSquare = new BoardSquare(position, "purple");
            boardSquares[22] = boardSquare;

            position = new int[] { 7, 2 };
            boardSquare = new BoardSquare(position, "purple");
            boardSquares[23] = boardSquare;

        }



        public async Task<ResultObject> CreateQuestion(int[] category, string difficulty)
        {
            resultObject = new ResultObject();
            await resultObject.CreateQuestion(category, difficulty);
            return resultObject;
        }

        internal void Answer(string answer)
        {
            if (answer == resultObject.Results[0].CorrectAnswer)
            {
                IsCurrentAnswerCorrect = true;
            }
            else
            {
                IsCurrentAnswerCorrect = false;
            }

            if(!IsCurrentAnswerCorrect)
            {
                game.WhosTurnIsIt = new Player("","",0,0);
            }
            else
            {
                // Nothing happens yet if the answer is correct.
            }

            if (game.WhosTurnIsIt.Green && game.WhosTurnIsIt.Red && game.WhosTurnIsIt.Blue && game.WhosTurnIsIt.Yellow && game.WhosTurnIsIt.Pink && game.WhosTurnIsIt.Purple)
            {
                MessageDialog dialog = new MessageDialog(game.WhosTurnIsIt.Name + ", WON", "WE HAVE A WINNER");
                dialog.ShowAsync();
            }
            // Spara resultatet, yes eller nej


            // Vems tur är det?
            //_game.WhosTurnIsIt++;

            // Har vi en vinnare?

        }

        internal void RollDice()
        {
            game.RollDice();
        }
    }
}
