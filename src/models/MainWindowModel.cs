using ReactiveUI;

namespace Filters.Models
{
    public class MainWindowModel : ReactiveObject
    {
        // Properties
        private string path;

        public string Path
        {
            get => path;
            set => this.RaiseAndSetIfChanged(ref path, value);
        }

        /// <summary>Constructor</summary>
        public MainWindowModel()
        {
            path = "No image. Please select one.";
        }
    }
}
