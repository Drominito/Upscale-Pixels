using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Upscale_Pixels.Code.DataManagment
{
    public static class HistoryData
    {
        public static string ImagePathFeld { get; set; }
        public static string FilePathFeld { get; private set; }

        public static void   CreateImagesHistoryPath()
        {
            string DirectoryName = "History";
            string FileName = "ImagesHistory";
            string FileEnding = ".hist";
            string FilePath = "NaN";
            //FileInfo? HistoryFileInfo = null;



            string FirstFolderPath = @$"{AppContext.BaseDirectory}{DirectoryName}";

            if (File.Exists(FirstFolderPath) != true)
            {
                
                CreateFirstFolder(FirstFolderPath);
                
                FilePath = $@"{FirstFolderPath}\{FileName}{FileEnding}";
                FilePathFeld = FilePath;
                
                



                //HistoryFileInfo = CreateHistoryFileInstance(HistoryFileInfo, Path);
                //AddInformationToHistoryFile(false, Path); // Es soll der Pfad in die Datei geschreiben werden
            }
        }

        private static FileInfo CreateHistoryFileInstance(FileInfo? HistoryFileInfo, string Path)
        {
            HistoryFileInfo = new FileInfo(Path);

            return HistoryFileInfo;
        }

        public static void   CreateFirstFolder(string HistoryFolderPath) { Directory.CreateDirectory(HistoryFolderPath); }
        
        
        public static void   AddInformationToHistoryFile(bool Executeable, string FilePath, string ImagePath)
        {
            
            ImagePath = ImagePath; // hmmm, vielleicht soll das Feld ein Array sein?

            FileInfo? WriteInformations = new FileInfo(ImagePath);

            if (Executeable)
            {
                string FileName = WriteInformations.Name; // Der name von der Datei, dem Pfad "Path"

                if (WriteInformations != null && null == null)
                {
                    //WriteInformations = null;
                    StreamWriter sw = new StreamWriter(FilePath);
                    sw.WriteLine($"Path : {ImagePath} | Name : {FileName}");
                    
                    sw.Close();
                    sw.Dispose();
                }

            }
        }
        
        public static void   GetHistoryFilePath(string Path)
        {
            // Das ist später für die ComboBox einzeige
            FileInfo GetFileName = new FileInfo(Path);
            string FileName = GetFileName.Name;
        }
        
        public static long   GetFileSize(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length; // Die Dateigröße in Bytes
            }
            catch
            {
                // Hier können Sie Fehlerbehandlung für den Fall hinzufügen, dass die Datei nicht gefunden wird oder andere Probleme auftreten
                return -1; // Geben Sie -1 zurück, um anzuzeigen, dass ein Fehler aufgetreten ist
            }
        }
        
        public static void   WriteDataText(DragEventArgs e, System.Windows.Controls.TextBlock InfoBox)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string filePath = files[0]; // Wir nehmen an, dass es sich um eine einzelne Datei handelt
                    long ByteFileSize = GetFileSize(filePath);

                    long MegaByteFileSize = GetFileSize(filePath);
                    decimal MegaByteFileSize_Int = Convert.ToDecimal(MegaByteFileSize);
                    MegaByteFileSize_Int /= 1000000;

                    string InfoData = $"File Size:\n" +
                                      $"{ByteFileSize}Bytes\n" +
                                      $"{MegaByteFileSize_Int}Megabytes";

                    InfoBox.Text = $"{InfoBox.Text}\n{InfoData}";
                }
            }
        }
        
        public static void   ShowFileData(DragEventArgs e, System.Windows.Controls.TextBlock InfoBox, List<DragEventArgs> SeveralEDataList)
        {
            SeveralEDataList.Add(e);
            InfoBox.Text = $"";
            WriteDataText(SeveralEDataList.Last(), InfoBox);
        }
        public static string   ShowFileImage(DragEventArgs e,  System.Windows.Controls.Image OriginalImage)
        {
            string[] Files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string ImagePath = Files[0];

            BitmapImage bitmapImage = new BitmapImage(new Uri(ImagePath));
            OriginalImage.Source = bitmapImage;


            //GetHistoryFilePath(Path);
            HistoryData.AddInformationToHistoryFile(true, FilePathFeld, ImagePath);


            return ImagePath;
        }
        
    }
}
