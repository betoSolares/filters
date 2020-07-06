using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Filters.Models;
using ImageProcessing;
using System;
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

        /// <summary>Try to applied the kernel to the image</summary>
        /// <returns>The path where the images are stored</returns>
        private void ApplyKernel()
        {
            MainWindowModel context = DataContext as MainWindowModel;
            string path = context.Options.Path;
            string kernel = context.Options.KernelSelected;

            Processor processor = new Processor(path, kernel);

            try
            {
                double[,] matrix;
                if (kernel.Equals("Custom"))
                {
                    Dictionary<string, double> values = context.Options.CustomMatrix;
                    matrix = new double[3, 3]
                    {
                        { values["a"], values["b"], values["c"] },
                        { values["d"], values["e"], values["f"] },
                        { values["g"], values["h"], values["i"] }
                    };
                }
                else
                {
                    matrix = SetMatrix(kernel);
                }

                processor.GenerateImages(matrix);

                ChangeImage("OriginalImg", processor.Original);
                ChangeImage("GrayScaledImg", processor.GrayScaled);
                ChangeImage("ResultImg", processor.Applied);

                context.Output.Path = "The results are stores on the directory: " + processor.SavePath;
                context.Output.ShowResult = true;
            }
            catch (Exception e)
            {
                context.Output.ErrorMsg = "An un expected error ocurred: " + e.Message;
                context.Output.ShowError = true;
            }
        }

        /// <summary>Change the image on the screen</summary>
        /// <param name="name">The name of the component for the image</param>
        /// <param name="img">The path of the image to show</param>
        private void ChangeImage(string name, string img)
        {
            Image image = this.FindControl<Image>(name);
            image.Source = new Bitmap(img);
        }

        /// <summary>Validate the options and create the new image</summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The object that is being handled</param>
        private void GenerateNewImage(object sender, RoutedEventArgs e)
        {
            MainWindowModel context = DataContext as MainWindowModel;
            context.Output.ShowResult = false;
            context.Output.ShowError = false;

            if (HasValidOptions())
                ApplyKernel();
            else
                context.Output.ShowError = true;
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

        /// <summary>Get the correct matrix for the kernel</summary>
        /// <param name="kernel">The kernel to get the matrix</param>
        /// <returns>The matrix to use</returns>
        private double[,] SetMatrix(string kernel)
        {
            double[,] matrix;

            if (kernel.Equals("Blurred"))
            {
                matrix = new double[3, 3]
                {
                    { 1, 2, 1 },
                    { 2, 4, 2 },
                    { 1, 2, 1 }
                };
            }
            else if (kernel.Equals("Enhancement"))
            {
                matrix = new double[3, 3]
                {
                    { -2, -1, 0 },
                    { -1, 1, 1 },
                    { 0, 1, 2 }
                };
            }
            else if (kernel.Equals("LeftS"))
            {
                matrix = new double[3, 3]
                {
                    { 1, 0, -1 },
                    { 2, 0, -2 },
                    { 1, 0, -1 }
                };
            }
            else if (kernel.Equals("LowS"))
            {
                matrix = new double[3, 3]
                {
                    { -1, -2, -1 },
                    { 0, 0, 0 },
                    { 1, 2, 1}
                };
            }
            else if (kernel.Equals("Original"))
            {
                matrix = new double[3, 3]
                {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 0, 0 }
                };
            }
            else if (kernel.Equals("Outline"))
            {
                matrix = new double[3, 3]
                {
                    { -1, -1, -1 },
                    { -1, 8, -1 },
                    { -1, -1, -1 }
                };
            }
            else if (kernel.Equals("RightS"))
            {
                matrix = new double[3, 3]
                {
                    { -1, 0, 1 },
                    { -2, 0, 2 },
                    { -1, 0, 1 }
                };
            }
            else if (kernel.Equals("Sharpen"))
            {
                matrix = new double[3, 3]
                {
                    { 0, -1, 0 },
                    { -1, 5, -1 },
                    { 0, -1, 0 }
                };
            }
            else
            {
                matrix = new double[3, 3]
                {
                    { 1, 2, 1},
                    { 0, 0, 0 },
                    { -1, -2, -1 }
                };
            }

            return matrix;
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
