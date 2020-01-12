using TriviaGame.Models;
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

namespace TriviaGame
{
    public sealed partial class BoardPage : Page
    {
        ResultObject _resultObject;
        GameViewModel _gameViewModel;

        public BoardPage()
        {
            this.InitializeComponent();
        }

        private void RollDie(object sender, RoutedEventArgs e)
        {
            if (die.Text == "a")
            {
                die.Text = "b";
            }
            else
            {
                die.Text = "a";
            }
        }

        private void GetQuestion(object sender, RoutedEventArgs e)
        {
            _gameViewModel = new GameViewModel();
            _gameViewModel.CreateQuestion();
        }
    }
}
