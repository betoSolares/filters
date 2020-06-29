using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Filters.Models;
using System.IO;
using System;

namespace Filters.Views.Output
{
    public class Output : UserControl
    {
        /// <summary>Constructor</summary>
        public Output()
        {
            InitializeComponent();
        }

        /// <summary>Applied the kernel to the image</summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The object that is being handled</param>
        private void GenerateNewImage(object sender, RoutedEventArgs e)
        {
            if (HasValidOptions())
                Console.WriteLine("GENERATE");
            else
                Console.WriteLine("ERROR");
        }

        /// <summary>Check if has valid options to applied the kernel</summary>
        /// <returns>True if all the options are valid</returns>
        private bool HasValidOptions()
        {
            return PathExists() && ValidImage() && ValidMatrix();
        }

        /// <summary>Initialize all the components</summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        /// <summary>Check that the path for the image exists</summary>
        /// <returns>True if the path exists and is a file</returns>
        private bool PathExists()
        {
            MainWindowModel context = DataContext as MainWindowModel;
            string path = context.Options.Path;

            if (!string.IsNullOrEmpty(path) || !string.IsNullOrWhiteSpace(path))
            {
                if (!Directory.Exists(path))
                    if (File.Exists(path))
                        return true;
                return false;
            }
            return false;
        }

        private bool ValidImage()
        {
            return true;
        }

        private bool ValidMatrix()
        {
            return true;
        }
    }
}
