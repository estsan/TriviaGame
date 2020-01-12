using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Models
{
    class Game
    {
        public List<Player> Players { get; set; }
        public int Die { get; set; }
        public int NumberOfPlayers { get; set; }

        public Game(string[] _names)
        {
            NumberOfPlayers = _names.Length;
            int i = 0;
            foreach (string _name in _names)
            {
                string[] player = _name.Split(',');
                Player p = new Player(player[0].Trim(), player[1].Trim(), NumberOfPlayers, i);
                Players.Add(p);
                i++;
            }

            Die = 0;

        }
    }
}
