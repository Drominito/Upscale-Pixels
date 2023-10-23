using System;
using System.Collections.Generic;
using System.Windows;


using Upscale_Pixels.Code.Data.PixelManager;
using Upscale_Pixels.Code.Data.User;

namespace Upscale_Pixels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string DropBlockText = "You can Drop the File here!";
        
        List<DragEventArgs> SeveralEDataList = new List<DragEventArgs>();
        
        public string MainInputImagePath = "";
        
        public string MainOutputImagepath = "";

        public MainWindow()
        {
            InitializeComponent();
            DropTextBlock.Text = DropBlockText;

            PixelPro.CreateBitmapIstancesAndSimilar(MainInputImagePath, MainOutputImagepath, OutputImage);

            
            UserData Drominito = new UserData("Drominito");
            // Hier wird dann in der Combobox die Namen hinzugefügt, falls es diese Datei gibt
            
            //comboboxitem.Content = 
            //ImagesHistory.Items = Com;

            //DataContext = new MyViewModel(); // MVVM werde ich erst später nutzen, und ich möchte damit besser werden ! >:(
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PixelEditing.CreateBitmapIstancesAndSimilar(MainInputImagePath, MainOutputImagepath, OutputImage);

        }

        private void LabelDrop_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                HistoryData.ShowFileData(e, InfoBox, SeveralEDataList);
                MainInputImagePath = HistoryData.ShowFileImage(e, OriginalImage);
            }
        }
        //private void Window_KeyDown(object sender, KeyEventArgs e) { }
        private void LabelDrop_PreviewDrop(object sender, DragEventArgs e) { }

    }

    
}