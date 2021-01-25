using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace fbla.Views
{
    public class MultipleChoiceQuestionView : UserControl
    {
        public MultipleChoiceQuestionView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
