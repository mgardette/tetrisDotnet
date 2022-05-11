using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using tetrisDotnet.model;

namespace tetrisDotnet.view_model
{
    class ViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        GameState gameState = new GameState();

        public ViewModel()
        {
        }

        public void moveBlockDown()
        {
            gameState.MoveBlockDown();
        }

        public void moveBlockSide(string side)
        {
            switch (side)
            {
                case "right":
                    gameState.MoveBlockRight();
                    break;

                case "left":
                    gameState.MoveBlockLeft();
                    break;
            }
        }
    }
}
