using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeCreator
{
    class Camera
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        const int TileSize = 16;
        const int TileCount = 25;

        public int CameraViewX { get;  } = TileCount * TileSize;
        public int CameraViewY { get;  } = TileCount * TileSize;

        public Pos CameraClickPos { get; set; }
        public Pos MousePos { get; set; }
    }


    class Pos
    {
        public int x { get; set; }
        public int y { get; set; }

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
