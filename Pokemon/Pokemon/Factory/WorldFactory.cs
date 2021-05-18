using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokemon.Model;

namespace Pokemon.Factory
{
    public static class WorldFactory
    {
        public static Grid[,,] generateWorld()
        {
            Grid[,,] world = new Grid[4, 10, 10];
            for (int i = 0; i < 4; i++)
            {
                for (int h = 0; h < 10; h++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        world[i, h, j] = new Grid(h, j, World.worldMap.GetValue(i, h, j).ToString());
                    }
                }
            }

            // Halt trader part
            /*
            for (int i = 0; i < 4; i++)
            {
                int trader_m = World.traderLocation[i, 0];
                int trader_r = World.traderLocation[i, 1];
                int trader_c = World.traderLocation[i, 2];
                world[trader_m, trader_r, trader_c].addSpecialUsage(true, false, false, World.traderImage_1);
                world[trader_m, trader_r, trader_c + 1].addSpecialUsage(true, false, false, World.traderImage_2);
            }
            */
            Random rnd = new Random();
            for (int i = 0; i < (World.gymBattleLocation.Length/3); i++)
            {
                int gymBattle_m = World.gymBattleLocation[i, 0];
                int gymBattle_r = World.gymBattleLocation[i, 1];
                int gymBattle_c = World.gymBattleLocation[i, 2];
                
                int imageNum = rnd.Next(1,4);
                world[gymBattle_m, gymBattle_r, gymBattle_c].addSpecialUsage(false, false, true, (World.gymBattleImage + "gymBattle0" + imageNum + "_01.png"));
                world[gymBattle_m, gymBattle_r, gymBattle_c + 1].addSpecialUsage(false, false, true, (World.gymBattleImage + "gymBattle0" + imageNum + "_02.png"));
            }

            for (int i = 0; i < (World.pokemonLocation.Length/3); i++)
            {
                int pokemon_m = World.pokemonLocation[i, 0];
                int pokemon_r = World.pokemonLocation[i, 1];
                int pokemon_c = World.pokemonLocation[i, 2];
                world[pokemon_m, pokemon_r, pokemon_c].addSpecialUsage(false, true, false, null);
            }
            return world;
        }

        public static Dictionary<int,Map> getMap() // Declare Map Direction
        {
            Map map1 = new Map(1);
            Map map2 = new Map(2);
            Map map3 = new Map(3);
            Map map4 = new Map(4);

            map1.SetMapDirection(null, map4, null, map2);
            map2.SetMapDirection(null, map3, map1, null);
            map3.SetMapDirection(map2, null, map4, null);
            map4.SetMapDirection(map1, null, null, map3);

            Dictionary<int, Map> mapList = new Dictionary<int, Map>();
            mapList.Add(1, map1);
            mapList.Add(2, map2);
            mapList.Add(3, map3);
            mapList.Add(4, map4);

            return mapList;
        }
    }
}
