using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pokemon.BgMusic
{
    public static class BGMPlayer
    {
        private static MediaPlayer mplayer = new MediaPlayer();
        private static double defaultVolume = 0.1;
        private static Random rnd;
        public delegate void SongPlayer();
        public static SongPlayer tmpPlayer {
            get;
            private set;
        }

        private static void Media_Ended(object sender, EventArgs e)
        {
            tmpPlayer();
        }

        public static void TitleScreenMusic()
        {
            tmpPlayer = TitleScreenMusic;
            mplayer = new MediaPlayer();
            mplayer.Volume = defaultVolume;
            mplayer.Open(new Uri(@"../../BgMusic/misc/titleScreen.mp3", UriKind.Relative));
            mplayer.Play();
            mplayer.MediaEnded += new EventHandler(Media_Ended);
        }

        public static void MainWindowMusic()
        {
            tmpPlayer = MainWindowMusic;
            if(mplayer != null)
                mplayer.Stop();
            rnd = new Random();
            int musicNum = rnd.Next(1, 3);
            mplayer.Open(new Uri(@"../../BgMusic/main/main"+musicNum+".mp3", UriKind.Relative));
            mplayer.Play();
            mplayer.MediaEnded += new EventHandler(Media_Ended);
        }

        public static void GymBattleMusic()
        {
            tmpPlayer = GymBattleMusic;
            if (mplayer != null)
                mplayer.Stop();
            rnd = new Random();
            int musicNum = rnd.Next(1, 3);
            mplayer.Open(new Uri(@"../../BgMusic/gymBattle/gymBattle"+musicNum+".mp3", UriKind.Relative));
            mplayer.Play();
            mplayer.MediaEnded += new EventHandler(Media_Ended);
        }

        public static void IsMusicMute(bool mute)
        {
            if (mute)
            {
                mplayer.Volume = 0;
            } else
            {
                mplayer.Volume = defaultVolume;
            }
        }

        private static MediaPlayer sfxPlayer = new MediaPlayer();
        public static void Hit()
        {
            sfxPlayer.Volume = 0.3;
            sfxPlayer.Open(new Uri(@"../../BgMusic/misc/hit.mp3", UriKind.Relative));
            sfxPlayer.Play();
        }
        public static void SuperHit()
        {
            sfxPlayer.Volume = 0.3;
            sfxPlayer.Open(new Uri(@"../../BgMusic/misc/superHit.mp3", UriKind.Relative));
            sfxPlayer.Play();
        }
        public static void SuccessfulCatch()
        {
            StopSfxPlayer();
            sfxPlayer.Volume = 0.2;
            sfxPlayer.Open(new Uri(@"../../BgMusic/misc/successfulCatch.mp3", UriKind.Relative));
            sfxPlayer.Play();
        }

        public static void FailedCatch()
        {
            StopSfxPlayer();
            sfxPlayer.Volume = 0.2;
            sfxPlayer.Open(new Uri(@"../../BgMusic/misc/failedCatch.mp3", UriKind.Relative));
            sfxPlayer.Play();
        }

        public static void BattleVictory()
        {
            StopSfxPlayer();
            sfxPlayer.Volume = 0.2;
            sfxPlayer.Open(new Uri(@"../../BgMusic/misc/gymVictory.mp3", UriKind.Relative));
            sfxPlayer.Play();
        }

        public static void BattleLose()
        {
            StopSfxPlayer();
            sfxPlayer.Volume = 0.2;
            sfxPlayer.Open(new Uri(@"../../BgMusic/misc/gymLose.mp3", UriKind.Relative));
            sfxPlayer.Play();
        }

        public static void StopSfxPlayer()
        {
            if (sfxPlayer != null)
                sfxPlayer.Stop();
            mplayer.Volume = 0;
        }

        public static void SFX_Ended()
        {
            mplayer.Volume = defaultVolume;
        }
    }
}
