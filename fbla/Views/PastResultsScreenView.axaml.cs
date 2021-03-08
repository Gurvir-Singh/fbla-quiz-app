using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
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

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            
            Button buttonClicked = (Button)sender;
            buttonClicked.Opacity = 0;
            Grid buttonContainer = (Grid)(buttonClicked.Parent);
            Border borderOfButton = (Border)(buttonContainer.Parent);
            borderOfButton.IsHitTestVisible = false;

        }

    }
}
