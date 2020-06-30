using System.IO;

namespace ImageProcessing
{
    public class Processor
    {
        /// <summary>Create a new image applying the correct kernel </summary>
        /// <param name="path">The path of the image</param>
        /// <param name="kernel">The kernel to apply</param>
        public void CreateNew(string path, string kernel)
        {
            double[,] matrix = SetMatrix(kernel);
            string savePath = GetSaveDirectory();
        }

        /// <summary>Create a new image applying a custom kernel</summary>
        /// <param name="path">The path of the image</param>
        /// <param name="matrix">The custom matrix to apply</param>
        public void CreateNew(string path, double[,] matrix)
        {
            string kernel = "Custom";
            string savePath = GetSaveDirectory();
        }

        /// <summary>Get the directory to save the results</summary>
        /// <returns>The path to save the results</returns>
        private string GetSaveDirectory()
        {
            string current = Directory.GetCurrentDirectory();

            string save;
            if (current.Contains(Path.Combine("filters", "src", "bin", "Debug", "netcoreapp3.0")))
                save = Path.Combine(Directory.GetParent(current).Parent.Parent.FullName, "src/results");
            else if (current.Contains(Path.Combine("filters", "src", "bin", "Release", "netcoreapp3.0")))
                save = Path.Combine(Directory.GetParent(current).Parent.Parent.FullName, "src/results");
            else
                save = Path.Combine(current, "src/results");

            Directory.CreateDirectory(save);
            return save;
        }

        /// <summary>Get the correct matrix for the kernel</summary>
        /// <param name="kernel">The kernel to get the matrix</param>
        /// <returns>The matrix to use</returns>
        private double[,] SetMatrix(string kernel)
        {
            double[,] matrix;

            if (kernel.Equals("Blurred"))
            {
                matrix = new double[3, 3]
                {
                    { 0.0625, 0.125, 0.0625 },
                    { 0.125, 0.25, 0.125 },
                    { 0.0625, 0.125, 0.0625 }
                };
            }
            else if (kernel.Equals("Enhancement"))
            {
                matrix = new double[3, 3]
                {
                    { -2, -1, 0 },
                    { -1, 1, 1 },
                    { 0, 1, 2 }
                };
            }
            else if (kernel.Equals("LeftS"))
            {
                matrix = new double[3, 3]
                {
                    { 1, 0, -1 },
                    { 2, 0, -2 },
                    { 1, 0, -1 }
                };
            }
            else if (kernel.Equals("LowS"))
            {
                matrix = new double[3, 3]
                {
                    { -1, -2, -1 },
                    { 0, 0, 0 },
                    { 1, 2, 1}
                };
            }
            else if (kernel.Equals("Original"))
            {
                matrix = new double[3, 3]
                {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 0, 0 }
                };
            }
            else if (kernel.Equals("Outline"))
            {
                matrix = new double[3, 3]
                {
                    { -1, -1, -1 },
                    { -1, 8, -1 },
                    { -1, -1, -1 }
                };
            }
            else if (kernel.Equals("RightS"))
            {
                matrix = new double[3, 3]
                {
                    { -1, 0, 1 },
                    { -2, 0, 2 },
                    { -1, 0, 1 }
                };
            }
            else if (kernel.Equals("Sharpen"))
            {
                matrix = new double[3, 3]
                {
                    { 0, -1, 0 },
                    { -1, 5, -1 },
                    { 0, -1, 0 }
                };
            }
            else
            {
                matrix = new double[3, 3]
                {
                    { 1, 2, 1},
                    { 0, 0, 0 },
                    { -1, -2, -1 }
                };
            }

            return matrix;
        }
    }
}
