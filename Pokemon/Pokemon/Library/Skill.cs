using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Library
{
	public class Skill
	{

		private string _name;
		public string Name
		{
			get { return _name; }
		}

		private int _power;
		public int Power
		{
			get { return _power; }
		}

		public void LearnSkill(int SkillNum)
		{
			if (SkillNum == 0)
			{
				_name = "Thunderbolt";
				_power = 10;
			}
			else if (SkillNum == 1)
			{
				_name = "Thunder";
				_power = 30;
			}
			else if (SkillNum == 2)
			{
				_name = "Flame Thrower";
				_power = 10;
			}
			else if (SkillNum == 3)
			{
				_name = "Fire Blast";
				_power = 30;
			}
			else if (SkillNum == 4)
			{
				_name = "Vine Whip";
				_power = 10;
			}
			else if (SkillNum == 5)
			{
				_name = "Solar Beam";
				_power = 30;
			}
			else if (SkillNum == 6)
			{
				_name = "Water Pulse";
				_power = 10;
			}
			else if (SkillNum == 7)
			{
				_name = "Hydro Pump";
				_power = 30;
			}
			else if (SkillNum == 8)
			{
				_name = "Peck";
				_power = 10;
			}
			else if (SkillNum == 9)
			{
				_name = "Sky Attack";
				_power = 30;
			}
		}

	}
}
