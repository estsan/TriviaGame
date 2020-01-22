using System;
using TriviaGame.Models;
using TriviaGame.ViewModels;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



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
       
        private void LetsPlay(object sender, RoutedEventArgs e)
        {
            
            //string text1 = redplayer.Text + "|" + greenplayer.Text;
            string players = redplayer.Text + "|" + greenplayer.Text;
            ComboBoxItem CBI = (ComboBoxItem)comboBoxNumber.SelectedItem;
            if(CBI!=null)
            {
                int antal = int.Parse(CBI.Content.ToString());
                if (antal >= 3)
                {
                    players += "|" + blueplayer.Text;
                }
                if (antal == 4)
                {
                    players += "|" + yellowplayer.Text;
                }

                this.Frame.Navigate(typeof(BoardPage), players);
            }
            else
            {
                MessageDialog dialog = new MessageDialog("You should select a player first", "Alert");
                dialog.ShowAsync();
            }
          
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
