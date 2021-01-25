using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace fbla.Views
{
    public class TrueFalseQuestionView : UserControl
    {
        public TrueFalseQuestionView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
