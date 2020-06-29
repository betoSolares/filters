using ReactiveUI;

namespace Filters.Models
{
    public class OptionsModel : ReactiveObject
    {
        // Path for the image
        private string path;
        public string Path
        {
            get => path;
            set => this.RaiseAndSetIfChanged(ref path, value);
        }

        // True for manual input for the path
        private bool isManual;
        public bool IsManual
        {
            get => isManual;
            set => this.RaiseAndSetIfChanged(ref isManual, value);
        }

        // The kernel selected to applied
        private string kernelSelected;
        public string KernelSelected
        {
            get => kernelSelected;
            set => this.RaiseAndSetIfChanged(ref kernelSelected, value);
        }

        // Path for the kernel image
        private string kernelImg;
        public string KernelImg
        {
            get => kernelImg;
            set => this.RaiseAndSetIfChanged(ref kernelImg, value);
        }

        /// <summary>Constructor</summary>
        public OptionsModel()
        {
            path = "No image. Please select one.";
            isManual = false;
            kernelSelected = "Blurred";
            kernelImg = "/assets/Blurred.png";
        }
    }
}
