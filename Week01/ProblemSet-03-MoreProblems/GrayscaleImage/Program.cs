using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace GrayscaleImage
{
    class Program
    {
        static void GrayScaleImage(Bitmap bitmap, string savePath)
        {
            Bitmap grayImage = new Bitmap(bitmap.Width, bitmap.Height);

            int x, y;
            for (x = 0; x < bitmap.Width; x++)
            {
                for (y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);

                    int grayScale = (int)((pixelColor.R * 0.27) + (pixelColor.G * 0.58) + (pixelColor.B * 0.15));
                    Color newColor = Color.FromArgb(pixelColor.A, grayScale, grayScale, grayScale);

                    grayImage.SetPixel(x, y, newColor);
                }
            }

            grayImage.Save(savePath);
        }

        [STAThread]
        static void Main(string[] args)
        {
            Bitmap imageForGrayscale;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap images (*.bmp)|*.bmp";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            try
            {

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        imageForGrayscale = (Bitmap)Image.FromFile(openFileDialog.FileName, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Could not open file. Error: {0}", ex.Message);
                        Console.ReadKey();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("No file selected! Program will terminate!");
                    Console.ReadKey();
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("File opened succesfully!");

            string newPath = openFileDialog.FileName;
            newPath = newPath.Substring(0, newPath.Length - 4) + "_gray.bmp";
            try
            {
                GrayScaleImage(imageForGrayscale, newPath);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not convert or save image! Error: {0}", ex.Message);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("File converted and saved successfuly to:");
            Console.WriteLine(newPath);
            Console.ReadKey();
        }
    }
}
