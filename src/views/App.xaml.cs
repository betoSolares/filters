using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Filters.Models;
using Filters.Views.Main;

namespace Filters.Views
{
    public class App : Application
    {
        /// <summary>Initialization method</summary>
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        /// <summary>Actions when the framework have completed initialization<summary>
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = new MainWindowModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
   }
}
