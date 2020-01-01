using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MapeCreator
{
    enum TileTypes
    {
        Wall,
        Surface,
        none
    }
    class SaveMeneger
    {
        public static void Save(List<Cell> Space, int height, int width, string MapName)
        {
            char[] Charackers = new char[height * width];
            int index = 0;
            foreach (Cell x in Space)
            {
                try
                {
                    switch (x.rect.TileType)
                    {
                        case TileTypes.none:
                            Charackers[index] = '-';
                            break;
                        case TileTypes.Surface:
                            Charackers[index] = 'b';
                            break;
                        case TileTypes.Wall:
                            Charackers[index] = 'w';
                            break;
                    }
                }
                catch (NullReferenceException)
                {
                    Charackers[index] = '-';
                }
                index++;
            }
            SaveInstance save = new SaveInstance();
            save.Characters = Charackers;
            save.Height = height;
            save.Width = width;

            string AppFullname = Assembly.GetEntryAssembly().FullName;
            string Appname = AppFullname.Split(',')[0] + ".exe";
            string PathFiles = Assembly.GetEntryAssembly().Location.Replace(Appname, "");
            string FinalPath = PathFiles + $"{MapName}.json";

            CreateFile(FinalPath, save,true);
        }
        public static void CreateFile(string filePath, SaveInstance objectToWrite, bool append)
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }

    class SaveInstance
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public char[] Characters { get; set; }
    }
}
