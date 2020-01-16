using TriviaGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.ViewModels
{
    public class GameViewModel
    {
        public ResultObject resultObject;
        public Game game;

        public GameViewModel()
        {
            game = new Game(new string[] { "Alice", "Bob", "Eve"});
        }
        public async Task<ResultObject> CreateQuestion(string category, string difficulty)
        {
            resultObject = new ResultObject();
            await resultObject.CreateQuestion(category, difficulty);
            return resultObject;
        }

        internal void Answer(int category)
        {
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
