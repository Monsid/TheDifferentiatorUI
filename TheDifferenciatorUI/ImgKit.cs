using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDifferenciatorUI
{
    public static class ImgKit
    {
        public static Bitmap cropAtRect(this Image b, Rectangle r)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(b, -r.X, -r.Y);
                return nb;
            }
        }

        public static float AveragePixelsBlack(int totalPixelsX, int totalPixelsY, Bitmap anImage)
        {
            float totalPixelScore = 0;

            float averagePixelScore;
            //for loop for all pixels to be averaged
            for (int pixelx = 0; pixelx < totalPixelsX; pixelx++)
            {
                for (int pixely = 0; pixely < totalPixelsY; pixely++)
                {
                    totalPixelScore = totalPixelScore + anImage.GetPixel(pixelx, pixely).GetBrightness();
                }
            }

            averagePixelScore = totalPixelScore / (totalPixelsX * totalPixelsY);
            return averagePixelScore;
        }

        public static float AverageBlue(int totalPixelsX, int totalPixelsY, Bitmap anImage)
        {
            float totalPixelScore = 0;

            float averagePixelScore;
            //for loop for all pixels to be averaged
            for (int pixelx = 0; pixelx < totalPixelsX; pixelx++)
            {
                for (int pixely = 0; pixely < totalPixelsY; pixely++)
                {
                    totalPixelScore = totalPixelScore + anImage.GetPixel(pixelx, pixely).B;
                }
            }

            averagePixelScore = totalPixelScore / (totalPixelsX * totalPixelsY);
            return averagePixelScore;
        }

        public static float AverageSat(int totalPixelsX, int totalPixelsY, Bitmap anImage)
        {
            float totalPixelScore = 0;

            float averagePixelScore;
            //for loop for all pixels to be averaged
            for (int pixelx = 0; pixelx < totalPixelsX; pixelx++)
            {
                for (int pixely = 0; pixely < totalPixelsY; pixely++)
                {
                    totalPixelScore = totalPixelScore + anImage.GetPixel(pixelx, pixely).GetSaturation();
                }
            }

            averagePixelScore = totalPixelScore / (totalPixelsX * totalPixelsY);
            return averagePixelScore;
        }

        public static bool isWet(float wetScore, Bitmap cropped)
        {
            int howmanypixelX = cropped.Width;

            int howmanypixelY = howmanypixelX / 2;



            //cropped.Save(@"C:\Users\TKand\Pictures\DIFFERENCIATOR\output\differentiator_someCROP_checkmypixels.jpg", ImageFormat.Jpeg);
            var AveragePxScore = ImgKit.AveragePixelsBlack(howmanypixelX, howmanypixelY, cropped);

            //getpixelyyy.getBrightness --- supposing this will be 0 for black, Trial and Error until i get it close to perfect
            if (AveragePxScore < wetScore) //insert pixel average instead of cropped.getonepixel
            {
                
                return true;
            }
            else
            {
                
                return false;
            }
        }

    }
}
