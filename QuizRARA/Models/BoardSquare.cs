using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace QuizRARA.Models
{
    public class BoardSquare
    {
        public int GridRow { get; set; }
        public int GridColumn { get; set; }
        public SolidColorBrush Stroke { get; set; }
        public int[] Category { get; set; }

        public BoardSquare(int[] position, string color)
        {
            GridRow = position[0];
            GridColumn = position[1];

            //Geography
            if (color.ToLower() == "green")
            {
                Category = new int[] { 22 };
                Stroke = new SolidColorBrush(Colors.Green);
            }
            // Animals, science or nature
            else if (color.ToLower() == "red")
            {
                Category = new int[] { 27, 17 };
                Stroke = new SolidColorBrush(Colors.Red);
            }
            // Books, film or music
            else if (color.ToLower() == "purple")
            {
                Category = new int[] { 10, 11, 12 };
                Stroke = new SolidColorBrush(Colors.Purple);
            }
            // Celebrities
            else if (color.ToLower() == "pink")
            {
                Category = new int[] { 26 };
                Stroke = new SolidColorBrush(Colors.HotPink);
            }
            // Mythology
            else if (color.ToLower() == "yellow")
            {
                Category = new int[] { 20 };
                Stroke = new SolidColorBrush(Colors.DarkGoldenrod);
            }
            // General Knowledge
            if (color.ToLower() == "blue")
            {
                Category = new int[] { 9 };
                Stroke = new SolidColorBrush(Colors.Blue);
            }
        }
    }
}
