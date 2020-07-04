using ReactiveUI;

namespace Filters.Models
{
    public class OutputModel : ReactiveObject
    {

        // Show the error message
        private bool showError;
        public bool ShowError
        {
            get => showError;
            set => this.RaiseAndSetIfChanged(ref showError, value);
        }

        // The error message to display
        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set => this.RaiseAndSetIfChanged(ref errorMsg, value);
        }

        // Show the spinner loader
        private bool loading;
        public bool Loading
        {
            get => loading;
            set => this.RaiseAndSetIfChanged(ref loading, value);
        }

        // Show the results
        private bool showResult;
        public bool ShowResult
        {
            get => showResult;
            set => this.RaiseAndSetIfChanged(ref showResult, value);
        }

        // Show the path where the results are saved
        private string path;
        public string Path
        {
            get => path;
            set => this.RaiseAndSetIfChanged(ref path, value);
        }

        /// <summary>Constructor</summary>
        public OutputModel()
        {
            showError = false;
            errorMsg = "ERROR: Not a valid file";
            loading = false;
            showResult = false;
            path = "No path yet";
        }
    }
}
