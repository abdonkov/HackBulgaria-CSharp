using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace InterpolateImage
{
    class Program
    {
        static void ResampleImage(Bitmap bitmap, Size newSize, string savePath)
        {
            Bitmap resizedBitmap = new Bitmap(newSize.Width, newSize.Height);

            double xScale = (double)bitmap.Width / resizedBitmap.Width;
            double yScale = (double)bitmap.Height / resizedBitmap.Height;

            int x, y;
            for (x = 0; x < newSize.Width; x++)
            {
                for (y = 0; y < newSize.Height; y++)
                {
                    int oldPixelX = (int)Math.Round(x * xScale);
                    int oldPixelY = (int)Math.Round(y * yScale);
                    if (oldPixelX > bitmap.Width) oldPixelX = bitmap.Width - 1;
                    if (oldPixelY > bitmap.Height) oldPixelX = bitmap.Height -1;

                    resizedBitmap.SetPixel(x, y, bitmap.GetPixel(oldPixelX, oldPixelY));
                }
            }

            resizedBitmap.Save(savePath);


        }

        [STAThread]
        static void Main(string[] args)
        {
            Bitmap imageForResize;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap images (*.bmp)|*.bmp";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();

            try
            {

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        imageForResize = (Bitmap)Image.FromFile(openFileDialog.FileName, true);
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

            Size newSize = new Size(0, 0);
            Console.WriteLine("Current Width: {0}\nCurrent Height: {1}", imageForResize.Width, imageForResize.Height);
            Console.WriteLine("Enter new Width: ");
            newSize.Width = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new Height: ");
            newSize.Height = int.Parse(Console.ReadLine());

            string newPath = openFileDialog.FileName;
            newPath = newPath.Substring(0, newPath.Length - 4) + "_resized.bmp";

            try
            {
                ResampleImage(imageForResize, newSize, newPath);
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
