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

        public void startQuiz(String path)
        {
            if (path == null)
            {
                CurrentScreen = new QuizScreenViewModel();
            }
            else
            {
                CurrentScreen = new QuizScreenViewModel(path);
            }
        }
        public void PastResults()
        {
            CurrentScreen = new PastResultsScreenViewModel("C:\\Users\\Gurv\\Documents\\fblaResults");
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
