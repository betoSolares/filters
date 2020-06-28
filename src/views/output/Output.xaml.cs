using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Filters.Views.Output
{
    public class Output : UserControl
    {
        /// <summary>Constructor</summary>
        public Output()
        {
            InitializeComponent();
        }

        /// <summary>Initialize all the components</summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
