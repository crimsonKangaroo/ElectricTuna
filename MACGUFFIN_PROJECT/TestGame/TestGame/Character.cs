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
    class Character: MovableGamePiece
    {
        KeyboardState kState2;
        Game1 gm1 = new Game1();


        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public Character()
        {
            
        }

        public void Move()
        {
            kState2 = Keyboard.GetState();

            if (kState2.IsKeyDown(Keys.A)) gm1.place.X -= 6;
            if (kState2.IsKeyDown(Keys.D)) gm1.place.X += 6;
        }


        public void TakeHit()
        {
            // will loose a macguffin when hit
        }


        public void Attack()
        {
            // attacks with stun baton
        }


        public void Jump()
        {
            
        }
    }
}
