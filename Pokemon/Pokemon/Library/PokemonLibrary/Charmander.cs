using Pokemon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Library.PokemonLibrary
{
	public class Charmander : Pokemon.Model.PokemonModel
	{

		public Charmander()
		{
			EvolveStage = "Charmander";
			NickName = "Charmander";
			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(2);
			specialSkill.LearnSkill(3);

		}
		public Charmander(string name) : base(name)
		{
			EvolveStage = "Charmander";
			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(2);
			specialSkill.LearnSkill(3);

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
			if (_lvl >= 10)
			{
				if (NickName == EvolveStage)
					NickName = "Charizard";
				EvolveStage = "Charizard";
			}
			else if (_lvl >= 5)
			{
				if (NickName == EvolveStage)
					NickName = "Charmeleon";
				EvolveStage = "Charmeleon";
			}

		}

	}
}