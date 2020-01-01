using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapeCreator
{
    public partial class Form1 : Form
    {
        List<RectanglePicture> Textures = new List<RectanglePicture>();
        List<Cell> Space = new List<Cell>();

        RectanglePicture ClickedTexture;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bitmap = (Bitmap)(MapeCreator.Properties.Resources.MapSprite);
            Rectangle[] rect = { new Rectangle(16, 16, 16, 16), new Rectangle(96, 0, 16, 16), new Rectangle(64, 112, 16, 16),
                                 new Rectangle(0, 144, 16, 16), new Rectangle(16, 144, 16, 16), new Rectangle(96, 138, 16, 22)};

            Bitmap Picturebitmap = new Bitmap(32 * rect.Length, 44);

            for (int i = 0; i < rect.Length; i++)
            {
                if (i <= 3)
                    Textures.Add(new RectanglePicture(i * 32, 0, 16 * 2, 22 * 2, bitmap.Clone(rect[i], System.Drawing.Imaging.PixelFormat.DontCare), TileTypes.Wall));
                else if (i == rect.Length)
                    Textures.Add(new RectanglePicture(i * 32, 0, 16 * 2, 16 * 2, bitmap.Clone(rect[i], System.Drawing.Imaging.PixelFormat.DontCare), TileTypes.none));
                else
                    Textures.Add(new RectanglePicture(i * 32, 0, 16 * 2, 22 * 2, bitmap.Clone(rect[i], System.Drawing.Imaging.PixelFormat.DontCare), TileTypes.none));
            }

            using (Graphics g = Graphics.FromImage(Picturebitmap))
                foreach (RectanglePicture x in Textures)
                {
                    g.DrawImage(ScaleUpImage(x.texture), x.x, 0);
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
                    pictureBox2.Image = ScaleUpImage(ClickedTexture.texture);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_HeightCell.Text != String.Empty || textBox_WidthCell.Text != String.Empty)
            {
                Space.Clear();
                int Width = int.Parse(textBox_WidthCell.Text);
                int Height = int.Parse(textBox_HeightCell.Text);

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        Space.Add(new Cell(j * 16, i * 16, 16, 16));
                    }
                }
                RefreshPictureBox();
            }
        }
        private Bitmap ScaleUpImage(Bitmap b)
        {
            Bitmap t1 = new Bitmap(32, 44);
            using (Graphics c = Graphics.FromImage(t1))
            {
                c.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                c.DrawImage(b, 0, 0, 32, 44);
            }
            return t1;
        }
        private void RefreshPictureBox()
        {
            int Width = int.Parse(textBox_WidthCell.Text);
            int Height = int.Parse(textBox_HeightCell.Text);

            Bitmap btm = new Bitmap(Width * 16 + 1, Height * 16 + 1);

            using (Graphics g = Graphics.FromImage(btm))
            {
                foreach (Cell c in Space)
                {
                    if (c.rect != null)
                        g.DrawImage(c.rect.texture, c.x, c.y);
                    else
                        g.DrawRectangle(new Pen(Color.Black), c.x, c.y, 16, 16);
                }
            }
            pictureBox3.Image = btm;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Cell r in Space)
            {
                if ((r.x <= e.X && r.x + r.width >= e.X)
                    && (r.y <= e.Y && r.y + r.height >= e.Y))
                {
                    r.rect = ClickedTexture;
                }
            }
            RefreshPictureBox();
        }

        private void buttonSaveChar_Click(object sender, EventArgs e)
        {
            int Width = int.Parse(textBox_WidthCell.Text);
            int Height = int.Parse(textBox_HeightCell.Text);
            SaveMeneger.Save(Space,Width,Height,textBoxName.Text);
        }
    }

    //dodawanie przez przeciągniecie myszką
    //wiecej textur
    //wiecej TypeTile

}
