using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapeCreator
{
    public partial class Form2 : Form
    {
        const int width = 7;
        const int height = 6;
        List<RectanglePicture> Textures = new List<RectanglePicture>();
        RectanglePicture ClickedTexture;
        

        public RectanglePicture GetCllickedObject() => ClickedTexture;
        public RectanglePicture GetTextureObject(int n)
        {
            Console.WriteLine(Textures.Count);
           return Textures[n];
        }
        

        PictureBox p1;

        public Form2(PictureBox p1)
        {
            this.p1 = p1;
            InitializeComponent();
        }

        public static string appPath()
        {
            string AppFullname = Assembly.GetEntryAssembly().FullName;
            string Appname = AppFullname.Split(',')[0] + ".exe";
           return Assembly.GetEntryAssembly().Location.Replace(Appname, "");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Bitmap Picturebitmap = new Bitmap(width * 32, 3 * 32);


            Textures.Add(new RectanglePicture(0 * 32, 0 * 32, 32, 32, MapeCreator.Properties.Resources.floor_1));
            Textures.Add(new RectanglePicture(1 * 32, 0 * 32, 32, 32, MapeCreator.Properties.Resources.floor_2));
            Textures.Add(new RectanglePicture(2 * 32, 0 * 32, 32, 32, MapeCreator.Properties.Resources.floor_3));
            Textures.Add(new RectanglePicture(3 * 32, 0 * 32, 32, 32, MapeCreator.Properties.Resources.floor_4));
            Textures.Add(new RectanglePicture(4 * 32, 0 * 32, 32, 32, MapeCreator.Properties.Resources.floor_5));
            Textures.Add(new RectanglePicture(5 * 32, 0 * 32, 32, 32, MapeCreator.Properties.Resources.floor_6));
            Textures.Add(new RectanglePicture(6 * 32, 0 * 32, 32, 32, MapeCreator.Properties.Resources.floor_7));
            Textures.Add(new RectanglePicture(0 * 32, 1 * 32 + 1, 32, 32, MapeCreator.Properties.Resources.floor_8));
            Textures.Add(new RectanglePicture(1 * 32, 1 * 32 + 1, 32, 32, MapeCreator.Properties.Resources.floor_9));
            Textures.Add(new RectanglePicture(2 * 32, 1 * 32 + 1, 32, 32, MapeCreator.Properties.Resources.floor_10));
            Textures.Add(new RectanglePicture(3 * 32, 1 * 32 + 1, 32, 32, MapeCreator.Properties.Resources.wall_1));
            Textures.Add(new RectanglePicture(4 * 32, 1 * 32 + 1, 32, 32, MapeCreator.Properties.Resources.wall_2));
            Textures.Add(new RectanglePicture(5 * 32, 1 * 32 + 1, 32, 32, MapeCreator.Properties.Resources.wall_3));
            Textures.Add(new RectanglePicture(6 * 32, 1 * 32 + 1, 32, 32, MapeCreator.Properties.Resources.wall_crack));

            Textures[0].TileType = TileTypes.FloorR1;
            Textures[1].TileType = TileTypes.FloorR2;
            Textures[2].TileType = TileTypes.FloorR3;
            Textures[3].TileType = TileTypes.FloorR4;
            Textures[4].TileType = TileTypes.FloorR5;
            Textures[5].TileType = TileTypes.FloorR6;
            Textures[6].TileType = TileTypes.FloorR7;
            Textures[7].TileType = TileTypes.FloorR8;
            Textures[8].TileType = TileTypes.FloorR9;
            Textures[9].TileType = TileTypes.FloorR10;
            Textures[10].TileType = TileTypes.WallR1;
            Textures[11].TileType = TileTypes.WallR2;
            Textures[12].TileType = TileTypes.WallR3;
            Textures[13].TileType = TileTypes.WallR4;

            using (Graphics g = Graphics.FromImage(Picturebitmap))
                foreach (RectanglePicture x in Textures)
                {
                    g.DrawImage(ScaleUpImage(x.texture), x.x, x.y);
                }
            pictureBox1.Image = Picturebitmap;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (RectanglePicture r in Textures)
            {
                if ((r.x <= e.X && r.x + r.width >= e.X)
                    && (r.y <= e.Y && r.y + r.height >= e.Y))
                {
                    ClickedTexture = r;
                    p1.Image = ScaleUpImage(r.texture);
                }
            }
        }
        public static Bitmap ScaleUpImage(Bitmap b)
        {
            Bitmap t1 = new Bitmap(32, 32);
            using (Graphics c = Graphics.FromImage(t1))
            {
                c.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                c.DrawImage(b, 0, 0, 32, 44);
            }
            return t1;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
