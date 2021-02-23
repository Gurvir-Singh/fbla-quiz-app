using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using fbla.ViewModels;
using Avalonia.Interactivity;
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
    }
}
