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

        /// <summary>Constructor</summary>
        public OutputModel()
        {
            showError = false;
            errorMsg = "ERROR: Not a valid file";
        }
    }
}
