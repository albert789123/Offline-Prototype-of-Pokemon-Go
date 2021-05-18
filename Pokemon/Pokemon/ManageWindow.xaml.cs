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
using Pokemon.ViewModel;

namespace Pokemon
{
    /// <summary>
    /// ManageWindow.xaml 的互動邏輯
    /// </summary>
    public partial class ManageWindow : Window
    {
        public GameSession CurrentGame => DataContext as GameSession;
        private Image[] images = new Image[6];
        private TextBlock[] statBlocks = new TextBlock[6];
        private NamingButton[] namingButtons = new NamingButton[6];
        private TextBox[] nameBoxes = new TextBox[6];
        private NamingButton[] confirmButtons = new NamingButton[6];
        private NamingButton[] abandonButtons = new NamingButton[6];

        public ManageWindow()
        {
            InitializeComponent();
        }

        public void ManageInitialize()
        {
            for (int i = 0; i < 6; i++)
            {
                images[i] = new Image();
                //images[i] = ImagePanel.Children[i] as Image;
                images[i] = (Image)this.FindName("PokemonImage" + i);

                statBlocks[i] = new TextBlock();
                //statBlocks[i] = StatBlockPanel.Children[i] as TextBlock;
                statBlocks[i] = (TextBlock)this.FindName("StatBlock" + i);

                namingButtons[i] = new NamingButton((Button)this.FindName("NamingButton" + i));
                namingButtons[i].Num = i;

                nameBoxes[i] = new TextBox();
                //nameBoxes[i] = NameBoxPanel.Children[i] as TextBox;
                nameBoxes[i] = (TextBox)this.FindName("NameBox" + i);

                confirmButtons[i] = new NamingButton((Button)this.FindName("ConfirmButton" + i));
                confirmButtons[i].Num = i;

                abandonButtons[i] = new NamingButton((Button)this.FindName("AbandonButton" + i));
                abandonButtons[i].Num = i;

                namingButtons[i].btn.Visibility = Visibility.Hidden;
                nameBoxes[i].Visibility = Visibility.Hidden;
                confirmButtons[i].btn.Visibility = Visibility.Hidden;
                abandonButtons[i].btn.Visibility = Visibility.Hidden;
                namingButtons[i].btn.Click += delegate (object sender, RoutedEventArgs e) { Naming(sender, e); };
                confirmButtons[i].btn.Click += delegate (object sender, RoutedEventArgs e) { NamingFinish(sender, e); };
                abandonButtons[i].btn.Click += delegate (object sender, RoutedEventArgs e) { Abandoning(sender, e); };
            }

            DisplayStat();
        }

        public class NamingButton
        {
            public int Num;
            public Button btn;
            public NamingButton(Button btn)
            {
                this.btn = btn;
            }
        }
        public void DisplayStat()
        {
            for (int i = 0; i < CurrentGame.CurrentPlayer.CollectedPokemon.Count; i++)
            {
                if (CurrentGame.CurrentPlayer.CollectedPokemon[i] != null)
                {
                    Uri fileUri = new Uri("/Images/PokemonImages/" + CurrentGame.CurrentPlayer.CollectedPokemon[i].EvolveStage + ".png", UriKind.Relative);
                    images[i].Source = new BitmapImage(fileUri);
                    statBlocks[i].Text = "Name: " + CurrentGame.CurrentPlayer.CollectedPokemon[i].NickName +
                                         "\nLevel: " + CurrentGame.CurrentPlayer.CollectedPokemon[i].Level +
                                         "\nEXP: " + CurrentGame.CurrentPlayer.CollectedPokemon[i].EXP +
                                         "\nHP: " + CurrentGame.CurrentPlayer.CollectedPokemon[i].HP +
                                         "\nAttack: " + CurrentGame.CurrentPlayer.CollectedPokemon[i].Attack +
                                         "\nDefense: " + CurrentGame.CurrentPlayer.CollectedPokemon[i].Defense +
                                         "\nSpeed: " + CurrentGame.CurrentPlayer.CollectedPokemon[i].Speed;
                    namingButtons[i].btn.Visibility = Visibility.Visible;
                    abandonButtons[i].btn.Visibility = Visibility.Visible;
                }
            }

        }

        public void Naming(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            NamingButton targetNB = null;
            foreach(NamingButton nB in namingButtons)
            {
                if(nB.btn.Name == btn.Name)
                {
                    targetNB = nB;
                    break;
                }
            }
            if(targetNB == null)
            {
                throw new Exception("Unexpected error: Button not found in array");
            }
            targetNB.btn.Visibility = Visibility.Hidden;
            nameBoxes[targetNB.Num].Visibility = Visibility.Visible;
            confirmButtons[targetNB.Num].btn.Visibility = Visibility.Visible;
        }

        public void NamingFinish(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            NamingButton targetNB = null;
            foreach (NamingButton nB in confirmButtons)
            {
                if (nB.btn.Name == btn.Name)
                {
                    targetNB = nB;
                    break;
                }
            }
            if (targetNB == null)
            {
                throw new Exception("Unexpected error: Button not found in array");
            }
            if(nameBoxes[targetNB.Num].Text == "")
            {
                MessageBox.Show("Nick name cannot be null. Please try again!");
                targetNB.btn.Visibility = Visibility.Hidden;
                nameBoxes[targetNB.Num].Visibility = Visibility.Hidden;
                namingButtons[targetNB.Num].btn.Visibility = Visibility.Visible;
                DisplayStat();
                return;
            }

            targetNB.btn.Visibility = Visibility.Hidden;
            CurrentGame.CurrentPlayer.CollectedPokemon[targetNB.Num].ChangeName(nameBoxes[targetNB.Num].Text);
            nameBoxes[targetNB.Num].Visibility = Visibility.Hidden;
            namingButtons[targetNB.Num].btn.Visibility = Visibility.Visible;
            DisplayStat();
            return;
        }

        public void Abandoning(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            NamingButton targetNB = null;
            foreach (NamingButton nB in abandonButtons)
            {
                if (nB.btn.Name == btn.Name)
                {
                    targetNB = nB;
                    break;
                }
            }
            if (targetNB == null)
            {
                throw new Exception("Unexpected error: Button not found in array");
            }

            if(CurrentGame.CurrentPlayer.CollectedPokemon.Count > 1)
            {
                targetNB.btn.Visibility = Visibility.Hidden;
                CurrentGame.CurrentPlayer.CollectedPokemon.RemoveAt(targetNB.Num);
                ManageWindow newWindow = new ManageWindow();
                newWindow.DataContext = CurrentGame;
                newWindow.ManageInitialize();
                Application.Current.MainWindow = newWindow;
                newWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("You need at least one pokemon to be your partner =)");
            }
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}