using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Pokemon.Model
{
    public class PlayerModel : BaseNotificationClass
    {
        private string playerName;
        private ObservableCollection<PokemonModel> collectedPokemon = new ObservableCollection<PokemonModel>();
        public ObservableCollection<PokemonModel> CollectedPokemon
        {
            get { return collectedPokemon; }
            private set 
            { 
                collectedPokemon = value;
                OnPropertyChanged();
            }
        }
        public string PlayerName
        {
            get { return playerName; }
            private set 
            { 
                playerName = value; 
                OnPropertyChanged();
            }
        }

        public PlayerModel(string name)
        {
            PlayerName = name;
        }

        public void AddPokemon(PokemonModel newPokemon)
        {
            CollectedPokemon = AddPokemonService(CollectedPokemon, newPokemon);
            return;
        }

        private ObservableCollection<PokemonModel> AddPokemonService(ObservableCollection<PokemonModel> PokemonList, PokemonModel newPokemon)
        {
            PokemonList.Add(newPokemon);
            return PokemonList;
        }
    }
}
