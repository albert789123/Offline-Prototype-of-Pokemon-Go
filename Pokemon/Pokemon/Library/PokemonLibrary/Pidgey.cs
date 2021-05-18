using Pokemon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Library.PokemonLibrary
{
	public class Pidgey : Pokemon.Model.PokemonModel
	{

		public Pidgey()
		{
			EvolveStage = "Pidgey";
			NickName = "Pidgey";
			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(8);
			specialSkill.LearnSkill(9);

		}
		public Pidgey(string name) : base(name)
		{
			EvolveStage = "Pidgey";
			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(8);
			specialSkill.LearnSkill(9);

		}

		public override void LevelUpEvent()
		{
			if (_exp >= 10)
			{
				Level += _exp / 10;
				HP += 5 * _exp / 10;
				Attack += 1 * _exp / 10;
				Defense += 1 * _exp / 10;
				Speed += 3 * _exp / 10;
				EXP = _exp % 10;
			}
			if (_lvl >= 10)
			{
				if (NickName == EvolveStage)
					NickName = "Pidgeot";
				EvolveStage = "Pidgeot";
			}
			else if (Level >= 5)
			{
				if (NickName == EvolveStage)
					NickName = "Pidgeotto";
				EvolveStage = "Pidgeotto";
			}

		}

	}
}