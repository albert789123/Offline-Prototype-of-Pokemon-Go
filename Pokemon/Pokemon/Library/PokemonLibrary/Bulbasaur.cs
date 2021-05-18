using Pokemon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Library.PokemonLibrary
{
	public class Bulbasaur : Pokemon.Model.PokemonModel
	{

		public Bulbasaur()
		{
			EvolveStage = "Bulbasaur";
			NickName = "Bulbasaur";
			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(4);
			specialSkill.LearnSkill(5);

		}
		public Bulbasaur(string name) : base(name)
		{
			EvolveStage = "Bulbasaur";
			Level = 1;
			EXP = 0;
			HP = 30;
			Attack = 10;
			Defense = 10;
			Speed = 10;

			noramlSkill.LearnSkill(4);
			specialSkill.LearnSkill(5);

		}

		public override void LevelUpEvent()
		{
			if (_exp >= 10)
			{
				Level = _lvl + _exp / 10;
				HP = _hp + 10 * _exp / 10;
				Attack = _atk + 1 * _exp / 10;
				Defense = _def + 2 * _exp / 10;
				Speed = _spd + 1 * _exp / 10;
				EXP = _exp % 10;
			}
			if (_lvl >= 10)
			{
				if (NickName == EvolveStage)
					NickName = "Vanusaur";
				EvolveStage = "Vanusaur";
			}
			else if (_lvl >= 5)
			{
				if (NickName == EvolveStage)
					NickName = "Ivysaur";
				EvolveStage = "Ivysaur";
			}

		}

	}
}