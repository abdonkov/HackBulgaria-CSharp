using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ApplyALinearFilterToAnImage
{
    class Program
    {
        static void BoxBlur(Bitmap bitmap, int curX, int curY, ref int newRed, ref int newGreen, ref int newBlue, int blurRange = 1)
        {
            newRed = 0;
            newGreen = 0;
            newBlue = 0;

            int pixelCounted = 0;

            for (int k = curX - blurRange; k <= curX + blurRange; k++)
            {
                for (int l = curY - blurRange; l <= curY + blurRange; l++)
                {
                    if (k >= 0 && k < bitmap.Width && l >= 0 && l < bitmap.Height)
                    {
                        Color curColor = bitmap.GetPixel(k, l);
                        newRed += curColor.R;
                        newGreen += curColor.G;
                        newBlue += curColor.B;
                        pixelCounted++;
                    }
                }

            }

            newRed /= pixelCounted;
            newGreen /= pixelCounted;
            newBlue /= pixelCounted;
        }
        static void BlurImage(Bitmap bitmap, string savePath)
        {
            Bitmap bluredBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            int x, y;
            for (x = 0; x < bitmap.Width; x++)
            {
                for (y = 0; y < bitmap.Height; y++)
                {
                    int newRed = 0;
                    int newGreen = 0;
                    int newBlue = 0;

                    Color pixelColor = bitmap.GetPixel(x, y);

                    BoxBlur(bitmap, x, y, ref newRed, ref newGreen, ref newBlue);

                    Color newColor = Color.FromArgb(pixelColor.A, newRed, newGreen, newBlue);

                    bluredBitmap.SetPixel(x, y, newColor);
                }
            }

            bluredBitmap.Save(savePath);
        }

        [STAThread]
        static void Main(string[] args)
        {
            Bitmap imageForBlur;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap images (*.bmp)|*.bmp";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            try
            {

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        imageForBlur = (Bitmap)Image.FromFile(openFileDialog.FileName, true);
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
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.ReadKey();
                return;
            }

            Console.WriteLine("File opened succesfully!");

            string newPath = openFileDialog.FileName;
            newPath = newPath.Substring(0, newPath.Length - 4) + "_blured.bmp";

            try
            {
                BlurImage(imageForBlur, newPath);
            }
            catch (Exception ex)
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
