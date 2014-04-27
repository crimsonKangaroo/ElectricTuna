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
    class Player
    {

        private int playerNum; // Player's number (1 - 4)
        private PlayerIndex playerIndex;
        private int macGuffinCount; // number of macGuffins collected
        Texture2D sprite;
        
        // Player color (for differentiating the sprites - this should just be a temporary  measure)
        Color playerColor;
              
        public int PlayerNum
        {
            get { return playerNum; }
        }
        
        public int MacGuffinCount
        {
            get { return macGuffinCount; }
            set {
                    if (value < 0) macGuffinCount = 0;
                    else macGuffinCount = value;
                }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        

        public Color PlayerColor
        {
            get { return playerColor; }
        }

        // constructor

        public Player(int num, Color pColor)
        {
            playerNum = num;
            playerColor = pColor;
        }


    }
}
