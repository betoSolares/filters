using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Filters.Views
{
    public class MainWindow : Window
    {
        /// <summary>Constructor</summary>
        public MainWindow()
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
