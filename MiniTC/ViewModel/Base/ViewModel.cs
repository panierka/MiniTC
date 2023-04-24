using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.ViewModel.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChange(params string[] names)
        {
            foreach(string name in names)
            {
                PropertyChanged?.Invoke(this, new(name));
            }
        }
    }
}
