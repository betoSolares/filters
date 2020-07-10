using ImageProcessing.Applicator;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageProcessing
{
    public class Processor
    {
        // Properties
        public string Applied { get; private set; }
        public string GrayScaled { get; private set; }
        public string Kernel { get; private set; }
        public string Original { get; private set; }
        public string SavePath { get; private set; }

        private readonly string filename;

        /// <summary>Constructor</summary>
        /// <param name="path">The path of the image to use</param>
        /// <param name="kernel">The kernel to apply</param>
        public Processor(string path, string kernel)
        {
            Kernel = kernel;
            Original = path;

            SavePath = GetSaveDirectory();
            filename = Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>Create a new image applying the correct matrix</summary>
        /// <param name="matrix">The custom matrix to apply</param>
        public void GenerateImages(double[,] matrix)
        {
            KernelApplicator applicator = new KernelApplicator(Original, matrix, Kernel);
            (Bitmap gray, Bitmap applied) bitmaps = applicator.Apply();

            string grayPath = Path.Combine(SavePath, filename + "_grayscaled.png");
            GrayScaled = grayPath;
            bitmaps.gray.Save(grayPath, ImageFormat.Png);

            string appliedPath = Path.Combine(SavePath, filename + "_" + Kernel + ".png");
            Applied = appliedPath;
            bitmaps.applied.Save(appliedPath, ImageFormat.Png);
        }

        /// <summary>Get the directory to save the results</summary>
        /// <returns>The path to save the results</returns>
        private string GetSaveDirectory()
        {
            string current = Directory.GetCurrentDirectory();

            string save;
            if (current.Contains(Path.Combine("src", "bin", "Debug", "netcoreapp3.0")))
                save = Path.Combine(Directory.GetParent(current).Parent.Parent.FullName, "src/results");
            else if (current.Contains(Path.Combine("src", "bin", "Release", "netcoreapp3.0")))
                save = Path.Combine(Directory.GetParent(current).Parent.Parent.FullName, "src/results");
            else
                save = Path.Combine(current, "src/results");

            Directory.CreateDirectory(save);
            return save;
        }
    }
}
