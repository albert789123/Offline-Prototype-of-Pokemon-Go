using System;
using Pokemon.Library;

namespace Pokemon.Model
{
	public abstract class PokemonModel : BaseNotificationClass
	{

		protected string _evolveStage;
		public string EvolveStage
		{
			get { return _evolveStage; }
			protected set { 
				_evolveStage = value;
				OnPropertyChanged();
			}
		}

		protected string _nickName;
		public string NickName
		{
			get { return _nickName; }
			protected set
			{
				_nickName = value;
				OnPropertyChanged();
			}
		}

		public PokemonModel()
		{

		}

		public PokemonModel(string Name)
		{
			_nickName = Name;
		}

		protected int _lvl;
		public int Level
		{
			get { return _lvl; }
			protected set
			{
				_lvl = value;
				OnPropertyChanged();
			}
		}

		protected int _exp;
		public int EXP
		{
			get { return _exp; }
			protected set
			{
				_exp = value;
				OnPropertyChanged();
			}
		}

		protected int _hp;
		public int HP
		{
			get { return _hp; }
			protected set
			{
				_hp = value;
				OnPropertyChanged();
			}
		}

		protected int _atk;
		public int Attack
		{
			get { return _atk; }
			protected set
			{
				_atk = value;
				OnPropertyChanged();
			}
		}

		protected int _def;
		public int Defense
		{
			get { return _def; }
			protected set
			{
				_def = value;
				OnPropertyChanged();
			}
		}

		protected int _spd;
		public int Speed
		{
			get { return _spd; }
			protected set
			{
				_spd = value;
				OnPropertyChanged();
			}
		}

		public Skill noramlSkill = new Skill();
		public Skill specialSkill = new Skill();

		public void GainEXP()
		{
			_exp = _exp++;
			LevelUpEvent();
		}
		public void GainEXP(int i)
		{
			_exp = _exp + i;
			LevelUpEvent();
		}

		public abstract void LevelUpEvent();

		public void ChangeName(string newName)
        {
			_nickName = newName;
		}
	}

	
}