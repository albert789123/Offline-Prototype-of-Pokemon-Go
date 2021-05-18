using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Model
{
    public class Map
    {
        private int mapNumber;
        public int MapNumber
        {
            get { return mapNumber; }
            private set { mapNumber = value; }
        }

        private Map right;
        public Map Right
        {
            get { return right; }
            private set { right = value; }
        }
        private Map left;
        public Map Left
        {
            get { return left; }
            private set { left = value; }
        }
        private Map up;
        public Map Up
        {
            get { return up; }
            private set { up = value; }
        }
        private Map down;
        public Map Down
        {
            get { return down; }
            private set { down = value; }
        }

        public Map(int mapNumber)
        {
            this.MapNumber = mapNumber;
        }

        public void SetMapDirection(Map up, Map down, Map left, Map right)
        {
            if (up != null)
                this.Up = up;

            if(down != null)
                this.Down = down;

            if (left != null)
                this.Left = left;

            if (right != null)
                this.Right = right;
        }
    }
}
