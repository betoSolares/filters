using ReactiveUI;
using System.Collections.Generic;

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

        // Show the inputs for the custom matrix
        private bool showCustom;
        public bool ShowCustom
        {
            get => showCustom;
            set => this.RaiseAndSetIfChanged(ref showCustom, value);
        }

        // Values of the custom matrix
        private Dictionary<string, double> customMatrix;
        public Dictionary<string, double>  CustomMatrix
        {
            get => customMatrix;
            set => this.RaiseAndSetIfChanged(ref customMatrix, value);
        }

        /// <summary>Constructor</summary>
        public OptionsModel()
        {
            path = "No image. Please select one.";
            isManual = false;
            kernelSelected = "Blurred";
            kernelImg = "/assets/Blurred.png";
            showCustom = false;
            customMatrix = new Dictionary<string, double>()
            {
                { "a", 0 },
                { "b", 0 },
                { "c", 0 },
                { "d", 0 },
                { "e", 0 },
                { "f", 0 },
                { "g", 0 },
                { "h", 0 },
                { "i", 0 }
            };
        }
    }
}
