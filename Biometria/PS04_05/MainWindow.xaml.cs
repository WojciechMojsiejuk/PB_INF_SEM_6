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
using LiveCharts;
using LiveCharts.Wpf;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Zadanie1
{
    public partial class MainWindow : Window
    {
        private Bitmap img;
        Bitmap originImg;
        Uri fileUri;
        private Histogram histogram;
        private bool GreyScale;
        public MainWindow()
        {
            
            InitializeComponent();
            
            GreyScale = false;
        }

        private void Upload_Image(object sender, RoutedEventArgs e)
        {
            string name;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() == true)
            {
                fileUri = new Uri(openFileDialog.FileName);
                name = openFileDialog.FileName;
                imgDynamic.Source = new BitmapImage(fileUri);             
                img = new Bitmap(openFileDialog.FileName);
                originImg = new Bitmap(openFileDialog.FileName);
                if (img.PixelFormat==PixelFormat.Format8bppIndexed )img = CreateNonIndexedImage((System.Drawing.Image)img);
            }        
            Equalize.IsEnabled = false;
            LightSlider.IsEnabled = false;
            histogram = new Histogram(img);
            GreyScale = histogram.greyScale(img);
            SaveImg.IsEnabled = true;
            Histogram.IsEnabled = true;
            LightSlider.Value = 1;
            LightSlider.IsEnabled = true;
            Light.IsEnabled = true;
        }

        private void Save_Image(object sender, RoutedEventArgs e)
        {
            if (img == null)
            {
                MessageBox.Show("No Image!");
                return;
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png|Gif Image|*.gif|TIF Image|*.tif";
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
                        encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgDynamic.Source));
                        encoder.Save(fs);
                        break;
                    case 4:
                        encoder = new GifBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgDynamic.Source));
                        encoder.Save(fs);
                        break;
                    case 5:
                        encoder = new TiffBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgDynamic.Source));
                        encoder.Save(fs);
                        break;


                }

                fs.Close();
            }
        }

        public Bitmap CreateNonIndexedImage(System.Drawing.Image src)
        {
            Bitmap newBmp = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics gfx = Graphics.FromImage(newBmp))
            {
                gfx.DrawImage(src, 0, 0);
            }

            return newBmp;
        }

        private void Pixel_read(object sender, MouseEventArgs e)
        {
            System.Windows.Point p = e.GetPosition((IInputElement)e.Source);
            if((int)p.X>=0 && (int)p.Y>=0 && (int)p.Y<img.Height&& (int)p.X<img.Width)
            {
                System.Drawing.Color color = img.GetPixel((int)p.X, (int)p.Y);
                temp.Content = "RGB(" + color.R + ", " + color.G + ", " + color.B + ")";
            }
                
           
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

        private void create_histogram(object sender, RoutedEventArgs e)
        {
            GreyScale = histogram.greyScale(img);
            if (img==null)
            {
                MessageBox.Show("No Image!");
                return;
            }
            if (RCH.Series.Count > 0) {
                RCH.Series.RemoveAt(0);
                GCH.Series.RemoveAt(0);
                BCH.Series.RemoveAt(0);
                RGB3.Series.RemoveAt(0);

            }
            else if (Standard.Series.Count > 0)
            {
                Standard.Series.RemoveAt(0);
            }
            int[] standardArray = histogram.standardHistogram(img);
            int[,] rgbArray = histogram.RGBImage(img);
            if(GreyScale)
                drawNormalHist(standardArray);
            else 
                drawRGBHist(standardArray, rgbArray);
            LightSlider.IsEnabled = true;
            Extend.IsEnabled = true;
            Equalize.IsEnabled = true;
                                                                    
        }
        public void drawNormalHist(int[] standardArray)
        {
                SeriesCollection seriesCollection = new SeriesCollection();
                ColumnSeries standardcolumns = new ColumnSeries
                {
                    Title = "Standard",
                    Values = new ChartValues<int>(),
                    ColumnPadding = 0,
                    Fill = System.Windows.Media.Brushes.Gray
                };
                seriesCollection.Add(standardcolumns);
                for (int j = 0; j < 256; j++)
                {
                    seriesCollection[0].Values.Add(standardArray[j]);

                }
                Standard.Series.Add(seriesCollection[0]);
            
        }
        public void drawRGBHist(int[] standardArray, int[,] rgbArray)
        {
            SeriesCollection seriesCollection = new SeriesCollection();
            ColumnSeries rgb3columns = new ColumnSeries
            {
                Title = "RGB/3",
                Values = new ChartValues<int>(),
                ColumnPadding = 0,
                Fill = System.Windows.Media.Brushes.Pink
            };
            ColumnSeries rcolumns = new ColumnSeries
            {
                Title = "R channel",
                Values = new ChartValues<int>(),
                ColumnPadding = 0,
                Fill = System.Windows.Media.Brushes.Red
            };
            ColumnSeries gcolumns = new ColumnSeries
            {

                Title = "G channel",
                Values = new ChartValues<int>(),
                ColumnPadding = 0,
                Fill = System.Windows.Media.Brushes.Green
            };
            ColumnSeries bcolumns = new ColumnSeries
            {
                Title = "B channel",
                Values = new ChartValues<int>(),
                ColumnPadding = 0,
                Fill = System.Windows.Media.Brushes.Blue
            };
            seriesCollection.Add(rcolumns);
            seriesCollection.Add(gcolumns);
            seriesCollection.Add(bcolumns);
            seriesCollection.Add(rgb3columns);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    seriesCollection[i].Values.Add(rgbArray[i, j]);

                }
            }
            for (int j = 0; j < 256; j++)
            {
                seriesCollection[3].Values.Add(standardArray[j]);
                seriesCollection[3].Values.Add(standardArray[j] / 3);
            }
            RGB3.Series.Add(seriesCollection[3]);
            RCH.Series.Add(seriesCollection[0]);
            GCH.Series.Add(seriesCollection[1]);
            BCH.Series.Add(seriesCollection[2]);
        }
        private void equalize_histogram(object sender, RoutedEventArgs e)
        {            
            if (RCH.Series.Count > 0)
            {
                RCH.Series.RemoveAt(0);
                GCH.Series.RemoveAt(0);
                BCH.Series.RemoveAt(0);
                RGB3.Series.RemoveAt(0);

            }
            else if (Standard.Series.Count > 0)
            {
                Standard.Series.RemoveAt(0);
            }
            img =  histogram.equalizeHist(img);

            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            imgDynamic.Source = image;
            int[] standardArray = histogram.standardHistogram(img);
            int[,] rgbArray = histogram.RGBImage(img);
            if (GreyScale)
                drawNormalHist(standardArray);
            else
                drawRGBHist(standardArray, rgbArray);
            LightSlider.Value = 1;
        }

        private void extend_histogram(object sender, RoutedEventArgs e)
        {
            if (img == null)
            {
                MessageBox.Show("No Image!");
                return;
            }
            if (RCH.Series.Count > 0)
            {
                RCH.Series.RemoveAt(0);
                GCH.Series.RemoveAt(0);
                BCH.Series.RemoveAt(0);
                RGB3.Series.RemoveAt(0);

            }
            else if (Standard.Series.Count > 0)
            {
                Standard.Series.RemoveAt(0);
            }
            img = histogram.stretchHist(img);           
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            imgDynamic.Source = image;
            int[] standardArray = histogram.standardHistogram(img);
            int[,] rgbArray = histogram.RGBImage(img);
            if (GreyScale)
                drawNormalHist(standardArray);
            else
                drawRGBHist(standardArray, rgbArray);
            LightSlider.Value = 1;
        }
            
        private void Light_Click(object sender, RoutedEventArgs e)
        {
            if (img == null)
                return;
            Bitmap tempImg = img;
            tempImg = histogram.lightImage(img, LightSlider.Value);                 
            MemoryStream ms = new MemoryStream();
            tempImg.Save(ms, ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            imgDynamic.Source = image;
        }

        private void Original_Image(object sender, RoutedEventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            originImg.Save(ms, ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            imgDynamic.Source = image;
        }
    }
}
