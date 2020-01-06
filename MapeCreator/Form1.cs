using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapeCreator
{
    public partial class Form1 : Form
    {
        List<Cell> Space = new List<Cell>();
        private Camera _camera = new Camera();
        private Form2 form2;


        RectanglePicture ClickedTexture;

        public Form1()
        {
            InitializeComponent();
           
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
            GridCreator.Focus();
        }
        private void RefreshPictureBox(int? height = null, int? width = null)
        {
            int Width = width == null ? int.Parse(textBox_WidthCell.Text) : width.Value;
            int Height = height == null ? int.Parse(textBox_HeightCell.Text) : height.Value;

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
                    if ((c.x >= tx && c.x <= tx + vx) ||
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
            GridCreator.Focus();
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
                        r.rect = form2.GetCllickedObject();
                    }
                }
                RefreshPictureBox();
            }
            GridCreator.Focus();
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

        private void MapLoader_Click(object sender, EventArgs e)
        {
            string Filepath = string.Empty;
            string FileContent = string.Empty;
            using (OpenFileDialog DialogFile = new OpenFileDialog())
            {
                DialogFile.DefaultExt = "json";
                if (DialogFile.ShowDialog() == DialogResult.OK)
                {
                    Filepath = DialogFile.FileName;
                }
                DialogFile.Dispose();
            }

            using (StreamReader reader = new StreamReader(Filepath))
            {
                FileContent = reader.ReadToEnd();
                reader.Dispose();
            }

            SaveInstance map = JsonConvert.DeserializeObject<SaveInstance>(FileContent);

            Space.Clear();

            string[] characters = map.Characters.Split(',');

            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    switch (characters[(map.Width*j)+i])
                    {
                        case "-":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16));
                            break;
                        case "f1":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(0).texture, TileTypes.FloorR1)));
                            break;
                        case "f2":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(1).texture, TileTypes.FloorR2)));
                            break;
                        case "f3":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(2).texture, TileTypes.FloorR3)));
                            break;
                        case "f4":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(3).texture, TileTypes.FloorR4)));
                            break;
                        case "f5":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(4).texture, TileTypes.FloorR5)));
                            break;
                        case "f6":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(5).texture, TileTypes.FloorR6)));
                            break;
                        case "f7":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(6).texture, TileTypes.FloorR7)));
                            break;
                        case "f8":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(7).texture, TileTypes.FloorR8)));
                            break;
                        case "f9":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(8).texture, TileTypes.FloorR9)));
                            break;
                        case "f10":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(9).texture, TileTypes.FloorR10)));
                            break;
                        case "w1":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(10).texture, TileTypes.WallR1)));
                            break;
                        case "w2":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(11).texture, TileTypes.WallR2)));
                            break;
                        case "w3":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(12).texture, TileTypes.WallR3)));
                            break;
                        case "w4":
                            Space.Add(new Cell(i * 16, j * 16, 16, 16, new RectanglePicture(0, 0, 16 * 2, 16 * 2, form2.GetTextureObject(13).texture, TileTypes.WallR4)));
                            break;
                    }
                }
            }
            textBox_HeightCell.Text = map.Height.ToString();
            textBox_WidthCell.Text = map.Width.ToString();
            RefreshPictureBox(map.Width, map.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form2 = new Form2(this.pictureBox2);
            form2.Show();
            form2.Hide();
        }

        private void buttonTextures_Click(object sender, EventArgs e)
        {
            form2.Show();
        }
    }


    //dodawanie przez przeciągniecie myszką
}
