using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zadanie1
{
    public partial class MainWindow : Window
    {
        private Bitmap img;
        Uri fileUri;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Upload_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() == true)
            {
                fileUri = new Uri(openFileDialog.FileName);
                imgDynamic.Source = new BitmapImage(fileUri);
                img = new Bitmap(openFileDialog.FileName);
            }
        }

        private void Save_Image(object sender, RoutedEventArgs e)
        {         
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|TIF Image|*.tif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.ShowDialog();
            
            if (saveFileDialog1.FileName != "")
            {
                
                FileStream fs =
                    (FileStream)saveFileDialog1.OpenFile();
                BitmapEncoder encoder;
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgDynamic.Source));
                        encoder.Save(fs);
                        break;
                    case 2:
                        encoder = new BmpBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgDynamic.Source));
                        encoder.Save(fs);
                        break;
                    case 3:
                        encoder = new GifBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgDynamic.Source));
                        encoder.Save(fs);
                        break;
                    case 4:
                        encoder = new TiffBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgDynamic.Source));
                        encoder.Save(fs);
                        break;


                }

                fs.Close();
            }
        }
      
           

        private void Pixel_read(object sender, MouseEventArgs e)
        {
            System.Windows.Point p = e.GetPosition((IInputElement)e.Source);
            System.Drawing.Color color = img.GetPixel((int)p.X, (int)p.Y);
            temp.Content = "RGB(" + color.R + ", " + color.G + ", " + color.B + ")";
        }

        private void Pixel_change(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition((IInputElement)e.Source);
            System.Drawing.Color color = img.GetPixel((int)p.X, (int)p.Y);
            img.SetPixel((int)p.X, (int)p.Y, System.Drawing.Color.Red);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            imgDynamic.Source = image;
        }
    }
}
