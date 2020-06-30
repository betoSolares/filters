using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Filters.Models;
using System.Collections.Generic;
using System.IO;

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
            MainWindowModel context = DataContext as MainWindowModel;
            context.Output.Loading = true;

            if (HasValidOptions())
                System.Console.WriteLine("GENERATE");
            else
                context.Output.ShowError = true;
            context.Output.Loading = false;
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
                context.Output.ErrorMsg = "ERROR: Verify that the path exists and is a file";
                return false;
            }
            context.Output.ErrorMsg = "ERROR: Verify that the path exists and is a file";
            return false;
        }

        /// <summary>Check if the file is a PNG image</summary>
        /// <returns>True if the file is a PNG image</returns>
        private bool ValidImage()
        {
            MainWindowModel context = DataContext as MainWindowModel;
            string path = context.Options.Path;

            if (Path.GetExtension(path).Equals(".png"))
                    return true;
            context.Output.ErrorMsg = "ERROR: Only PNG files are allowed";
            return false;
        }

        /// <summary>Check if the matrix has only numbers</summary>
        /// <returns>True if the matrix is valid</returns>
        private bool ValidMatrix()
        {
            MainWindowModel context = DataContext as MainWindowModel;
            string kernel = context.Options.KernelSelected;
            Dictionary<string, double> values = context.Options.CustomMatrix;
            bool valid = true;
            context.Output.ErrorMsg = "ERROR: Check that the values of the matrix are only numbers";

            if (kernel.Equals("Custom"))
            {
                foreach (KeyValuePair<string, double> item in values)
                {
                    if (item.Value < -21474 && item.Value > 21474)
                        valid = false;
                }
                return valid;
            }
            return true;
        }
    }
}
