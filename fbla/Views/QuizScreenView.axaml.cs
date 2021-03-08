using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using fbla.ViewModels;
using Avalonia.Interactivity;
using System.Threading;

namespace fbla.Views
{
    public class QuizScreenView : UserControl
    {
        public QuizScreenView()
        {
            this.InitializeComponent();
            
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.IsEnabled = false;
            this.IsEnabled = true;
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {

            Button buttonClicked = (Button)sender;
            buttonClicked.Opacity = 0;
            Grid buttonContainer = (Grid)(buttonClicked.Parent);
            Border borderOfButton = (Border)(buttonContainer.Parent);
            borderOfButton.IsHitTestVisible = false;
            Thread animThread = new Thread(() => ReEnable(sender));
            //animThread.Start();
        }
        private void ReEnable(object sender)
        {
            Thread.Sleep(100);
            Button buttonClicked = (Button)sender;
            buttonClicked.Opacity = 100;
            Grid buttonContainer = (Grid)(buttonClicked.Parent);
            Border borderOfButton = (Border)(buttonContainer.Parent);
            borderOfButton.IsHitTestVisible = true;
        }
    }
}
