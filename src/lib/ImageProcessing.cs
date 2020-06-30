using System.IO;

namespace ImageProcessing
{
    public class Processing
    {
        // Properties
        private readonly string OriginalImgPath;
        private readonly string Kernel;
        private readonly string SavePath;

        /// <summary>Constructor</summary>
        /// <param name="path">The path of the original image</param>
        /// <param name="kernel">The kernel to apply to the image</param>
        public Processing(string path, string kernel)
        {
            OriginalImgPath = path;
            Kernel = kernel;
            SavePath = GetSaveDirectory();
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
