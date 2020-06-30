using System.Drawing;

namespace ImageProcessing.Convertion
{
    public class Converter
    {
        /// <summary>Transform an bitmap from RGB to gray scale</summary>
        /// <param name="original">The bitmap to change</param>
        /// <returns>The new grayscaled bitmap</returns>
        public Bitmap FromColorToGray(Bitmap original)
        {
            Bitmap grayscaled = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int average = (pixel.R + pixel.G + pixel.B) / 3;
                    Color newColor = Color.FromArgb(pixel.A, average, average, average);
                    grayscaled.SetPixel(x, y, newColor);
                }
            }

            return grayscaled;
        }
    }
}
