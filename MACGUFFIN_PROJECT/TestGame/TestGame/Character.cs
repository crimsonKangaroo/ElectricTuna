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
        GamePadState gPadState;
        Rectangle place;

        // attributes for jumping
        bool jumping = false;
        int startY; // holds current Y position
        int jumpSpeed; // will be used to act as 'gravity'

        // for collision
        // attributes to store Y cords in 
        int playerY1;
        int playerY2;

        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public Rectangle Place
        {
            get { return place; }
            set { place = value; }
        }

        public Character(Player pl)
        {
            player = pl;
            startY = place.Y;
            jumpSpeed = 0;
        }

        public void Move(List<Rectangle> walls)
        {
            gPadState = GamePad.GetState(player.Index);

            if (gPadState.ThumbSticks.Left.X <= -.5f) place.X -= 6;
            if (gPadState.ThumbSticks.Left.X >= .5f) place.X += 6;

            // jumping     
            if (jumping)
            {
                place.Y += jumpSpeed;
                jumpSpeed += 1;
                playerY1 = jumpSpeed;

                foreach (Rectangle rect in walls)
                {
                    if (place.Intersects(rect))
                    {
                        jumpSpeed = 0;
                        jumping = false;

                        if (gPadState.IsButtonDown(Buttons.A))
                        {
                            jumping = true;
                            jumpSpeed = -20;
                        }

                        while (place.Intersects(rect))
                        {
                            place.Y--;
                        }
                    }
                    else
                    {
                        //jumpSpeed = playerY1;
                        jumping = true;
                    }
                }
            }
            else
            {
                if (gPadState.IsButtonDown(Buttons.A))
                {
                    jumping = true;
                    jumpSpeed = -20;
                }
            }
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
