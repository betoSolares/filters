namespace ImageProcessing
{
    public class Processing
    {
        // Properties
        private readonly string Path;
        private readonly string Kernel;

        /// <summary>Constructor</summary>
        /// <param name="path">The path of the original image</param>
        /// <param name="kernel">The kernel to apply to the image</param>
        public Processing(string path, string kernel)
        {
            Path = path;
            Kernel = kernel;
        }
    }
}
