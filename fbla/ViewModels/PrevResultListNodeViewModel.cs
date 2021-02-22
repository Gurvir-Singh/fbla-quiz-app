using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using fbla.Models;
using System.IO;
using ReactiveUI;
namespace fbla.ViewModels
{
    public class PrevResultListNodeViewModel : ViewModelBase
    {
        public PrevResultListNode Model { get; set; }
        public PrevResultListNodeViewModel(string Path)
        {
            Model = new PrevResultListNode(Path);
            path = Model.fullPath;
        }

        private string _path;
        public string path
        {
            get
            {
                return _path;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _path, value);
            }
        }
    }
}
