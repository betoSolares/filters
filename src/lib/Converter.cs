using System;
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

        /// <summary>Convert a matrix to bitmap</summary>
        /// <param name="matrix">The matrix to convert</param>
        /// <param name="original">The original bitmap to get the alpha value</param>
        /// <returns>The new bitmap</returns>
        public Bitmap MatrixToBitmap(double[,] matrix, (double min, double max) limits)
        {
            Bitmap result = new Bitmap(matrix.GetLength(0), matrix.GetLength(1));

            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    int scaled = Convert.ToInt32(((matrix[x, y] - limits.min) * 255) / (limits.max - limits.min));
                    Color newColor = Color.FromArgb(scaled, scaled, scaled);
                    result.SetPixel(x, y, newColor);
                }
            }

            return result;
        }
    }
}
