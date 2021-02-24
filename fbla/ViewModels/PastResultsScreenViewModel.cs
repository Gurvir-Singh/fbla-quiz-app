using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using fbla.Models;
using ReactiveUI;
namespace fbla.ViewModels
{
    public class PastResultsScreenViewModel : ViewModelBase
    {
        private Avalonia.Controls.Primitives.ScrollBarVisibility _scrollBarVisible = Avalonia.Controls.Primitives.ScrollBarVisibility.Hidden;
        public Avalonia.Controls.Primitives.ScrollBarVisibility scrollBarVisible
        {
            get
            {
                return _scrollBarVisible;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _scrollBarVisible, value);
            }
        }
        public PastResultsScreenViewModel()
        {
            Items = new ObservableCollection<PrevResultListNodeViewModel>();

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\fblaresults"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\fblaresults");
            }
            if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\fblaresults").Length > 6)
            {
                scrollBarVisible = Avalonia.Controls.Primitives.ScrollBarVisibility.Visible;
            }
            foreach (string f in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\fblaresults"))
            {
                Items.Add(new PrevResultListNodeViewModel(f));
            }

        }

        public ObservableCollection<PrevResultListNodeViewModel> Items { get; }
        


    }
}
