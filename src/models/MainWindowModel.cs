using Avalonia.Markup.Xaml.Styling;
using ReactiveUI;

namespace Filters.Models
{
    public class MainWindowModel : ReactiveObject
    {
        // Options for the output
        private OptionsModel options;
        public OptionsModel Options
        {
            get => options;
            set => this.RaiseAndSetIfChanged(ref options, value);
        }

        // Values of the output
        private OutputModel output;
        public OutputModel Output
        {
            get => output;
            set => this.RaiseAndSetIfChanged(ref output, value);
        }

        // The current theme of the application
        private string currentTheme;
        public string CurrentTheme
        {
            get => currentTheme;
            set => this.RaiseAndSetIfChanged(ref currentTheme, value);
        }

        /// <summary>Constructor</summary>
        public MainWindowModel()
        {
            options = new OptionsModel();
            output = new OutputModel();
            currentTheme = "Rust";
        }
    }
}
