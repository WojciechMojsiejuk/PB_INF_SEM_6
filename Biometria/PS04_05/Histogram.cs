using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Zadanie1
{
    public class Histogram
    {
        private int[] red = null;
        private int[] green = null;
        private int[] blue = null;
        public int[] getRed()
        {
            return this.red;
        }
        public int[] getGreen()
        {
            return this.green;
        }
        public int[] getBlue()
        {
            return this.blue;
        }
        public Histogram(Bitmap image)
        {
            red = new int[256];
            green = new int[256];
            blue = new int[256];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    red[pixelColor.R]++;
                    blue[pixelColor.B]++;
                    green[pixelColor.G]++;                
                }
            }
        }
        public int[,] RGBImage(Bitmap image)
        {
            int[,] histogram = new int[3, 256];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                   
                    histogram[0, pixelColor.R]++;
                    histogram[1, pixelColor.G]++;
                    histogram[2, pixelColor.B]++;
                }
            }
            
            return histogram;
        }
        public bool greyScale(Bitmap image)
        {
            double delta = 0.006;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    if(Math.Abs(pixelColor.R- pixelColor.G)>delta || Math.Abs(pixelColor.R - pixelColor.B) > delta || Math.Abs(pixelColor.G - pixelColor.B) > delta)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public int[] standardHistogram(Bitmap image)
        {
            int[] histogram = new int[256];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    histogram[pixelColor.R]++;
                    histogram[pixelColor.G]++;
                    histogram[pixelColor.B]++;
                }
            }
            return histogram;
        } 
        public int[] getLUTstretch(int[] values)
        {
            int minValue = 0;
            for (int i = 0; i < 256; i++)
            {
                if (values[i] != 0)
                {
                    minValue = i;
                    break;
                }
            }
            int maxValue = 255;
            for (int i = 255; i >= 0; i--)
            {
                if (values[i] != 0)
                {
                    maxValue = i;
                    break;
                }
            }
            int[] result = new int[256];
            double a = 255.0 / (maxValue - minValue);
            for (int i = 0; i < 256; i++)
            {
                result[i] = (int)(a * (i - minValue));
                if (result[i] < 0) result[i] = 0;
                if (result[i] > 255) result[i] = 255;
            }

            return result;
        }
        public int[] getLutEqualize(int[] values,int size)
        {
           
            double minValue = 0;
            for (int i = 0; i < 256; i++)
            {
                if (values[i] != 0 )
                {
                    minValue = values[i];
                    break;
                }
            }
            int[] result = new int[256];
            
            double sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += values[i];
                result[i] = (int)(((sum - minValue) / (size - minValue)) * 255.0);
            }

            return result;
        }
        public int[] getLUTBright(double value)
        {
            int[] LUT = new int[256];
            for (int i = 0; i < 256; i++)
            {
                if (Math.Pow(i, value) > 255)
                {
                    LUT[i] = 255;
                }
                else
                {
                    LUT[i] = (int)(Math.Pow(i, value));
                }
            }
            return LUT;
           
        }      
        public  Bitmap stretchHist(Bitmap oldImg)
        {
            int[] LUTred = getLUTstretch(red);
            int[] LUTgreen = getLUTstretch(green);
            int[] LUTblue = getLUTstretch(blue);          
            Bitmap newImg = new Bitmap(oldImg.Width, oldImg.Height, PixelFormat.Format24bppRgb);
            for (int x = 0; x < oldImg.Width; x++)
            {
                for (int y = 0; y < oldImg.Height; y++)
                {
                    Color pixel = oldImg.GetPixel(x, y);
                    Color newPixel = Color.FromArgb(LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);
                    newImg.SetPixel(x, y, newPixel);                  
                }
            }
            return newImg;
        }
        public Bitmap equalizeHist(Bitmap oldImg)
        {
            int[] LUTred = getLutEqualize(red, oldImg.Width * oldImg.Height);
            int[] LUTgreen = getLutEqualize(green, oldImg.Width * oldImg.Height);
            int[] LUTblue = getLutEqualize(blue, oldImg.Width * oldImg.Height);                     
            Bitmap newBitmap = new Bitmap(oldImg.Width, oldImg.Height, PixelFormat.Format24bppRgb);
            for (int x = 0; x < oldImg.Width; x++)
            {
                for (int y = 0; y < oldImg.Height; y++)
                {
                    Color pixel = oldImg.GetPixel(x, y);
                    Color newPixel = Color.FromArgb(LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);
                    newBitmap.SetPixel(x, y, newPixel);                   
                }
            }
            return newBitmap;
        }
        public Bitmap lightImage(Bitmap oldImg,double value)
        {
               
            int[] LUTred = getLUTBright(value);
            int[] LUTgreen = getLUTBright(value);
            int[] LUTblue = getLUTBright(value);
            Bitmap newImg = new Bitmap(oldImg.Width, oldImg.Height, PixelFormat.Format24bppRgb);
            for (int x = 0; x < oldImg.Width; x++)
            {
                for (int y = 0; y < oldImg.Height; y++)
                {
                    Color pixel = oldImg.GetPixel(x, y);
                    Color newPixel = Color.FromArgb(LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);
                    newImg.SetPixel(x, y, newPixel);
                }
            }
            return newImg;
        }

    }
}
