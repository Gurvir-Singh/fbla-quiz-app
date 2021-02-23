using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace fbla.Views
{
    public class HomeScreenView : UserControl
    {
        public HomeScreenView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

        }
        private void OnQuitClicked(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent.Parent.Parent).Close();
        }

    }
}
