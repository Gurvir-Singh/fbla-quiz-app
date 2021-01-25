using System;
using System.Collections.Generic;
using System.Text;

namespace fbla.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {

            quizScreen = new QuizScreenViewModel();

        }
        public QuizScreenViewModel quizScreen { get; set; }

    }
}
