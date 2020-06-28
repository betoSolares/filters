using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Filters.Models;
using System.Threading.Tasks;

namespace Filters.Views.Options
{
    public class Options : UserControl
    {
        /// <summary>Constructor</summary>
        public Options()
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
            context.Options.Path = path;
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

            string[] result = await dialog.ShowAsync(this.VisualRoot as Window);

            if (result.Length > 0)
                return result[0];
            return "No image. Please select one.";
        }

        /// <summary>Initialize all the components</summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        /// <summary>Display the controls based on the option selected</summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The object that is being handled</param>
        private void SelectChooseMethod(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            MainWindowModel context = DataContext as MainWindowModel;

            if (radioButton.Name.Equals("ManualRadio"))
                context.Options.IsManual = true;
            else
                context.Options.IsManual = false;
        }
    }
}
