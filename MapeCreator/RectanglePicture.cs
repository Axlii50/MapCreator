using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeCreator
{
    public class RectanglePicture
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Bitmap texture { get; set; }
        public TileTypes TileType { get; set; }

        public RectanglePicture(int x, int y, int width, int height, Bitmap texture,TileTypes? type = null)
        {
            if(type != null) TileType = type.Value;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.texture = texture;
        }
    }
}
