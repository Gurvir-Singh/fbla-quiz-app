using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace fbla.Views
{
    public class PastResultsScreenView : UserControl
    {
        public PastResultsScreenView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
