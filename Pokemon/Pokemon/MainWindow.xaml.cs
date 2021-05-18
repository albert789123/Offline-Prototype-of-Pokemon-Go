using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Pokemon.BgMusic;
using Pokemon.Model;
using Pokemon.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using Pokemon.Library.PokemonLibrary;

namespace Pokemon
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mediaPlayMute = false; //Default music not mute
        private GameSession _gameSession;
        private bool newGame = true;


        //Player image and movement
        Image playerImage = new Image();
        Point playerPosition; //Keep track of users; position

        public MainWindow()
        {
            InitializeComponent();
            BGMPlayer.MainWindowMusic();
            mainCanvas.Focus();

        }

        public void CreateGame(PlayerModel newPlayer)
        {
            if (newGame) //Can only be used once
            {
                newGame = false;
                _gameSession = new GameSession(newPlayer);
                DataContext = _gameSession; // Link game session detail to WPF
                playerPosition = new Point(0, 0);
                mapGenerator(_gameSession.MapNumber);
            }
        }

        private void mapGenerator(int mapNum)
        {
            mainCanvas.Children.Clear();
            UniformGrid baseGrid = new UniformGrid();
            double columnWidth = mainCanvas.Width / 10;
            double rowHeight = mainCanvas.Height / 10;
            baseGrid.Columns = 10;
            baseGrid.Rows = 10;
            baseGrid.Width = columnWidth * 10;
            baseGrid.Height = rowHeight * 10;

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Image img = new Image();
                    string pathToBaseImage = string.Format("/Images/Location/rpgTile" + _gameSession.WorldMap[mapNum - 1, x, y].MapImage + ".png");
                    Uri uriToBaseImage = new Uri(pathToBaseImage, UriKind.Relative);
                    img.Source = new BitmapImage(uriToBaseImage);
                    baseGrid.Children.Add(img);
                }
            }
            Canvas.SetLeft(baseGrid, (mainCanvas.Width - baseGrid.Width) / 2); // Restrict 1 column
            Canvas.SetTop(baseGrid, (mainCanvas.Height - baseGrid.Height) / 2); // Restrict 1 column
            mainCanvas.Children.Add(baseGrid);


            UniformGrid placesGrid = new UniformGrid();
            placesGrid.Columns = 10;
            placesGrid.Rows = 10;
            placesGrid.Width = columnWidth * 10;
            placesGrid.Height = rowHeight * 10;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (_gameSession.WorldMap[mapNum - 1, x, y].TradeAvailable)
                    {

                        Image traderImg = new Image();
                        string pathToTraderImage = string.Format(_gameSession.WorldMap[mapNum - 1, x, y].TradeImage);
                        Uri uriToTraderImage = new Uri(pathToTraderImage, UriKind.Relative);
                        traderImg.Source = new BitmapImage(uriToTraderImage);
                        placesGrid.Children.Add(traderImg);

                    }
                    else if (_gameSession.WorldMap[mapNum - 1, x, y].GymBattleAvailable)
                    {

                        Image gymImg = new Image();
                        string pathToGymImage = string.Format(_gameSession.WorldMap[mapNum - 1, x, y].GymBattleImage);
                        Uri uriToGymImage = new Uri(pathToGymImage, UriKind.Relative);
                        gymImg.Source = new BitmapImage(uriToGymImage);
                        placesGrid.Children.Add(gymImg);
                    }
                    else if (_gameSession.WorldMap[mapNum - 1, x, y].PokemonAvailable)
                    {
                        Image pokemonImg = new Image();
                        string pathToPokemonImage = string.Format("/Images/PokemonImages/" + _gameSession.WorldMap[mapNum - 1, x, y].PokemonChosen.EvolveStage + ".png");
                        Uri uriToGymImage = new Uri(pathToPokemonImage, UriKind.Relative);
                        pokemonImg.Source = new BitmapImage(uriToGymImage);
                        placesGrid.Children.Add(pokemonImg);
                    }
                    else
                    {
                        placesGrid.Children.Add(new Image());
                    }
                }
            }
            Canvas.SetLeft(placesGrid, (mainCanvas.Width - placesGrid.Width) / 2); // Restrict 1 column
            Canvas.SetTop(placesGrid, (mainCanvas.Height - placesGrid.Height) / 2); // Restrict 1 column
            mainCanvas.Children.Add(placesGrid);

            playerImage = new Image();
            playerImage.Source = new BitmapImage(new Uri("/Images/Player/ash_back.png", UriKind.Relative));
            Canvas.SetLeft(playerImage, mainCanvas.Width / 2);
            Canvas.SetTop(playerImage, mainCanvas.Height / 2);
            mainCanvas.Children.Add(playerImage);

            for(int i = 1; i < 5; i++)
            {
                if (i == mapNum)
                {
                    Rectangle tmp = (Rectangle)this.FindName("mapBox" + i);
                    tmp.Fill = new SolidColorBrush(Colors.LightGreen);
                }
                else
                {
                    Rectangle tmp = (Rectangle)this.FindName("mapBox" + i);
                    tmp.Fill = new SolidColorBrush(Colors.Green);
                }
            }
        }

        private void PlayerGenerator(Point pos, bool horizontal, bool vertical)
        {
            TranslateTransform trans = new TranslateTransform();
            playerImage.RenderTransform = trans;
            if (horizontal && vertical)
            {
                throw new Exception("Cannot be both horizontal movement and vertical movement.");
            }

            if (vertical)
            {
                if (pos.Y < 0)
                {
                    DoubleAnimation anim1 = new DoubleAnimation()
                    {
                        From = -400,
                        To = (pos.Y),
                        Duration = TimeSpan.FromSeconds(0.2)
                    };
                    Image img = new Image();
                    string pathToImage = string.Format("/Images/Player/ash_back.png");
                    Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                    playerImage.Source = new BitmapImage(uriToImage);
                    trans.BeginAnimation(TranslateTransform.YProperty, anim1);

                    playerPosition.Y = pos.Y;
                }
                else
                {
                    DoubleAnimation anim1 = new DoubleAnimation()
                    {
                        From = 400,
                        To = (pos.Y),
                        Duration = TimeSpan.FromSeconds(0.2)
                    };
                    Image img = new Image();
                    string pathToImage = string.Format("/Images/Player/ash_front.png");
                    Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                    playerImage.Source = new BitmapImage(uriToImage);
                    trans.BeginAnimation(TranslateTransform.YProperty, anim1);

                    playerPosition.Y = pos.Y;
                }

            }
            else if (horizontal)
            {
                if (pos.X < 0)
                {
                    DoubleAnimation anim2 = new DoubleAnimation()
                    {
                        From = -400,
                        To = (pos.X),
                        Duration = TimeSpan.FromSeconds(0.2)
                    };
                    Image img = new Image();
                    string pathToImage = string.Format("/Images/Player/ash_right.png");
                    Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                    playerImage.Source = new BitmapImage(uriToImage);
                    trans.BeginAnimation(TranslateTransform.XProperty, anim2);

                    playerPosition.X = pos.X;
                }
                else
                {
                    DoubleAnimation anim2 = new DoubleAnimation()
                    {
                        From = 400,
                        To = (pos.X),
                        Duration = TimeSpan.FromSeconds(0.2)
                    };
                    Image img = new Image();
                    string pathToImage = string.Format("/Images/Player/ash_left.png");
                    Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                    playerImage.Source = new BitmapImage(uriToImage);
                    trans.BeginAnimation(TranslateTransform.XProperty, anim2);

                    playerPosition.X = pos.X;
                }

            }
        }

        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point P = Mouse.GetPosition(mainCanvas);
            // Origin 0,0 in the mainCanvas center and place the player to the cursor center
            P.Y = P.Y - 400 - playerImage.ActualWidth / 2;
            P.X = P.X - 400 - playerImage.ActualHeight / 2;


            //Prevent player goes into the boundary ( Bottom + Right )
            /*
            if (P.X + playerImage.ActualWidth > 400)
                P.X -= playerImage.ActualWidth;
            else 
            */
            if (P.X - playerImage.ActualWidth < -400)
                P.X += playerImage.ActualWidth;
            /*
            if (P.Y + playerImage.ActualHeight > 400)
                P.Y -= playerImage.ActualHeight;
            else if (P.Y - playerImage.ActualHeight < -400)
                P.Y += playerImage.ActualHeight;
            */


            TranslateTransform trans = new TranslateTransform();
            playerImage.RenderTransform = trans;

            DoubleAnimation anim1 = new DoubleAnimation()
            {
                From = playerPosition.Y,
                To = (P.Y),
                Duration = TimeSpan.FromSeconds(0.2)
            };
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);

            DoubleAnimation anim2 = new DoubleAnimation()
            {
                From = playerPosition.X,
                To = (P.X),
                Duration = TimeSpan.FromSeconds(0.2)
            };
            if (Math.Abs(P.X - playerPosition.X) > Math.Abs(P.Y - playerPosition.Y))
            {
                if (P.X > playerPosition.X) // Right
                {
                    Image img = new Image();
                    string pathToImage = string.Format("/Images/Player/ash_right.png");
                    Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                    playerImage.Source = new BitmapImage(uriToImage);

                }
                else //Left
                {
                    Image img = new Image();
                    string pathToImage = string.Format("/Images/Player/ash_Left.png");
                    Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                    playerImage.Source = new BitmapImage(uriToImage);
                }
            }
            else
            {
                if (P.Y > playerPosition.Y) // Right
                {
                    Image img = new Image();
                    string pathToImage = string.Format("/Images/Player/ash_back.png");
                    Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                    playerImage.Source = new BitmapImage(uriToImage);

                }
                else //Left
                {
                    Image img = new Image();
                    string pathToImage = string.Format("/Images/Player/ash_front.png");
                    Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                    playerImage.Source = new BitmapImage(uriToImage);
                }
            }
            playerPosition.X = P.X;
            playerPosition.Y = P.Y;
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);

            // Update Player latest Position and update map
            LocationUpdate(playerPosition);

        }

        private void LocationUpdate(Point playerPosition)
        {
            // Firstly, check if player is intended to leave the map
            if (playerPosition.X < -375)
            {
                if (_gameSession.CurrentLocation.CanGoLeft)
                {
                    int newMapNumber = _gameSession.MapDirection[_gameSession.MapNumber].Left.MapNumber;
                    playerPosition.X = 350;
                    _gameSession.UpdatePlayerPosition(newMapNumber, playerPosition);
                    mapGenerator(_gameSession.MapNumber);
                    PlayerGenerator(playerPosition, true, false);

                }
            }
            else if (playerPosition.X > 365)
            {
                if (_gameSession.CurrentLocation.CanGoRight)
                {
                    int newMapNumber = _gameSession.MapDirection[_gameSession.MapNumber].Right.MapNumber;
                    playerPosition.X = -360;
                    _gameSession.UpdatePlayerPosition(newMapNumber, playerPosition);
                    mapGenerator(_gameSession.MapNumber);
                    PlayerGenerator(playerPosition, true, false);
                }
            }
            else if (playerPosition.Y > 370)
            {
                if (_gameSession.CurrentLocation.CanGoDown)
                {
                    int newMapNumber = _gameSession.MapDirection[_gameSession.MapNumber].Down.MapNumber;
                    playerPosition.Y = -360;
                    _gameSession.UpdatePlayerPosition(newMapNumber, playerPosition);
                    mapGenerator(_gameSession.MapNumber);
                    PlayerGenerator(playerPosition, false, true);
                }
            }
            else if (playerPosition.Y < -370)
            {
                if (_gameSession.CurrentLocation.CanGoUp)
                {
                    int newMapNumber = _gameSession.MapDirection[_gameSession.MapNumber].Up.MapNumber;
                    playerPosition.Y = 360;
                    _gameSession.UpdatePlayerPosition(newMapNumber, playerPosition);
                    mapGenerator(_gameSession.MapNumber);
                    PlayerGenerator(playerPosition, false, true);
                }
            }
            else
            {
                // Then, check if the player is standing on a grid with special event
                _gameSession.UpdatePlayerPosition(_gameSession.MapNumber, playerPosition);
                if (_gameSession.CurrentLocation.PokemonHere)
                {
                    catchPokemonBtn.Visibility = Visibility.Visible;
                    gymBattleBtn.Visibility = Visibility.Hidden;
                }
                else if (_gameSession.CurrentLocation.GymBattleHere)
                {
                    catchPokemonBtn.Visibility = Visibility.Hidden;
                    gymBattleBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    catchPokemonBtn.Visibility = Visibility.Hidden;
                    gymBattleBtn.Visibility = Visibility.Hidden;
                }
            }


        }

        private void muteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!mediaPlayMute) // Music to be muted
            {
                mediaPlayMute = true;
                Image img = new Image();
                string pathToImage = string.Format("/Images/Buttons/musicMute.png");
                Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                muteBtn_Image.Source = new BitmapImage(uriToImage);

                BGMPlayer.IsMusicMute(true);
            }
            else
            {
                mediaPlayMute = false;
                Image img = new Image();
                string pathToImage = string.Format("/Images/Buttons/musicPlay.png");
                Uri uriToImage = new Uri(pathToImage, UriKind.Relative);
                muteBtn_Image.Source = new BitmapImage(uriToImage);

                BGMPlayer.IsMusicMute(false);
            }
        }

        private void catchPokemonBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_gameSession.CurrentPlayer.CollectedPokemon.Count >= 6)
            {
                MessageBox.Show("You have reached the maximum number of pokemon! " +
                    "Please abandon some before you want to catch more QAQ. (Do not abandon pets in real life!! )");
                return;
            }
            else {
                if (mainCanvas.Visibility == Visibility.Visible)
                {
                    if (_gameSession.CurrentLocation.PokemonHere)
                    {
                        int rowGrid = _gameSession.CurrentLocation.RowPos;
                        int colGrid = _gameSession.CurrentLocation.ColPos;

                        PokemonModel targetPokemon = _gameSession.CurrentLocation.CatchPokemon;
                        CaptureWindow captureWindow = new CaptureWindow();
                        captureWindow.DataContext = _gameSession;
                        captureWindow.CaptureInitialize(targetPokemon);
                        captureWindow.Show();

                        //Stop game when ongoing
                        mainCanvas.Visibility = Visibility.Hidden;
                        captureWindow.Closed += new EventHandler(HandleMainWindow); ;
                    }
                } else
                {
                    MessageBox.Show("Finish the game first, please");
                }
            }
        }

        private void gymBattleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainCanvas.Visibility == Visibility.Visible)
            {
                if (_gameSession.CurrentLocation.GymBattleHere)
                {
                    int rowGrid = _gameSession.CurrentLocation.RowPos;
                    int colGrid = _gameSession.CurrentLocation.ColPos;

                    GymBattleWindow gymBattleWindow = new GymBattleWindow();
                    gymBattleWindow.DataContext = _gameSession;
                    gymBattleWindow.GymBattleInitialize();
                    BGMPlayer.GymBattleMusic();
                    gymBattleWindow.Show();

                    //Stop game when ongoing
                    mainCanvas.Visibility = Visibility.Hidden;


                    gymBattleWindow.Closed += new EventHandler(HandleMainWindow);
                }
            } else
            {
                MessageBox.Show("Finish the Gym Battle first, please");
            }
        }

        private void HandleMainWindow(object sender, EventArgs e)
        {
            BGMPlayer.StopSfxPlayer();
            if (!mediaPlayMute)
            {
                BGMPlayer.SFX_Ended();
            }
            if(BGMPlayer.tmpPlayer != BGMPlayer.MainWindowMusic)
            {
                BGMPlayer.MainWindowMusic();
            }
            mainCanvas.Visibility = Visibility.Visible;
        }

        private void mangePokemonBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageWindow manageWindow = new ManageWindow();
            manageWindow.DataContext = _gameSession;
            manageWindow.ManageInitialize();
            manageWindow.Show();

            //Stop game when ongoing
            mainCanvas.Visibility = Visibility.Hidden;
            manageWindow.Closed += new EventHandler(HandleMainWindow); ;
        }
    }
}