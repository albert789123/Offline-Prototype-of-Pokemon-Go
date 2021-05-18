using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Model
{
    public class Grid
    {
        private string mapImage;
        public string MapImage
        {
            get { return mapImage; }
            private set { mapImage = value; }
        }
        private int rowPos;
        public int RowPos
        {
            get { return rowPos; }
            private set { rowPos = value; }
        }
        private int colPos;
        public int ColPos
        {
            get { return colPos; }
            private set { colPos = value; }
        }

        bool tradeAvailable;
        public bool TradeAvailable
        {
            get { return tradeAvailable; }
            private set { tradeAvailable = value; }
        }

        string tradeImage;
        public string TradeImage
        {
            get { return tradeImage; }
            private set { tradeImage = value; }
        }

        bool pokemonAvailable;
        public bool PokemonAvailable
        {
            get { return pokemonAvailable; }
            private set { pokemonAvailable = value; }
        }

        PokemonModel pokemonChosen;
        public PokemonModel PokemonChosen
        {
            get { return pokemonChosen; }
            private set { pokemonChosen = value; }
        }

        bool gymBattleAvailable;
        public bool GymBattleAvailable
        {
            get { return gymBattleAvailable; }
            private set { gymBattleAvailable = value; }
        }

        string gymBattleImage;
        public string GymBattleImage
        {
            get { return gymBattleImage; }
            private set { gymBattleImage = value; }
        }

        public Grid(int row, int col, string mapImage) //Default constructor
        {
            if (mapImage.Equals(""))
                throw new ArgumentException("Map image address cannot be null");
            RowPos = row;
            ColPos = col;
            MapImage = mapImage;
            TradeAvailable = false;
            TradeImage = null;
            PokemonAvailable = false;
            PokemonChosen = null;
            GymBattleAvailable = false;
            GymBattleImage = null;
        }

        public void addSpecialUsage(bool trade, bool catchPokemon, bool gym, string specialImage)
        {
            if (trade == false && catchPokemon == false && gym == false)
            {
                throw new ArgumentException("Wrong usage of the function.");
            }
            else if ((trade == true && catchPokemon == true) || (catchPokemon == true && gym == true) || (trade == true && gym == true))
            {
                throw new ArgumentException("One grid can only have one usage.");
            }
            else
            {
                TradeAvailable = trade;
                PokemonAvailable = catchPokemon;
                GymBattleAvailable = gym;
                if (TradeAvailable)
                {
                    TradeImage = specialImage;
                    GymBattleImage = null;
                    PokemonChosen = null;
                }
                else if (PokemonAvailable)
                {
                    TradeImage = null;
                    GymBattleImage = null;
                    PokemonChosen = PokemonFactory.Spawn();
                }
                else if (GymBattleAvailable)
                {
                    TradeImage = null;
                    GymBattleImage = specialImage;
                    PokemonChosen = null;
                }
            }
        }
        /*
        public Grid(int row, int col, string mapImage, bool trade, bool catchPokemon, bool gym, string specialImage) // When there is trader / pokemon / gymbattle
        {
            if(trade == false && catchPokemon == false && gym == false)
            {
                throw new ArgumentException("If no special usage, use another constructor");
            } else if((trade == true && catchPokemon == true) || (catchPokemon == true && gym == true) || (trade == true && gym == true))
            {
                throw new ArgumentException("One grid can only have one usage");
            }
            else
            {
                RowPos = row;
                ColPos = col;
                MapImage = mapImage;
                TradeAvailable = trade;
                PokemonAvailable = catchPokemon;
                GymBattleAvailable = gym;
                if (TradeAvailable)
                {
                    TradeImage = specialImage;
                    GymBattleImage = null;
                    PokemonImage = null;
                } else if (PokemonAvailable)
                {
                    TradeImage = null;
                    GymBattleImage = null;
                    PokemonImage = specialImage;
                } else if (GymBattleAvailable)
                {
                    TradeImage = null;
                    GymBattleImage = specialImage;
                    PokemonImage = null;
                }
            }
        }
        */
    }
}
