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
            int[,] applied = ApplyKernel(grayscaled);
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
                                Color pixel = bitmap.GetPixel(x + cont_x, y + cont_y);
                                int average = (pixel.R + pixel.G + pixel.B) / 3;
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
    }
}
