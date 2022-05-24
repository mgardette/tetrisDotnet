﻿using GalaSoft.MvvmLight.Command;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using tetrisDotnet.model;

namespace tetrisDotnet.viewmodel
{
    class ViewModel
    {
        public GameState gameState = new GameState();

        private readonly cellviewmodel[] _cells = new cellviewmodel[10 * 20];
        public ViewModel()
        {
            var gameGrid = new GameGrid();
            KeyPressedCommand = ReactiveCommand.Create<string>(k =>
                gameState.MoveBlockSide((Key)Enum.Parse(typeof(Key), k)));

            Cells = CreateCellViewModels(gameGrid);
            gameState.CellChanges.Subscribe(OnCellModelChanges);
        }

        public RelayCommand StartCommand => new RelayCommand(() => {
            Thread thread = new Thread(gameState.Start);
            thread.IsBackground = true;
            thread.Start();

        });

        private void OnCellModelChanges(CellChanges changes)
        {
            foreach (CellChange change in changes.Changes)
            {
                //if(change != null)
                //{
                    _cells[190 - change.CellY * 10 + change.CellX].Color = change.Color ?? Colors.DarkGray;
                //}
                string test = "test";
            }
            string testt = "test";
        }

        public ReadOnlyCollection<cellviewmodel> Cells { get; }

        private ReadOnlyCollection<cellviewmodel> CreateCellViewModels(GameGrid gameGrid)
        {
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 20; y++)
                {
                    _cells[x + 10 * y] =
                        new cellviewmodel(
                            x * (20 + 1),
                            (20 - y) * (20 + 1))
                        {
                            Color = gameGrid.Cells[y, x] ?? Colors.DarkGray
                        };
                }

            var cells = new ReadOnlyCollection<cellviewmodel>(_cells);
            return cells;
        }

        //public ReactiveCommand<Unit, Unit> StartCommand { get; }

        public ReactiveCommand<string, Unit> KeyPressedCommand { get; }

        public void moveBlockDown()
        {
            gameState.MoveBlockDown();
        }
    }

    class cellviewmodel : INotifyPropertyChanged
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
