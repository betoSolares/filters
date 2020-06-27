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

        /// <summary>Let the user choose an image and put the path on screen</summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The object that is being handled</param>
        private async void ChooseImage(object sender, RoutedEventArgs e)
        {
            string path = await GetPath();
            MainWindowModel context = DataContext as MainWindowModel;
            context.Path = path;
        }

        /// <summary>Open the file chooser</summary>
        /// <returns>The path of the image selected</returns>
        private async Task<string> GetPath()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Choose Image",
                AllowMultiple = false
            };
            dialog.Filters.Add(new FileDialogFilter { Name = "Image", Extensions = { "png" } });

            string[] result = await dialog.ShowAsync(this);
            if (result.Length > 0)
                return result[0];
            return "No image. Please select one.";
        }

        /// <summary>Initialize all the components</summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
