using ImageProcessing.Convertion;
using System;
using System.Drawing;

namespace ImageProcessing.Applicator
{
    public class KernelApplicator
    {
        // Properties
        private readonly double[,] matrix;
        private readonly string path;

        /// <summary>Constructor</summary>
        /// <param name="path">The path for the image</param>
        /// <param name="matrix">The matrix to apply</matrix>
        public KernelApplicator(string path, double[,] matrix)
        {
            this.path = path;
            this.matrix = matrix;
        }

        /// <summary>Apply the kernel to the image</summary>
        /// <returns>The list with the bitmaps generated</returns>
        public Bitmap Apply()
        {
            Converter converter = new Converter();
            Bitmap original = new Bitmap(path);

            Bitmap grayscaled = converter.FromColorToGray(original);
            ApplyAndNormalize(grayscaled);
            return grayscaled;
        }

        /// <summary>Apply the kernel to the bitmap</summary>
        /// <param name="bitmap">The bitmap to apply the kernel</param>
        /// <returns>A new matrix with the values</returns>
        private int[,] ApplyKernel(Bitmap bitmap)
        {
            int[,] result = new int[bitmap.Width, bitmap.Height];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    double newValue = 0;

                    if ((x + 2 < bitmap.Width) && (y + 2 < bitmap.Height))
                    {
                        int cont_y = 0;

                        while (cont_y < 3)
                        {
                            int cont_x = 0;

                            while (cont_x < 3)
                            {
                                int average = GetAverage(bitmap, x + cont_x, y + cont_y);
                                newValue += (average * matrix[cont_x, cont_y]);
                                cont_x++;
                            }

                            cont_y++;
                        }

                        int value = Convert.ToInt32(newValue);
                        result[x + 1, y + 1] = value;
                    }
                }
            }

            return result;
        }

        private void ApplyAndNormalize(Bitmap bitmap)
        {
            int[,] matrix = ApplyKernel(bitmap);
            int[,] fixedBorders = FixBorders(bitmap, matrix);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    System.Console.Write(fixedBorders[x, y] + "  ");
                }
                System.Console.WriteLine("\n#############################################\n");
            }
        }

        /// <summary>Fix the border issue</summary>
        /// <param name="bitmap">The original bitmap to get the values</param>
        /// <param name="values">The matrix containing the new values to save the borders</param>
        /// <returns>A new matrix with the borders and values for the new bitmap</returns>
        private int[,] FixBorders(Bitmap bitmap, int[,] values)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            /* Top and bottom border */
            for (int x = 0; x < width; x++)
            {
                int[,] multiplyTop = new int[3, 3];
                int[,] multiplyBottom = new int[3, 3];

                if (x == 0)
                {
                    multiplyTop[0, 0] = multiplyTop[1, 0] = GetAverage(bitmap, 0, 0);
                    multiplyTop[0, 1] = multiplyTop[1, 1] = GetAverage(bitmap, 0, 0);
                    multiplyTop[2, 0] = multiplyTop[2, 1] = GetAverage(bitmap, 1, 0);
                    multiplyTop[0, 2] = multiplyTop[1, 2] = GetAverage(bitmap, 0, 1);
                    multiplyTop[2, 2] = GetAverage(bitmap, 1, 1);

                    multiplyBottom[0, 0] = multiplyBottom[1, 0] = GetAverage(bitmap, 0, height - 2);
                    multiplyBottom[0, 1] = multiplyBottom[1, 1] = GetAverage(bitmap, 0, height - 1);
                    multiplyBottom[0, 2] = multiplyBottom[1, 2] = GetAverage(bitmap, 0, height - 1);
                    multiplyBottom[2, 1] = multiplyBottom[2, 2] = GetAverage(bitmap, 1, height - 1);
                    multiplyBottom[2, 0] = GetAverage(bitmap, 1, height - 2);
                }
                else if (x == width - 1)
                {
                    multiplyTop[0, 0] = multiplyTop[0, 1] = GetAverage(bitmap, x - 1, 0);
                    multiplyTop[1, 0] = multiplyTop[2, 0] = GetAverage(bitmap, x, 0);
                    multiplyTop[1, 1] = multiplyTop[2, 1] = GetAverage(bitmap, x, 0);
                    multiplyTop[1, 2] = multiplyTop[2, 2] = GetAverage(bitmap, x, 1);
                    multiplyTop[0, 2] = GetAverage(bitmap, x - 1, 1);

                    multiplyBottom[0, 1] = multiplyBottom[0, 2] = GetAverage(bitmap, x - 1, height - 1);
                    multiplyBottom[1, 0] = multiplyBottom[2, 0] = GetAverage(bitmap, x, height - 2);
                    multiplyBottom[1, 1] = multiplyBottom[2, 1] = GetAverage(bitmap, x, height - 1);
                    multiplyBottom[1, 2] = multiplyBottom[2, 2] = GetAverage(bitmap, x, height - 1);
                    multiplyBottom[0, 0] = GetAverage(bitmap, x - 1, height - 2);
                }
                else
                {
                    multiplyTop[0, 0] = multiplyTop[0, 1] = GetAverage(bitmap, x - 1, 0);
                    multiplyTop[1, 0] = multiplyTop[1, 1] = GetAverage(bitmap, x, 0);
                    multiplyTop[2, 0] = multiplyTop[2, 1] = GetAverage(bitmap, x + 1, 0);
                    multiplyTop[0, 2] = GetAverage(bitmap, x - 1, 1);
                    multiplyTop[1, 2] = GetAverage(bitmap, x, 1);
                    multiplyTop[2, 2] = GetAverage(bitmap, x + 1, 1);

                    multiplyBottom[0, 1] = multiplyBottom[0, 2] = GetAverage(bitmap, x - 1, height - 1);
                    multiplyBottom[1, 1] = multiplyBottom[1, 2] = GetAverage(bitmap, x, height - 1);
                    multiplyBottom[2, 1] = multiplyBottom[2, 2] = GetAverage(bitmap, x + 1, height - 1);
                    multiplyBottom[0, 0] = GetAverage(bitmap, x - 1, height - 2);
                    multiplyBottom[0, 1] = GetAverage(bitmap, x, height - 2);
                    multiplyBottom[0, 2] = GetAverage(bitmap, x + 1, height - 2);
                }

                values[x, 0] = GetCentralValue(multiplyTop);
                values[x, bitmap.Height - 1] = GetCentralValue(multiplyBottom);
            }

            return values;
        }

        /// <summary>Get the average value from the pixel</summary>
        /// <param name="bitmap">The bitmap to get the average value</param>
        /// <param name="x">The x position of the pixel</param>
        /// <param name="y">The y position of the pixel</param>
        /// <returns>The average value from the pixel</returns>
        private int GetAverage(Bitmap bitmap, int x, int y)
        {
            Color pixel = bitmap.GetPixel(x, y);
            int value = (pixel.R + pixel.G + pixel.B) / 3;
            return value;
        }

        /// <summary>Get the new central value</summary>
        /// <param name="multiplyValues">The matrix to get the central value</param>
        /// <returns>The new value for the matrix</returns>
        private int GetCentralValue(int[,] multiplyValues)
        {
            double value = 0;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    value += matrix[x, y] * multiplyValues[x, y];
                }
            }

            return Convert.ToInt32(value);
        }
    }
}
