using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Filters.Models;
using System.IO;
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

        /// <summary>Change the kernel image on the screen</summary>
        /// <param name="img">The path of the image to show</param>
        private void ChangeImage(string img)
        {
            Image image = this.FindControl<Image>("KernelImage");
            string current = Directory.GetCurrentDirectory();
            string target = Path.Combine(current, "src" + img);
            image.Source = new Bitmap(target);
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

        /// <summary>Change the kernel to applied to the image</summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The object that is being handled</param>
        private void KernelChanged(object sender, SelectionChangedEventArgs args)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem item = comboBox.SelectedItem as ComboBoxItem;
            MainWindowModel context = DataContext as MainWindowModel;

            if (context != null)
            {
                context.Options.KernelSelected = item.Name;
                context.Options.KernelImg = item.Name;
                ChangeImage("/assets/" + item.Name + ".png");

                if (item.Name.Equals("Custom"))
                    context.Options.ShowCustom = true;
                else
                    context.Options.ShowCustom = false;
            }
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
