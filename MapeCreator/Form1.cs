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
        private Camera _camera = new Camera();

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
                if ((r.x <= e.X && r.x + r.width >= e.X )
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
            button1.Focus();
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
            // zmienic rysowanie mapy
            int Width = int.Parse(textBox_WidthCell.Text);
            int Height = int.Parse(textBox_HeightCell.Text);

            Bitmap btm = new Bitmap(Width * 16 + 1, Height * 16 + 1);
            Bitmap CameraViewPoint = new Bitmap(_camera.CameraViewX, _camera.CameraViewY);


            int tx = _camera.X - 16;
            int ty = _camera.Y - 16;

            int vx = _camera.CameraViewX + 16;
            int vy = _camera.CameraViewY + 16;


            using (Graphics g = Graphics.FromImage(btm))
            {
                foreach (Cell c in Space)
                {
                    if ((c.x >= tx && c.x <= tx + vx)||
                        (c.y >= ty && c.y <= tx + vy))
                    {

                        if (c.rect != null)
                            g.DrawImage(c.rect.texture, c.x, c.y);
                        else
                            g.DrawRectangle(new Pen(Color.Black), c.x, c.y, 16, 16);
                    }
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            //CameraViewPoint = btm.Clone(new Rectangle(_camera.X, _camera.Y, _camera.CameraViewX, _camera.CameraViewY), System.Drawing.Imaging.PixelFormat.DontCare);

            using (Graphics g = Graphics.FromImage(CameraViewPoint))
            {
                g.DrawImage(btm, new Rectangle(0, 0, _camera.CameraViewX, _camera.CameraViewY), new Rectangle(_camera.X, _camera.Y, _camera.CameraViewX, _camera.CameraViewY), GraphicsUnit.Pixel);
            }

            pictureBox3.Image = CameraViewPoint;
        }
        private void buttonSaveChar_Click(object sender, EventArgs e)
        {
            int Width = int.Parse(textBox_WidthCell.Text);
            int Height = int.Parse(textBox_HeightCell.Text);
            SaveMeneger.Save(Space, Width, Height, textBoxName.Text);
            button1.Focus();
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (Clickedkey == Keys.R)
            {
                IsDown = true;
                _camera.CameraClickPos = new Pos(e.X, e.Y);
                _camera.MousePos = new Pos(e.X, e.Y);
            }
            else
            {
                foreach (Cell r in Space)
                {
                    if ((r.x <= e.X + _camera.X && r.x + r.width >= e.X + _camera.X)
                        && (r.y <= e.Y + _camera.Y && r.y + r.height >= e.Y + _camera.Y))
                    {
                        r.rect = ClickedTexture;
                    }
                }
                RefreshPictureBox();
                
            }
            button1.Focus();
        }
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            IsDown = false;
            _camera.CameraClickPos = null;
        }

        private bool IsDown = false;
        private Keys Clickedkey;

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDown && Clickedkey == Keys.R)
            {
                bool Left = e.X > _camera.MousePos.x ? false : true;
                bool Up = e.Y > _camera.MousePos.y ? false : true;

                if (Left)
                    _camera.X += (_camera.CameraClickPos.x - e.X) * 1;
                if (!Left)
                    _camera.X -= -(_camera.CameraClickPos.x - e.X);

                if (Up)
                    _camera.Y += (_camera.CameraClickPos.y - e.Y) * 1;
                if (!Up)
                    _camera.Y -= -(_camera.CameraClickPos.y - e.Y);

                if (_camera.X < 0)
                    _camera.X = 0;

                if (_camera.Y < 0)
                    _camera.Y = 0;

                _camera.CameraClickPos = new Pos(e.X, e.Y);

                RefreshPictureBox();
            }
        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
                Clickedkey = Keys.R;
        }

        private void button1_KeyUp(object sender, KeyEventArgs e)
        {
            IsDown = false;
            Clickedkey = Keys.XButton1;
            _camera.CameraClickPos = null;
        }

        private void textBox_WidthCell_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
                Clickedkey = Keys.R;
        }

        private void textBox_WidthCell_KeyUp(object sender, KeyEventArgs e)
        {
            IsDown = false;
            Clickedkey = Keys.XButton1;
            _camera.CameraClickPos = null;
        }
    }




    //dodawanie przez przeciągniecie myszką
    //wiecej textur
    //wiecej TypeTile
    //wczytywanie map do edytowania
}
