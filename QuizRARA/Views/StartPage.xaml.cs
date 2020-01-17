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
using TriviaGame.Models;

namespace TriviaGame
{

    public sealed partial class StartPage : Page
    {
        GameViewModel _gameViewModel;
        Game _game;
        public StartPage()
        {
            this.InitializeComponent();
            string[] h = new string[] {"Ester", "Love"};
            _game = new Game(h); //name,color
        }
       
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            //string text1 = redplayer.Text + "|" + greenplayer.Text;
            string players = redplayer.Text + "|" + greenplayer.Text;
            ComboBoxItem CBI = (ComboBoxItem)comboBoxNumber.SelectedItem;
            int antal = int.Parse(CBI.Content.ToString());
            if (antal >= 3)
            {
                players += "|" + blueplayer.Text;
            }
            if(antal == 4)
            {
                players += "|" + yellowplayer.Text;
            }
            
            this.Frame.Navigate(typeof(BoardPage),players);
        }

        private void AddPlayers(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            ComboBoxItem comboboxItem = (ComboBoxItem)combobox.SelectedItem;
            int num = int.Parse(comboboxItem.Content.ToString());

            player1.Visibility = Visibility.Visible;
            player2.Visibility = Visibility.Visible;
            player3.Visibility = Visibility.Visible;
            player4.Visibility = Visibility.Visible;

            if (num < 4)
            {
                player4.Visibility = Visibility.Collapsed;
            }
            if ( num < 3)
            {
                player3.Visibility = Visibility.Collapsed;
            }

        }
    }
}
