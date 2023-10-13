using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upscale_Pixels.Code.DataManagment
{
    public static class HistoryData
    {
        public static void   CreateImagesHistoryPath()
        {
            string DirectoryName = "History";
            string FileName = "ImagesHistory";
            string FileEnding = ".hist";
            string Path = "NaN";


            string HistoryFolderPath = $@"{AppContext.BaseDirectory}{DirectoryName}";


            if (File.Exists(HistoryFolderPath) != true)
            {
                CreateHistoryFolder(HistoryFolderPath);
                Path = CreateHistoryFile(HistoryFolderPath, FileName, FileEnding);


                //AddInformationToHistoryFile(false, Path); // Es soll der Pfad in die Datei geschreiben werden
            }
        }
        public static void   AddInformationToHistoryFile(bool Executeable, string Path)
        {
            FileInfo WriteInformations = new FileInfo(Path);

            if (Executeable)
            {
                string FileName = WriteInformations.Name; // Der name von der Datei, dem Pfad "Path"

                if (WriteInformations != null)
                {
                    WriteInformations = null;
                    StreamWriter sw = new StreamWriter(Path);
                    sw.Write($"Path : {Path} | Name : {FileName}");
                }

            }
        }
        public static void   CreateHistoryFolder(string HistoryFolderPath) { Directory.CreateDirectory(HistoryFolderPath); }
        public static string CreateHistoryFile(string HistoryFolderPath, string FileName, string FileEnding)
        {
            string Input = $@"{HistoryFolderPath}\{FileName}{FileEnding}";

            File.Create(Input);


            return Input;
        }
        public static void   GetHistoryFilePath(string Path)
        {
            // Das ist später für die ComboBox einzeige
            FileInfo GetFileName = new FileInfo(Path);
            string FileName = GetFileName.Name;
        }




    }
}
