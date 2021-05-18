using Pokemon.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Model
{
    public class Location
    {
        int mapNumber;
        public int MapNumber
        {
            get { return mapNumber; }
            set { mapNumber = value; }
        }

        int rowPos;
        public int RowPos
        {
            get { return rowPos; }
            set { rowPos = value; }
        }

        int colPos;
        public int ColPos
        {
            get { return colPos; }
            set { colPos = value; }
        }

        bool tradeHere;
        public bool TradeHere
        {
            get { return tradeHere; }
            set { tradeHere = value; }
        }

        bool pokemonHere;
        public bool PokemonHere
        {
            get { return pokemonHere; }
            set { pokemonHere = value; }
        }
        PokemonModel catchPokemon;
        public PokemonModel CatchPokemon
        {
            get { return catchPokemon; }
            private set { catchPokemon = value; }
        }

        bool gymBattleHere;
        public bool GymBattleHere
        {
            get { return gymBattleHere; }
            set { gymBattleHere = value; }
        }
        PokemonModel gymPokemon;
        public PokemonModel GymPokemon
        {
            get { return gymPokemon; }
            private set { gymPokemon = value; }
        }

        bool canGoUp;
        public bool CanGoUp
        {
            get { return canGoUp; }
            set { canGoUp = value; }
        }
        bool canGoDown;
        public bool CanGoDown
        {
            get { return canGoDown; }
            set { canGoDown = value; }
        }
        bool canGoLeft;
        public bool CanGoLeft
        {
            get { return canGoLeft; }
            set { canGoLeft = value; }
        }
        bool canGoRight;
        public bool CanGoRight
        {
            get { return canGoRight; }
            set { canGoRight = value; }
        }

        public Location(Grid loc, int mapNum)
        {
            this.MapNumber = mapNum;
            this.GymBattleHere = loc.GymBattleAvailable;
            // Not yet edit the enemy pokemon
            this.TradeHere = loc.TradeAvailable;
            this.PokemonHere = loc.PokemonAvailable;
            this.CatchPokemon = loc.PokemonChosen;

            this.RowPos = loc.RowPos;
            this.ColPos = loc.ColPos;

            Dictionary<int,Map> mapList = WorldFactory.getMap();
            Map nowMap = mapList[mapNum];
            if (nowMap.Up != null)
                CanGoUp = true;
            if (nowMap.Down != null)
                CanGoDown = true;
            if (nowMap.Left != null)
                CanGoLeft = true;
            if (nowMap.Right != null)
                CanGoRight = true;
        }

        // Should use Factory to generate
        // Trade
        // Available pokemon to catch
        // GymBattle
        // Exit Point
    }
}
