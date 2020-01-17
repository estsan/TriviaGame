using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Models
{
    public class Game
    {
        public Player[] Players { get; set; }
        public int Dice { get; set; }
        public int NumberOfPlayers { get; set; }
        Player whosTurnIsIt { get; set; }
        public Player WhosTurnIsIt
        {
            get { return this.whosTurnIsIt; }
            set
            {
                int i = Array.IndexOf(Players, this.whosTurnIsIt) + 1;
                if (i < Players.Count())
                {
                    this.whosTurnIsIt = Players[i];
                }
                else
                {
                    this.whosTurnIsIt = Players[0];
                }
            }
        }

        public Game(string[] names)
        {
            NumberOfPlayers = names.Length;
            string[] colors = { "Red", "Green", "Blue", "Yellow" };
            Players = new Player[NumberOfPlayers];
            for (int i = 0; i < names.Length; i++)
            {
                Player p = new Player(names[i], colors[i], NumberOfPlayers, i);
                Players[i] = p;
            }

            WhosTurnIsIt = Players[0];
            Dice = 0;
        }

        internal void RollDice()
        {
            Random random = new Random();
            Dice = random.Next(1, 7);
        }
    }
}
