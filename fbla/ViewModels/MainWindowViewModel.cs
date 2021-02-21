using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace fbla.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        

        public MainWindowViewModel()
        {
            CurrentScreen = new HomeScreenViewModel();
        }

        private ViewModelBase _CurrentScreen;
        public ViewModelBase CurrentScreen { 
            get
            {
                return _CurrentScreen;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _CurrentScreen, value);
            }
        }

        public void startQuiz()
        {
            CurrentScreen = new QuizScreenViewModel();
        }
        public void PastResults()
        {

        }
        
        public void ReturnToHome()
        {
            CurrentScreen = new HomeScreenViewModel();
        }
    }
}
