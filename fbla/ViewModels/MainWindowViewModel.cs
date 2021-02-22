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
            CurrentScreen = new PastResultsScreenViewModel();
        }
        
        public void ReturnToHome()
        {
            if (CurrentScreen.GetType().Name == "QuizScreenViewModel")
            {
                if (((QuizScreenViewModel)CurrentScreen).submitted)
                {
                    CurrentScreen = new HomeScreenViewModel();
                }
                else if (((QuizScreenViewModel)CurrentScreen).forceSubmitting)
                {
                    CurrentScreen = new HomeScreenViewModel();
                }
                else
                {
                    ((QuizScreenViewModel)CurrentScreen).EarlyLeaveWarning();
                }
            }
            else
            {
                CurrentScreen = new HomeScreenViewModel();
            }
        }
    }
}
