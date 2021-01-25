using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace fbla.Views
{
    public class CheckboxQuestionView : UserControl
    {
        public CheckboxQuestionView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
