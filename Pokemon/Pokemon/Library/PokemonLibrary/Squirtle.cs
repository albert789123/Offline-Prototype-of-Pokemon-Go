using Pokemon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Library.PokemonLibrary
{
	public class Squirtle : Pokemon.Model.PokemonModel
	{

		public Squirtle()
		{
			EvolveStage = "Squirtle";
			NickName = "Squirtle";
			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(6);
			specialSkill.LearnSkill(7);

		}
		public Squirtle(string name) : base(name)
		{
			EvolveStage = "Squirtle";
			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(6);
			specialSkill.LearnSkill(7);

		}

		public override void LevelUpEvent()
		{
			if (_exp >= 10)
			{
				Level += _exp / 10;
				HP += 10 * _exp / 10;
				Attack += 1 * _exp / 10;
				Defense += 2 * _exp / 10;
				Speed += 1 * _exp / 10;
				EXP = _exp % 10;
			}
			if (_lvl >= 10)
			{
				if (NickName == EvolveStage)
					NickName = "Blastoise";
				EvolveStage = "Blastoise";
			}
			else if (_lvl >= 5)
			{
				if (NickName == EvolveStage)
					NickName = "Wartortle";
				EvolveStage = "Wartortle";
			}

		}

	}
}