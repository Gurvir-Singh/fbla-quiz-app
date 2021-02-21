using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace fbla.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        public string Greeting { get; set; }
        public HomeScreenViewModel()
        {
            DateTime now = DateTime.Now.ToLocalTime();
            if (now.ToString("tt") == "AM")
            {
                Greeting = "Good Morning";
                
            }
            else
            {
                Greeting = "Good Afternoon";
            }
        }

        private int _Width = 1000;
        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _Width, value);
            }
        }


        
    }
}
