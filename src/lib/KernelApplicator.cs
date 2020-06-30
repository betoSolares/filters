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
        public void Apply()
        {
        }
    }
}
