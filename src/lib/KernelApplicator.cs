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
        public (Bitmap gray, Bitmap applied) Apply()
        {
            Converter converter = new Converter();
            Bitmap original = new Bitmap(path);

            Bitmap grayscaled = converter.FromColorToGray(original);
            double[,] toConvert = ApplyAndNormalize(grayscaled);
            Bitmap applied = converter.MatrixToBitmap(toConvert, GetMinMaxValues(toConvert));

            return (grayscaled, applied);
        }

        /// <summary>Apply the kernel and normalize the bitmap</summary>
        /// <param name="bitmap">The bitmap to apply the image</param>
        /// <returns>The matrix to convert to bitmap</returns>
        private double[,] ApplyAndNormalize(Bitmap bitmap)
        {
            double[,] matrix = GetInnerValues(bitmap);
            matrix = FixBorders(bitmap, matrix);
            matrix = Normalize(matrix);
            return matrix;
        }

        /// <summary>Fix the border issue</summary>
        /// <param name="bitmap">The original bitmap to get the values</param>
        /// <param name="values">The matrix containing the new values to save the borders</param>
        /// <returns>A new matrix with the borders and values for the new bitmap</returns>
        private double[,] FixBorders(Bitmap bitmap, double[,] values)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            /* Top and bottom border */
            for (int x = 0; x < width; x++)
            {
                double[,] multiplyTop = new double[3, 3];
                double[,] multiplyBottom = new double[3, 3];

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
                values[x, height - 1] = GetCentralValue(multiplyBottom);
            }

            /* Left and right border */
            for (int y = 1; y < height - 1; y++)
            {
                double[,] multiplyLeft = new double[3, 3];
                double[,] multiplyRight = new double[3, 3];

                multiplyLeft[0, 0] = multiplyLeft[1, 0] = GetAverage(bitmap, 0, y - 1);
                multiplyLeft[0, 1] = multiplyLeft[1, 1] = GetAverage(bitmap, 0, y);
                multiplyLeft[0, 2] = multiplyLeft[1, 2] = GetAverage(bitmap, 0, y + 1);
                multiplyLeft[2, 0] = GetAverage(bitmap, 1, y - 1);
                multiplyLeft[2, 1] = GetAverage(bitmap, 1, y);
                multiplyLeft[2, 2] = GetAverage(bitmap, 1, y + 1);

                multiplyRight[1, 0] = multiplyRight[2, 0] = GetAverage(bitmap, width - 1, y - 1);
                multiplyRight[1, 1] = multiplyRight[2, 1] = GetAverage(bitmap, width - 1, y);
                multiplyRight[1, 2] = multiplyRight[2, 2] = GetAverage(bitmap, width - 1, y + 1);
                multiplyRight[0, 0] = GetAverage(bitmap, width - 2, y - 1);
                multiplyRight[0, 1] = GetAverage(bitmap, width - 2, y);
                multiplyRight[0, 2] = GetAverage(bitmap, width - 2, y + 1);

                values[0, y] = GetCentralValue(multiplyLeft);
                values[width - 1, y] = GetCentralValue(multiplyRight);
            }

            return values;
        }

        /// <summary>Get the average value from the pixel</summary>
        /// <param name="bitmap">The bitmap to get the average value</param>
        /// <param name="x">The x position of the pixel</param>
        /// <param name="y">The y position of the pixel</param>
        /// <returns>The average value from the pixel</returns>
        private double GetAverage(Bitmap bitmap, int x, int y)
        {
            Color pixel = bitmap.GetPixel(x, y);
            double value = (pixel.R + pixel.G + pixel.B) / 3;
            return value;
        }

        /// <summary>Get the new central value</summary>
        /// <param name="multiplyValues">The matrix to get the central value</param>
        /// <returns>The new value for the matrix</returns>
        private double GetCentralValue(double[,] multiplyValues)
        {
            double value = 0;

            for (int y = 0; y < multiplyValues.GetLength(1); y++)
            {
                for (int x = 0; x < multiplyValues.GetLength(0); x++)
                {
                    value += matrix[x, y] * multiplyValues[x, y];
                }
            }

            return value;
        }

        /// <summary>Apply the kernel to the bitmap and get the inner values</summary>
        /// <param name="bitmap">The bitmap to apply the kernel</param>
        /// <returns>A new matrix with the values</returns>
        private double[,] GetInnerValues(Bitmap bitmap)
        {
            double[,] result = new double[bitmap.Width, bitmap.Height];

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
                                double average = GetAverage(bitmap, x + cont_x, y + cont_y);
                                newValue += (average * matrix[cont_x, cont_y]);
                                cont_x++;
                            }

                            cont_y++;
                        }

                        result[x + 1, y + 1] = newValue;
                    }
                }
            }

            return result;
        }

        /// <summary>Get the min and max values in the matrix</summary>
        /// <param name="values">The matrix to get the values</param>
        /// <returns>The minimum and maximum values in the matrix</returns>
        private (double min, double max) GetMinMaxValues(double[,] values)
        {
            double min = values[0, 0];
            double max = values[0, 0];

            for (int y = 0; y < values.GetLength(1); y++)
            {
                for (int x = 0; x < values.GetLength(0); x++)
                {
                    if (values[x, y] > max)
                        max = values[x, y];
                    else if (values[x, y] < min)
                        min = values[x, y];
                }
            }

            return (min, max);
        }

        /// <summary>Normalize the values inside the matrix</summary>
        /// <param name="values">The matrix to normalize</param>
        /// <returns>A new matrix with the values normalized</returns>
        private double[,] Normalize(double[,] values)
        {
            double[,] normalized = new double[values.GetLength(0), values.GetLength(1)];
            (double min, double max) limits = GetMinMaxValues(values);

            for (int y = 0; y < values.GetLength(1); y++)
            {
                for (int x = 0; x < values.GetLength(0); x++)
                {
                    normalized[x, y] = values[x, y] / (limits.max - limits.min);
                }
            }

            return normalized;
        }
    }
}
