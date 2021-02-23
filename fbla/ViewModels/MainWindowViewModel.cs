using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using fbla.Models;
namespace fbla.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        Thread animThread;
        Serializer sz;
        dynamic EasingSetting = new Avalonia.Animation.Easings.CubicEaseOut();
        public MainWindowViewModel()
        {
            CurrentScreen = new HomeScreenViewModel();
            sz = new Serializer();
            animThread = new Thread(() => PlayAnimation(true));
            animThread.Start();

        }
        public void PlayAnimation(bool delayed = false)
        {
            backgroundOpacity = 0;
            if (delayed) { Thread.Sleep(500); }
            playBackDirection = Avalonia.Animation.PlaybackDirection.Normal;
            visible = true;
            disabledForAnimation = false;
            Thread.Sleep(750);
            disabledForAnimation = true;

        }
        public void ReverseAnimtion()
        {
            playBackDirection = Avalonia.Animation.PlaybackDirection.Reverse;
            disabledForAnimation = false;
            Thread.Sleep(750);
            disabledForAnimation = true;
        }

        private Avalonia.Animation.PlaybackDirection _playBackDirection = Avalonia.Animation.PlaybackDirection.Normal;
        public Avalonia.Animation.PlaybackDirection playBackDirection
        {
            get
            {
                return _playBackDirection;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _playBackDirection, value);
            }
        }
        private bool _visible = false;
        public bool visible
        {
            get
            {
                return _visible;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _visible, value);
            }
        }
        private bool _disabledForAnimation = true;
        public bool disabledForAnimation
        {
            get
            {
                return _disabledForAnimation;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _disabledForAnimation, value);
            }
        }
        private int _backgroundOpacity = 100;
        public int backgroundOpacity
        {
            get
            {
                return _backgroundOpacity;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _backgroundOpacity, value);
            }
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
            animThread = new Thread(() => ReverseAnimtion());
            Thread switchThread = new Thread(() => SwitchToQuiz(path));
            animThread.Start();
            switchThread.Start();
            
        }
        public void SwitchToQuiz(String path)
        {
            Thread.Sleep(750);
            animThread = new Thread(() => PlayAnimation());
            animThread.Start();
            if (path == null)
            {
                CurrentScreen = new QuizScreenViewModel(sz);
            }
            else
            {
                CurrentScreen = new QuizScreenViewModel(path);
            }
        }
        public void PastResults()
        {

            animThread = new Thread(() => ReverseAnimtion());
            Thread switchThread = new Thread(() => SwitchToPastResults());
            animThread.Start();
            switchThread.Start();
            
        }
        public void SwitchToPastResults()
        {
            Thread.Sleep(750);
            animThread = new Thread(() => PlayAnimation());
            animThread.Start();
            CurrentScreen = new PastResultsScreenViewModel("C:\\Users\\Gurv\\Documents\\fblaResults");
        }
        public void ReturnToHome()
        {
            if (CurrentScreen.GetType().Name == "QuizScreenViewModel")
            {
                if (((QuizScreenViewModel)CurrentScreen).submitted)
                {
                    CurrentScreen = new HomeScreenViewModel();
                    animThread = new Thread(() => PlayAnimation());
                    animThread.Start();
                }
                else if (((QuizScreenViewModel)CurrentScreen).forceSubmitting)
                {
                    CurrentScreen = new HomeScreenViewModel();
                    animThread = new Thread(() => PlayAnimation());
                    animThread.Start();
                }
                else
                {
                    ((QuizScreenViewModel)CurrentScreen).EarlyLeaveWarning();
                }
            }
            else
            {
                CurrentScreen = new HomeScreenViewModel();
                animThread = new Thread(() => PlayAnimation());
                animThread.Start();
            }
        }
    }
}
