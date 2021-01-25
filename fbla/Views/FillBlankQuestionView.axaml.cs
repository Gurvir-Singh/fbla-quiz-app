using Avalonia;
using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using fbla.ViewModels;
namespace fbla.Views
{
    public class FillBlankQuestionView : UserControl
    {
        public FillBlankQuestionView()
        {
            this.InitializeComponent();

        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
