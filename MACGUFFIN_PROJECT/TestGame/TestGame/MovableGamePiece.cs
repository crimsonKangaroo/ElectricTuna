using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace TestGame
{
    abstract class MovableGamePiece: GamePiece
    {

        private int direction;
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

       

        //public abstract void Move();



    }
}
