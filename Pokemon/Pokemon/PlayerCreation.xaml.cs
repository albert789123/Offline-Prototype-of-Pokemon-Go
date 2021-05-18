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
using Pokemon.BgMusic;

namespace Pokemon
{
    /// <summary>
    /// PlayerCreation.xaml 的互動邏輯
    /// </summary>
    public partial class PlayerCreation : Window
    {
        private Dictionary<string,PokemonModel> availablePokemon;
        private PlayerModel newPlayer;

        public PlayerCreation()
        {
            InitializeComponent();
            BGMPlayer.TitleScreenMusic();
            ObservableCollection<string> tmpList = new ObservableCollection<string>();
             availablePokemon = new Dictionary<string, PokemonModel>();
            while (availablePokemon.Count < 3)
            {
                PokemonModel tmp = PokemonFactory.Spawn();
                if (!availablePokemon.ContainsKey(tmp.NickName)) {
                    availablePokemon.Add(tmp.NickName,tmp);
                    tmpList.Add(tmp.NickName);
                }
            }
            FirstPokemon.ItemsSource = tmpList;
        }

        private void FirstPokemon_Selected(object sender, RoutedEventArgs e)
        {
            string selectedPokemon = FirstPokemon.SelectedItem.ToString();
            if (!selectedPokemon.Equals(""))
            {
                Uri fileUri = new Uri("/Images/PokemonImages/" + selectedPokemon +".png", UriKind.Relative);
                PokemonImage.Source = new BitmapImage(fileUri);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstPokemon.SelectedItem != null && inputedName.Text != "")
            {
                string selectedPokemonName = FirstPokemon.SelectedItem.ToString();
                PokemonModel selectedPokemon = null;
                string playerName = inputedName.Text;
                if (selectedPokemonName != "")
                {
                    newPlayer = new PlayerModel(playerName);
                    if (availablePokemon.TryGetValue(selectedPokemonName, out selectedPokemon))
                        newPlayer.AddPokemon(selectedPokemon);
                    else
                    {
                        MessageBox.Show("Unexpected Error : Pokemon not found!");
                        return;
                    }

                }
                MainWindow mainWindow = new MainWindow();
                mainWindow.CreateGame(newPlayer);
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please input your name and choose a pokemon.");
                return;
            }
        }
    }
}
