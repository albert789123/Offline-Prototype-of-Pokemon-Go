using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Library.PokemonLibrary;

namespace Pokemon.Model
{
	public static class PokemonFactory
	{
		//static int count = 10;
		static int totalNum = 5;
		static Random rnd = new Random();
		public static PokemonModel Spawn()
		{
			int roll = rnd.Next(totalNum);
			if (roll == 0)
				return new Pikachu();
			else if (roll == 1)
				return new Charmander();
			else if (roll == 2)
				return new Pidgey();
			else if (roll == 3)
				return new Bulbasaur();
			else if (roll == 4)
				return new Squirtle();
			else
				return null;
		}
	}
}
