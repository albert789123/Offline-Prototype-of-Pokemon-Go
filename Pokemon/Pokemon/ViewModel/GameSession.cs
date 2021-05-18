using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Pokemon.Factory;
using Pokemon.Model;
using Grid = Pokemon.Model.Grid;

namespace Pokemon.ViewModel
{
    public class GameSession : BaseNotificationClass
    {
        private PlayerModel _currentPlayer;
        private Location _currentLocation;
        private Pokemon.Model.Grid[,,] _worldMap;
        private Dictionary<int, Map> _mapDirection = new Dictionary<int, Map>();
        private int mapNumber;

        public PlayerModel CurrentPlayer{
            get => _currentPlayer;
            private set { 
                _currentPlayer = value;
                OnPropertyChanged();
            }
        }

        public Location CurrentLocation
        {
            get => _currentLocation;


            private set { _currentLocation = value; }
        }

        public Pokemon.Model.Grid[,,] WorldMap
        {
            get { return _worldMap; }
            private set { _worldMap = value; }
        }

        public int MapNumber
        {
            get { return mapNumber; }
            private set { mapNumber = value; }
        }

        public Dictionary<int, Map> MapDirection
        {
            get { return _mapDirection; }
            private set { _mapDirection = value; }
        }

        public GameSession(PlayerModel newPlayer)
        {
            CurrentPlayer = newPlayer;
            WorldMap = WorldFactory.generateWorld();
            MapDirection = WorldFactory.getMap();
            MapNumber = 1;
            _currentLocation = new Location(_worldMap[MapNumber, 1,1], MapNumber);

        }

        public void UpdatePlayerPosition(int mapNum, System.Windows.Point playerPosition)
        {
            if (mapNum != MapNumber)
            {
                MapNumber = mapNum;
            }
            int colGrid = (int) ( (playerPosition.X + 400) / 80); // Need + 400
            int rowGrid = (int) ( (playerPosition.Y + 400) / 80); // Need + 400
            Grid tmp = _worldMap[MapNumber-1, rowGrid, colGrid];
            _currentLocation = new Location(_worldMap[MapNumber-1, rowGrid, colGrid], MapNumber);
            return;
        }
    }
}
