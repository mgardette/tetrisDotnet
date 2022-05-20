using JetBrains.Annotations;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using tetrisDotnet.model;

namespace tetrisDotnet.viewmodel
{
    class ViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public GameState gameState = new GameState(); 

        public ViewModel()
        {
            StartCommand = ReactiveCommand.CreateFromTask(gameState.Start);
            KeyPressedCommand = ReactiveCommand.Create<string>(k =>
                gameState.MoveBlockSide((Key)Enum.Parse(typeof(Key), k)));
            
            Cells = CreateCellViewModels(gameState);
            gameState.CellChanges.Subscribe(OnCellModelChanges);
        }
        public ReadOnlyCollection<CellViewModel> Cells { get; }

        public ReactiveCommand<Unit, Unit> StartCommand { get; }

        public ReactiveCommand<string, Unit> KeyPressedCommand { get; }

        public void moveBlockDown()
        {
            gameState.MoveBlockDown();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
