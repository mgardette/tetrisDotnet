using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace tetrisDotnet.viewmodel
{
    class cellviewmodel: INotifyPropertyChanged
    {
        private Color _color;

        public cellviewmodel(int x, int y)
        {
            X = x;
            Y = y;
            _color = Colors.DarkGray;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Color Color
        {
            get => _color;
            set
            {
                if (value.Equals(_color)) return;
                _color = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
