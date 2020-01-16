using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int[] Position { get; set; }
        public bool Red { get; set; }
        public bool Green { get; set; }
        public bool Blue { get; set; }
        public bool Yellow { get; set; }
        public bool Pink { get; set; }
        public bool Purple { get; set; }

        public Player(string _name, string _color, int _numberOfPlayers, int _numberOfPlayer)
        {
            if (_numberOfPlayers == 2)
            {
                if (_numberOfPlayer == 0)
                {
                    Position = new int[2] { 1, 1 };
                }
                else if (_numberOfPlayer == 1)
                {
                    Position = new int[2] { 7, 7 };
                }

            }
            else if (_numberOfPlayers == 3)
            {
                if (_numberOfPlayer == 0)
                {
                    Position = new int[2] { 1, 1 };
                }
                else if (_numberOfPlayer == 1)
                {
                    Position = new int[2] { 3, 7 };
                }
                else if (_numberOfPlayer == 2)
                {
                    Position = new int[2] { 7, 3 };
                }
            }
            else if (_numberOfPlayers == 4)
            {
                if (_numberOfPlayer == 0)
                {
                    Position = new int[2] { 1, 1 };
                }
                else if (_numberOfPlayer == 1)
                {
                    Position = new int[2] { 1, 7 };
                }
                else if (_numberOfPlayer == 2)
                {
                    Position = new int[2] { 7, 1 };
                }
                else if (_numberOfPlayer == 3)
                {
                    Position = new int[2] { 7, 7 };
                }
            }

            Id = _numberOfPlayer;

            Red = false;
            Green = false;
            Blue = false;
            Yellow = false;
            Pink = false;
            Purple = false;

            Name = _name;

            Color = _color;
        }
    }
}
