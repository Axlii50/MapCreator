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
    public enum TileTypes
    {
        WallR1,
        WallR2,
        WallR3,
        WallR4,
        WallR5,
        WallR6,
        WallR7,
        WallR8,
        WallR9,
        FloorR1,
        FloorR2,
        FloorR3,
        FloorR4,
        FloorR5,
        FloorR6,
        FloorR7,
        FloorR8,
        FloorR9,
        FloorR10,
        none
    }
    class SaveMeneger
    {
        public static void Save(List<Cell> Space, int height, int width, string MapName)
        {
            string Charackers = string.Empty;
            int index = 0;
            foreach (Cell x in Space)
            {
                try
                {
                    switch (x.rect.TileType)
                    {
                        case TileTypes.none:
                            Charackers += "-,";
                            break;
                        case TileTypes.FloorR1:
                            Charackers += "f1,";
                            break;
                        case TileTypes.FloorR2:
                            Charackers += "f2,";
                            break;
                        case TileTypes.FloorR3:
                            Charackers += "f3,";
                            break;
                        case TileTypes.FloorR4:
                            Charackers += "f4,";
                            break;
                        case TileTypes.FloorR5:
                            Charackers += "f5,";
                            break;
                        case TileTypes.FloorR6:
                            Charackers += "f6,";
                            break;
                        case TileTypes.FloorR7:
                            Charackers += "f7,";
                            break;
                        case TileTypes.FloorR8:
                            Charackers += "f8,";
                            break;
                        case TileTypes.FloorR9:
                            Charackers += "f9,";
                            break;
                        case TileTypes.FloorR10:
                            Charackers += "f10,";
                            break;
                        case TileTypes.WallR1:
                            Charackers += "w1,";
                            break;
                        case TileTypes.WallR2:
                            Charackers += "w2,";
                            break;
                        case TileTypes.WallR3:
                            Charackers += "w3,";
                            break;
                        case TileTypes.WallR4:
                            Charackers += "w4,";
                            break;
                    }
                }
                catch (NullReferenceException)
                {
                    Charackers += "-,";
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
        public string Characters { get; set; }
    }
}
