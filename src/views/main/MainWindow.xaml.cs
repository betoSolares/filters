using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Filters.Models;
using System.Threading.Tasks;

namespace Filters.Views.Main
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
