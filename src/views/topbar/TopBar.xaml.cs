using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Filters.Views.TopBar
{
    public class TopBar : UserControl
    {
        /// <summary>Constructor</summary>
        public TopBar()
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
