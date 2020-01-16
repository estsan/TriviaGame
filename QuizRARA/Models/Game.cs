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
        public int Die { get; set; }
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

        public Game(string[] _names)
        {
            NumberOfPlayers = _names.Length;
            int i = 0;
            foreach (string _name in _names)
            {
                string[] player = _name.Split(',');
                Player p = new Player(player[0].Trim(), player[1].Trim(), NumberOfPlayers, i);
                Players = new Dictionary<int, Player>() { };
                Players.Add(i, p);
                i++;
            }

            WhosTurnIsIt = 0;
            Die = 0;
        }
    }
}
