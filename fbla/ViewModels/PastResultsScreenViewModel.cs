using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using fbla.Models;
namespace fbla.ViewModels
{
    public class PastResultsScreenViewModel : ViewModelBase
    {
        public PastResultsScreenViewModel()
        {
            Items = new ObservableCollection<PrevResultListNodeViewModel>();
            foreach (string f in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\fblaresults"))
            {
                Items.Add(new PrevResultListNodeViewModel(f));
            }

        }

        public ObservableCollection<PrevResultListNodeViewModel> Items { get; }
        


    }
}
