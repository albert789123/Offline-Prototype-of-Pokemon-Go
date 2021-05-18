using Pokemon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Library.PokemonLibrary
{
	public class Pikachu : Pokemon.Model.PokemonModel
	{

		public Pikachu()
		{
			EvolveStage = "Pikachu";
			NickName = "Pikachu";

			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(0);
			specialSkill.LearnSkill(1);

		}
		public Pikachu(string name) : base(name)
		{
			EvolveStage = "Pikachu";
			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(0);
			specialSkill.LearnSkill(1);

		}

		public override void LevelUpEvent()
		{
			if (_exp >= 10)
			{
				Level += _exp / 10;
				HP += 5 * _exp / 10;
				Attack += 2 * _exp / 10;
				Defense += 1 * _exp / 10;
				Speed += 2 * _exp / 10;
				EXP = _exp % 10;
			}
			if (_lvl >= 7)
			{
				if (NickName == EvolveStage)
					NickName = "Raichu";
				EvolveStage = "Raichu";
			}

		}

	}
}