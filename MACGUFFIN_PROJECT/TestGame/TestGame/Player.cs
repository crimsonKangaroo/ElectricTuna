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

        private int playerNum;
        public int PlayerNum
        {
            get { return playerNum; }
            set { playerNum = value; }
        }


        private int macGuffinCount;
        public int MacGuffinCount
        {
            get { return macGuffinCount; }
            set { macGuffinCount = value; }
        }




    }
}
