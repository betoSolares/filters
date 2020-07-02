using ImageProcessing.Applicator;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageProcessing
{
    public class Processor
    {
        // Properties
        public string GrayScaled { get; private set; }
        public string GrayScaledApplied { get; private set; }
        public string Kernel { get; private set; }
        public string Original {get; private set; }
        public string OriginalApplied { get; private set; }

        private readonly string savePath;
        private readonly string filename;

        /// <summary>Constructor</summary>
        /// <param name="path">The path of the image to use</param>
        /// <param name="kernel">The kernel to apply</param>
        public Processor(string path, string kernel)
        {
            Kernel = kernel;
            Original = path;

            savePath = GetSaveDirectory();
            filename = Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>Create a new image applying the correct matrix</summary>
        /// <param name="matrix">The custom matrix to apply</param>
        public void GenerateImages(double[,] matrix)
        {
            KernelApplicator applicator = new KernelApplicator(Original, matrix);
            (Bitmap gray, Bitmap applied) bitmaps = applicator.Apply();

            string grayPath = Path.Combine(savePath, filename + "_grayscaled.png");
            GrayScaled = grayPath;
            bitmaps.gray.Save(grayPath, ImageFormat.Png);

            string appliedPath = Path.Combine(savePath, filename + "_" + Kernel + ".png");
            GrayScaledApplied = appliedPath;
            bitmaps.applied.Save(appliedPath, ImageFormat.Png);
            System.Console.WriteLine("WORKS");
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
    }
}
