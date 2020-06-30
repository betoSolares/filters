using ImageProcessing.Applicator;
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

        /// <summary>Constructor</summary>
        /// <param name="path">The path of the image to use</param>
        /// <param name="kernel">The kernel to apply</param>
        public Processor(string path, string kernel)
        {
            Kernel = kernel;
            Original = path;

            savePath = GetSaveDirectory();
        }

        /// <summary>Create a new image applying the correct matrix</summary>
        /// <param name="matrix">The custom matrix to apply</param>
        public void GenerateImages(double[,] matrix)
        {
            KernelApplicator applicator = new KernelApplicator(Original, matrix);
            applicator.Apply();
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
