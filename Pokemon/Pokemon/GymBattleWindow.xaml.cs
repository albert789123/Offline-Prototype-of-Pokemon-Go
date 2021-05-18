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
using Pokemon.Library.PokemonLibrary;
using Pokemon.ViewModel;
using Pokemon.Model;
using Pokemon.Factory;
using Pokemon.BgMusic;
using System.Windows.Media.Animation;

namespace Pokemon
{
    /// <summary>
    /// GymBattleWindow.xaml 的互動邏輯
    /// </summary>
    public partial class GymBattleWindow : Window
    {
        public GameSession currentGame => DataContext as GameSession;
        private PokemonModel GymBattleEnemyPokemon;
        private Button[] buttons;
        private Uri MyUri;
        private Uri EnemyUri;
        private int chosen;
        private int turn;
        private Random rnd = new Random();

        private int MyAtk;
        private int MyDef;
        private int MyAttackPower;
        private int MySpecialAttackPower;
        private int MyMaxHP;
        private int MyCurrentHP;
        private int EnemyAtk;
        private int EnemyDef;
        private int EnemyAttackPower;
        private int EnemySpecialAttackPower;
        private int EnemyMaxHP;
        private int EnemyCurrentHP;
        private bool critical = false;

        public GymBattleWindow()
        {
            InitializeComponent();
        }


        public void GymBattleInitialize()
        {
            turn = 0;
            MyHP.Visibility = Visibility.Hidden;
            EnemyHP.Visibility = Visibility.Hidden;
            MyImage.Visibility = Visibility.Hidden;
            EnemyImage.Visibility = Visibility.Hidden;
            MessageDisplay.Visibility = Visibility.Hidden;
            AttackButton.Visibility = Visibility.Hidden;
            SpecialAttackButton.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;
            buttons = new Button[6];
            for (int i = 0; i < 6; i++)
            {
                buttons[i] = PokemonButtonPanel.Children[i] as Button;
                buttons[i].Visibility = Visibility.Hidden;
            }
            for (int i = 0; i < currentGame.CurrentPlayer.CollectedPokemon.Count; i++)
            {
                buttons[i].Visibility = Visibility.Visible;
                //buttons[i].Content = currentGame.CurrentPlayer.CollectedPokemon[i].NickName;
                Image img = (Image)this.FindName("PokemonButton" + i + "_Image");
                Label name = (Label)this.FindName("PokemonButton" + i + "_Name");
                Label level = (Label)this.FindName("PokemonButton" + i + "_Level");
                Label hp = (Label)this.FindName("PokemonButton" + i + "_HP");

                string pokemonName = currentGame.CurrentPlayer.CollectedPokemon[i].EvolveStage;
                int pokemonHP = currentGame.CurrentPlayer.CollectedPokemon[i].HP;
                int pokemonLevel = currentGame.CurrentPlayer.CollectedPokemon[i].Level;
                img.Source = new BitmapImage(new Uri("/Images/PokemonImages/" + pokemonName + ".png", UriKind.Relative));
                name.Content = pokemonName;
                level.Content = "Level: " + pokemonLevel.ToString();
                hp.Content = "HP: " + pokemonHP.ToString();
            }
            GymBattleEnemyPokemon = PokemonFactory.Spawn();
        }

        public void Choosing()
        {
            for (int i = 0; i < 6; i++)
                PokemonButtonPanel.Children[i].Visibility = Visibility.Hidden;
            MyHP.Visibility = Visibility.Visible;
            EnemyHP.Visibility = Visibility.Visible;
            MyImage.Visibility = Visibility.Visible;
            EnemyImage.Visibility = Visibility.Visible;
            MessageDisplay.Visibility = Visibility.Visible;
            AttackButton.Visibility = Visibility.Visible;

            GymBattleEnemyPokemon.GainEXP(currentGame.CurrentPlayer.CollectedPokemon[chosen].Level * 10 - 10);

            MyUri = new Uri("/Images/PokemonImages/" + currentGame.CurrentPlayer.CollectedPokemon[chosen].EvolveStage + ".png", UriKind.Relative);
            EnemyUri = new Uri("/Images/PokemonImages/" + GymBattleEnemyPokemon.EvolveStage + ".png", UriKind.Relative);
            MyImage.Source = new BitmapImage(MyUri);
            EnemyImage.Source = new BitmapImage(EnemyUri);
            MyAtk = currentGame.CurrentPlayer.CollectedPokemon[chosen].Attack;
            MyDef = currentGame.CurrentPlayer.CollectedPokemon[chosen].Defense;
            MyAttackPower = currentGame.CurrentPlayer.CollectedPokemon[chosen].noramlSkill.Power;
            MySpecialAttackPower = currentGame.CurrentPlayer.CollectedPokemon[chosen].specialSkill.Power;
            MyMaxHP = currentGame.CurrentPlayer.CollectedPokemon[chosen].HP;
            MyCurrentHP = MyMaxHP;
            EnemyAtk = GymBattleEnemyPokemon.Attack;
            EnemyDef = GymBattleEnemyPokemon.Defense;
            EnemyAttackPower = GymBattleEnemyPokemon.noramlSkill.Power;
            EnemySpecialAttackPower = GymBattleEnemyPokemon.specialSkill.Power;
            EnemyMaxHP = GymBattleEnemyPokemon.HP;
            EnemyCurrentHP = EnemyMaxHP;
            DisplayHP();
            MessageDisplay.Text = "Gym Battle Started!";
            Message.Visibility = Visibility.Hidden;
            AttackButton.Click += delegate (object sender, RoutedEventArgs e) { AttackButton_Click(sender, e, MyMaxHP, EnemyMaxHP, currentGame.CurrentPlayer.CollectedPokemon[chosen], GymBattleEnemyPokemon, chosen); };
            SpecialAttackButton.Click += delegate (object sender, RoutedEventArgs e) { SpecialAttackButton_Click(sender, e, MyMaxHP, EnemyMaxHP, currentGame.CurrentPlayer.CollectedPokemon[chosen], GymBattleEnemyPokemon, chosen); };

        }

        public void DisplayHP()
        {
            MyHP.Text = "HP: " + MyCurrentHP + " / " + MyMaxHP;
            EnemyHP.Text = "HP: " + EnemyCurrentHP + " / " + EnemyMaxHP;
        }

        private async void AttackButton_Click(object sender, RoutedEventArgs e, int MyMaxHP, int EnemyMaxHP, PokemonModel MyPokemon, PokemonModel GymBattleEnemyPokemon, int chosen)
        {
            turn++;
            DisplaySystemMessage(turn);
            AttackButton.Visibility = Visibility.Hidden;
            SpecialAttackButton.Visibility = Visibility.Hidden;
            if (MyPokemon.Speed < GymBattleEnemyPokemon.Speed)
            {
                MyCurrentHP -= damageCalculateion(GymBattleEnemyPokemon, MyPokemon, GymBattleEnemyPokemon.noramlSkill.Power);
                DisplayEnemyMessage(GymBattleEnemyPokemon);
                BGMPlayer.Hit();
                MyImageShake();
                if (!checking())
                {
                    await Task.Delay(800);
                    EnemyCurrentHP -= damageCalculateion(MyPokemon, GymBattleEnemyPokemon, MyPokemon.noramlSkill.Power);
                    DisplayMyMessage(MyPokemon, 0);
                    BGMPlayer.Hit();
                    EnemyImageShake();
                    if (checking())
                    {
                        return;
                    }
                    else
                        await Task.Delay(500);
                }
                else
                {
                    return;
                }
            }
            else
            {
                EnemyCurrentHP -= damageCalculateion(MyPokemon, GymBattleEnemyPokemon, MyPokemon.noramlSkill.Power);
                DisplayMyMessage(MyPokemon, 0);
                BGMPlayer.Hit();
                EnemyImageShake();
                if (!checking())
                {
                    await Task.Delay(800);
                    MyCurrentHP -= damageCalculateion(GymBattleEnemyPokemon, MyPokemon, GymBattleEnemyPokemon.noramlSkill.Power);
                    DisplayEnemyMessage(GymBattleEnemyPokemon);
                    BGMPlayer.Hit();
                    MyImageShake();
                    if(checking())
                    {
                        return;
                    } else
                        await Task.Delay(500);
                }
                else
                {
                    return;
                }
            }
            if (turn % 3 == 0)
                SpecialAttackButton.Visibility = Visibility.Visible;
            AttackButton.Visibility = Visibility.Visible;
        }

        private async void SpecialAttackButton_Click(object sender, RoutedEventArgs e, int MyMaxHP, int EnemyMaxHP, PokemonModel MyPokemon, PokemonModel GymBattleEnemyPokemon, int chosen)
        {
            turn++;
            DisplaySystemMessage(turn);
            AttackButton.Visibility = Visibility.Hidden;
            SpecialAttackButton.Visibility = Visibility.Hidden;
            if (MyPokemon.Speed < GymBattleEnemyPokemon.Speed)
            {
                MyCurrentHP -= damageCalculateion(GymBattleEnemyPokemon, MyPokemon, GymBattleEnemyPokemon.specialSkill.Power);
                DisplayEnemyMessage(GymBattleEnemyPokemon);
                BGMPlayer.Hit();
                MyImageShake();
                if(!checking())
                {
                    await Task.Delay(800);
                    EnemyCurrentHP -= damageCalculateion(MyPokemon, GymBattleEnemyPokemon, MyPokemon.specialSkill.Power);
                    DisplayMyMessage(MyPokemon, 1);
                    BGMPlayer.SuperHit();
                    EnemyImageShake();
                    if (checking())
                    {
                        return;
                    }
                    else
                        await Task.Delay(500);
                }
                else
                {
                    return;
                }
            }
            else
            {
                EnemyCurrentHP -= damageCalculateion(MyPokemon, GymBattleEnemyPokemon, MyPokemon.noramlSkill.Power);
                DisplayMyMessage(MyPokemon, 1);
                BGMPlayer.SuperHit();
                EnemyImageShake();
                if (!checking())
                {
                    await Task.Delay(1500);
                    MyCurrentHP -= damageCalculateion(GymBattleEnemyPokemon, MyPokemon, GymBattleEnemyPokemon.noramlSkill.Power);
                    DisplayEnemyMessage(GymBattleEnemyPokemon);
                    BGMPlayer.Hit();
                    MyImageShake();
                    if (checking())
                    {
                        return;
                    }
                    else
                        await Task.Delay(500);
                }
                else
                {
                    return;
                }
            }
            if (turn % 3 == 0)
                SpecialAttackButton.Visibility = Visibility.Visible;
            AttackButton.Visibility = Visibility.Visible;
        }

        private void MyImageShake()
        {
            TranslateTransform trans = new TranslateTransform();
            MyImage.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation()
            {
                From = 0,
                To = 0 - 10,
                SpeedRatio = 4,
                Duration = TimeSpan.FromSeconds(0.5),
                FillBehavior = FillBehavior.HoldEnd,
                RepeatBehavior = new RepeatBehavior(4)
            };
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
        }

        private void EnemyImageShake()
        {
            TranslateTransform trans = new TranslateTransform();
            EnemyImage.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation()
            {
                From = 0,
                To = 0 - 10,
                SpeedRatio = 4,
                Duration = TimeSpan.FromSeconds(0.5),
                FillBehavior = FillBehavior.HoldEnd,
                RepeatBehavior = new RepeatBehavior(4)
            };
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public int damageCalculateion(PokemonModel Attacker, PokemonModel Defenser, int SkillPower)
        {
            critical = false;
            Random rnd = new Random();
            int roll = rnd.Next(5);
            if (roll == 4)
                critical = true;
            if (critical)
            {
                int damage = (Attacker.Attack - Defenser.Defense + SkillPower) * 8 / 10 * 3 / 2;
                if (damage < 4)
                {
                    return 4;
                }
                else
                {
                    return damage;
                }
            }
            else
            {
                int damage = (Attacker.Attack - Defenser.Defense + SkillPower) * 8 / 10;
                if (damage < 2)
                {
                    return 2;
                }
                else
                {
                    return damage;
                }
            }

        }

        public void DisplaySystemMessage(int turn)
        {
            string systemMessage;
            systemMessage = "Round " + turn + ": \n";
            MessageDisplay.Text = systemMessage;
        }

        public void DisplayMyMessage(PokemonModel MyPokemon, int SkillType)
        {
            string myMessage;
            if (critical)
            {
                if (SkillType == 0)
                    myMessage = "Your " + MyPokemon.NickName + " used " + MyPokemon.noramlSkill.Name + ". Critical Hit!\n";
                else
                    myMessage = "Your " + MyPokemon.NickName + " used " + MyPokemon.specialSkill.Name + ". Critical Hit!\n";
            }
            else
            {
                if (SkillType == 0)
                    myMessage = "Your " + MyPokemon.NickName + " used " + MyPokemon.noramlSkill.Name + ".\n";
                else
                    myMessage = "Your " + MyPokemon.NickName + " used " + MyPokemon.specialSkill.Name + ".\n";
            }
            MessageDisplay.Text += myMessage;

        }

        public void DisplayEnemyMessage(PokemonModel GymBattleEnemyPokemon)
        {
            string enemyMessage;
            if (critical)
                enemyMessage = GymBattleEnemyPokemon.NickName + " used " + GymBattleEnemyPokemon.noramlSkill.Name + ". Critical Hit!\n";
            else
                enemyMessage = GymBattleEnemyPokemon.NickName + " used " + GymBattleEnemyPokemon.noramlSkill.Name + ".\n";
            MessageDisplay.Text += enemyMessage;
        }


        public bool checking()
        {
            if(turn > 20)
            {
                BGMPlayer.BattleLose();
                currentGame.CurrentPlayer.CollectedPokemon[chosen].GainEXP(5);
                AttackButton.Visibility = Visibility.Hidden;
                SpecialAttackButton.Visibility = Visibility.Hidden;
                BackButton.Visibility = Visibility.Visible;
                MessageDisplay.Text = "It is a tie! Your pokemon gained 5EXP.";
                return true;
            }
            if (EnemyCurrentHP <= 0)
            {
                BGMPlayer.BattleVictory();
                currentGame.CurrentPlayer.CollectedPokemon[chosen].GainEXP(10);
                AttackButton.Visibility = Visibility.Hidden;
                SpecialAttackButton.Visibility = Visibility.Hidden;
                BackButton.Visibility = Visibility.Visible;
                MessageDisplay.Text = "You win the battle! Your pokemon gained 10EXP.";
                EnemyHP.Text = "HP: 0 / " + EnemyMaxHP;
                return true;
            }
            else if (MyCurrentHP <= 0)
            {
                BGMPlayer.BattleLose();
                currentGame.CurrentPlayer.CollectedPokemon[chosen].GainEXP(5);
                AttackButton.Visibility = Visibility.Hidden;
                SpecialAttackButton.Visibility = Visibility.Hidden;
                BackButton.Visibility = Visibility.Visible;
                MessageDisplay.Text = "You lose the battle! Your pokemon gained 5EXP.";
                MyHP.Text = "HP: 0 / " + MyMaxHP;
                return true;
            }
            else
                DisplayHP();
                return false;
        }


        private void PokemonButton0_Click(object sender, RoutedEventArgs e)
        {
            chosen = 0;
            Choosing();
        }

        private void PokemonButton1_Click(object sender, RoutedEventArgs e)
        {
            chosen = 1;
            Choosing();
        }

        private void PokemonButton2_Click(object sender, RoutedEventArgs e)
        {
            chosen = 2;
            Choosing();
        }

        private void PokemonButton3_Click(object sender, RoutedEventArgs e)
        {
            chosen = 3;
            Choosing();
        }

        private void PokemonButton4_Click(object sender, RoutedEventArgs e)
        {
            chosen = 4;
            Choosing();
        }
        private void PokemonButton5_Click(object sender, RoutedEventArgs e)
        {
            chosen = 5;
            Choosing();
        }

    }
}