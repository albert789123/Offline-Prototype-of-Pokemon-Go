using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pokemon.Model;
using Pokemon.Library.PokemonLibrary;
using System.Collections.ObjectModel;
using Pokemon.ViewModel;
using Pokemon.BgMusic;

namespace Pokemon
{
    /// <summary>
    /// CaptureWindow.xaml 的互動邏輯
    /// </summary>
    public partial class CaptureWindow : Window
    {
        public GameSession currentGame => DataContext as GameSession;
        private PokemonModel wildPokemon;
        private Button[] buttons;
        private CaptureMiniGame captureMiniGame;
        private Uri fileUri;
        private Image[] images;
        public CaptureWindow()
        {
            InitializeComponent();
        }

        public void CaptureInitialize(PokemonModel wildPokemon)
        {
            this.wildPokemon = wildPokemon;
            fileUri = new Uri("/Images/PokemonImages/" + wildPokemon.EvolveStage + ".png", UriKind.Relative);
            StartMiniGame(fileUri);
        }

        public void StartMiniGame(Uri fileUri)
        {
            BackButton.Visibility = Visibility.Hidden;
            buttons = new Button[16];
            images = new Image[16];
            for (int i = 0; i < 16; i++)
                buttons[i] = ButtonPanel.Children[i] as Button;
            for (int i = 0; i < 16; i++)
                images[i] = ImagePanel.Children[i] as Image;
            TextBlock chanceBlock = ChanceBlock;
            captureMiniGame = new CaptureMiniGame();
            StartGame();
        }

        public void StartGame()
        {
            for (int i = 0; i < 16; i++)
            {
                buttons[i].Click += delegate (object sender, RoutedEventArgs e) { OnMove(sender, e, BackButton); };
                buttons[i].Content = i;
            }
        }

        public void OnMove(object sender, RoutedEventArgs e, Button back)
        {
            Button button = sender as Button;
            button.Visibility = Visibility.Hidden;
            if (button.Content.ToString() == captureMiniGame.CorrectPlace.ToString() && captureMiniGame.chanceNum != 0)
            {
                images[captureMiniGame.CorrectPlace].Source = new BitmapImage(fileUri);
                MiniGameWin(back);
            }
            else { 
                captureMiniGame.chanceNum--;
                ChanceBlock.Text = "You have 5 chances. " + captureMiniGame.chanceNum + " times left.";
            }

            if (captureMiniGame.chanceNum == 0)
            {
                BGMPlayer.FailedCatch();
                ChanceBlock.Text = "Good luck next time!";
                MiniGameEnd(back);
            }
        }

        public void MiniGameWin(Button back)
        {
            BGMPlayer.SuccessfulCatch();
            ChanceBlock.Text = "Congratulations!";
            currentGame.CurrentPlayer.AddPokemon(wildPokemon);
            MiniGameEnd(back);
        }

        public void MiniGameEnd(Button back)
        {
            ButtonPanel.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Visible;
        }

        public class CaptureMiniGame
        {
            private Random rnd = new Random();
            public int chanceNum = 5;
            private int correctPlace;
            public int CorrectPlace
            {
                get { return correctPlace; }
                private set { correctPlace = value; }
            }
            public CaptureMiniGame()
            {
                correctPlace = rnd.Next(16);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
