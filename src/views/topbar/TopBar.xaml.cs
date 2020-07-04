using Avalonia.Controls;
using Avalonia.Interactivity;
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

        /// <summary>Change the theme of the app</summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The object that is being handled</param>
        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("CHANGE");
        }

        /// <summary>Initialize all the components</summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
