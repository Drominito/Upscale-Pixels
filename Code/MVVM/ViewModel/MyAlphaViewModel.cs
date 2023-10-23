using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upscale_Pixels.Code.MVVM.ViewModel
{
    internal class MyAlphaViewModel : INotifyPropertyChanged
    {
        private string _MyProperty;
        public string MyProperty
        {
            get => _MyProperty;

            set
            {
                if (_MyProperty != value)
                {
                    _MyProperty = value;
                    OnPropertyChanged(nameof(MyProperty));
                }
            }


        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
