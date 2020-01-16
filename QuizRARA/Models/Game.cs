using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Models
{
    public class Game
    {
        public Dictionary<int, Player> Players { get; set; }
        public int Dice { get; set; }
        public int NumberOfPlayers { get; set; }
        int _whosTurnIsIt { get; set; }
        public int WhosTurnIsIt
        {
            get { return this._whosTurnIsIt; }
            set
            {
                if (_whosTurnIsIt <= NumberOfPlayers) { _whosTurnIsIt = 0; }
                else { _whosTurnIsIt++; }
            }
        }

        public Game(string[] names)
        {
            NumberOfPlayers = names.Length;
            string[] colors = { "Red", "Green", "Blue", "Yellow" };
            for (int i = 0; i < names.Length; i++)
            {
                Player p = new Player(names[i], colors[i], NumberOfPlayers, i);
                Players = new Dictionary<int, Player>() { };
                Players.Add(i, p);
                i++;
            }

            WhosTurnIsIt = 0;
            Dice = 0;
        }

        internal void RollDice()
        {
            Random random = new Random();
            Dice = random.Next(1, 7);
        }
    }
}
