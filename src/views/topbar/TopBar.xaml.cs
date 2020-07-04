using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Filters.Models;
using System;

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
            MainWindowModel context = DataContext as MainWindowModel;

            if (context.CurrentTheme.Equals("Rust"))
                LoadTheme("Citrus");
            else
                LoadTheme("Rust");
        }

        /// <summary>Load the specified theme</summary>
        /// <param name="name">The name of the theme to load</param>
        private void LoadTheme(string name)
        {
            Window window = this.VisualRoot as Window;
            MainWindowModel context = DataContext as MainWindowModel;

            if (window.Styles.Count == 0)
                window.Styles.Add(CreateStyle("avares://Citrus.Avalonia/" + name + ".xaml"));
            else
                window.Styles[0] = CreateStyle("avares://Citrus.Avalonia/" + name + ".xaml");

            context.CurrentTheme = name;
        }

        /// <summary>Create the new style to use</summary>
        /// <param name="url">The url for the style</param>
        /// <returns>A new style to use</returns>
        private StyleInclude CreateStyle(string url)
        {
            Uri self = new Uri("resm:resm:Styles?assembly=Filters");
            return new StyleInclude(self)
            {
                Source = new Uri(url)
            };
        }

        /// <summary>Initialize all the components</summary>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
